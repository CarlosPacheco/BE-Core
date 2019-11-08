using System.IO;
using System.Collections.Generic;
using System;
using Business.LogicObjects.ScheduledJob;
using Business.Entities.ScheduledJob;
using ScheduledJob.Entities;
using Business.LogicObjects.Loh;
using Business.LogicObjects.RootCause;
using System.Linq;

namespace ScheduledJob.Services
{
    public class HandlerEFF : IHandlerEFF
    {
        private IHandlerEffBLO HandlerEffBLO;
        private ILohBlo LohBLO;
        private IRootCauseBlo RootCauseBlo;

        public HandlerEFF(IHandlerEffBLO handlerEffBLO, ILohBlo lohBLO, IRootCauseBlo rootCauseBLO)
        {
            HandlerEffBLO = handlerEffBLO;
            LohBLO = lohBLO;
            RootCauseBlo = rootCauseBLO;
        }

        /// <summary>
        /// Check for old files to be deleted
        /// </summary>
        public void DeleteFilesTracking(Business.Entities.ScheduledJob.ScheduledJob scheduledJob)
        {
            List<FilesEff> filesEff = scheduledJob.FilesEff;
            List<DeleteTracking> deleteTracking = scheduledJob.DeleteTracking;

            foreach (DeleteTracking deleteTrack in deleteTracking)
            {
                DirectoryInfo directory = new DirectoryInfo(deleteTrack.FolderPath);
                FileInfo[] files = directory.GetFiles("*.eff"); //Getting Text files

                foreach (FileInfo file in files)
                {
                    TimeSpan dateDiff = file.LastWriteTimeUtc - DateTime.UtcNow;
                    if (dateDiff.Days > deleteTrack.Days)
                    {
                        if (deleteTrack.FolderPath == scheduledJob.LocalPath)
                        {
                            foreach (FilesEff fileEff in filesEff)
                            {
                                if (fileEff.FileName == file.Name && fileEff.ProcessState == Business.Enums.ScheduledJob.ProcessState.Completed)
                                {
                                    file.Delete();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            file.Delete();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets an ScheduledJob by it's unique identifier
        /// </summary>
        /// <param name="id">The loh unique identifier</param>
        /// <returns>Case with the specified unique identifier</returns>
        public Business.Entities.ScheduledJob.ScheduledJob GetById(int id)
        {
            return HandlerEffBLO.GetById(id);
        }

        /// <summary>
        /// Read all files
        /// </summary>
        /// <returns></returns>
        public void ReadFiles(Business.Entities.ScheduledJob.ScheduledJob scheduledJob)
        {
            try
            {
                if (!Directory.Exists(scheduledJob.SourcePath))
                {
                    throw new Exception($"Directory don't exist: {scheduledJob.SourcePath}");
                }

                if (!Directory.Exists(scheduledJob.LocalPath))
                {
                    throw new Exception($"Directory don't exist: {scheduledJob.LocalPath}");
                }

                DirectoryInfo sourceDirectory = new DirectoryInfo(scheduledJob.SourcePath);
                FileInfo[] sourcFiles = sourceDirectory.GetFiles("*.eff"); //Getting Text files

                //copy all files to be processing
                foreach (FileInfo sourceFile in sourcFiles)
                {
                    FilesEff sourceFileEff = new FilesEff
                    {
                        FileName = sourceFile.Name,
                        ProcessState = Business.Enums.ScheduledJob.ProcessState.Loading,
                        LastUpdated = sourceFile.LastWriteTimeUtc,
                        ScheduledJob = scheduledJob
                    };

                    if (!ProcessExistsFIle(scheduledJob, sourceFileEff))
                    {
                        sourceFile.CopyTo(Path.Combine(scheduledJob.LocalPath, sourceFileEff.FileName), true);
                        ReadFile(HandlerEffBLO.CreateFile(sourceFileEff));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool ProcessExistsFIle(Business.Entities.ScheduledJob.ScheduledJob scheduledJob, FilesEff sourceFileEff)
        {
            foreach (FilesEff fileEff in scheduledJob.FilesEff)
            {
                fileEff.ScheduledJob = scheduledJob;
                if (sourceFileEff.FileName == fileEff.FileName)
                {
                    if (sourceFileEff.LastUpdated.Subtract(DateTime.MinValue).TotalSeconds <= fileEff.LastUpdated.Subtract(DateTime.MinValue).TotalSeconds)
                    {
                        if (fileEff.ProcessState == Business.Enums.ScheduledJob.ProcessState.Failed)
                        {
                            ReadFile(fileEff);
                        }
                    }
                    else
                    {
                        File.Copy(Path.Combine(scheduledJob.SourcePath, sourceFileEff.FileName), Path.Combine(scheduledJob.LocalPath, sourceFileEff.FileName), true);
                        fileEff.LastUpdated = sourceFileEff.LastUpdated;
                        ReadFile(fileEff);
                    }

                    return true;
                }
            }

            return false;
        }

        private void ReadFile(FilesEff fileEff)
        {
            string separatorFile = ";";

            try
            {
                string[] lines = File.ReadAllLines(Path.Combine(fileEff.ScheduledJob.LocalPath, fileEff.FileName));
                //read header 
                string headerLine = lines[0];
                //<<EFF:1.00>>	Headers=37	Rows=45	Columns=115	Extractionname=b5622a11-e002-4a42-aeeb-64de74f6f377	EFFType=EBSXE_EFF_VERSION:01.006
                string[] headerRow = headerLine.Split(separatorFile);
                //get the start line number ( headers + Rows - headerLine )
                int headerIndex = int.Parse(headerRow[1].Split("=")[1]);

                EffFile effFile = new EffFile();

                for (int i = headerIndex; i < lines.Length; i++)
                {
                    string[] rowData = lines[i].Split(separatorFile);
                    if ("<+EFF:1.00>".Contains(rowData[0]))
                    {
                        effFile.Header = rowData;
                        continue;
                    }

                    if ("<+DataSource>".Contains(rowData[0]))
                    {
                        effFile.DataSource = rowData;
                        continue;
                    }

                    if ("<+SubMeasStep>".Contains(rowData[0]))
                    {
                        effFile.SubMeasStep = rowData;
                        continue;
                    }

                    if ("<+SeqNr>".Contains(rowData[0]))
                    {
                        effFile.SeqNr = rowData;
                        continue;
                    }

                    if ("20_Lot".Contains(rowData[0]))
                    {
                        effFile.LotData = rowData;
                        continue;
                    }
                }

                SerializeLotEff serializeLotEff = new SerializeLotEff();
                LotEff lotEff = serializeLotEff.Deserialize(effFile);

                Business.Entities.Loh.Loh loh = new Business.Entities.Loh.Loh();

                loh.LotId = lotEff.Lot;
                loh.ProcessGroup = lotEff.ProcessGroup;
                loh.BasicType = lotEff.BasicType;
                loh.LotDate = lotEff.LohTimestamp;
                loh.WaferQuantity = lotEff.WaferCount.ToString();
                loh.Bau = Convert.ToInt32(lotEff.Bau);

                Business.Entities.RootCause.RootCause rootCause = new Business.Entities.RootCause.RootCause();

                rootCause.Qmp = lotEff.QMP;
                rootCause.Sba = lotEff.SBA;

                Business.Entities.Loh.Loh existLoh = LohBLO.Get(new Business.SearchFilters.LohSearchFilter { LotId = loh.LotId }).FirstOrDefault();
                if (existLoh != null)
                {
                    existLoh.LotId = loh.LotId;
                    existLoh.ProcessGroup = loh.ProcessGroup;
                    existLoh.BasicType = loh.BasicType;
                    existLoh.LotDate = loh.LotDate;
                    existLoh.WaferQuantity = loh.WaferQuantity;
                    existLoh.Bau = loh.Bau;
                    LohBLO.Update(existLoh);

                    Business.Entities.RootCause.RootCause existRootCause = RootCauseBlo.GetById(existLoh.RootCause.Id.Value);
                    existRootCause.Qmp = rootCause.Qmp;
                    existRootCause.Sba = rootCause.Sba;
                    RootCauseBlo.Update(existRootCause);
                }
                else
                {
                    Business.Entities.Loh.Loh newLoh = LohBLO.Create(loh);
                    rootCause.LoH = newLoh;
                    RootCauseBlo.Create(rootCause);
                }
            }
            catch (Exception ex)
            {
                fileEff.ProcessState = Business.Enums.ScheduledJob.ProcessState.Failed;
                fileEff.ErrorCounter++;
                HandlerEffBLO.UpdateFile(fileEff);
                throw ex;
            }

            fileEff.ProcessState = Business.Enums.ScheduledJob.ProcessState.Completed;
            HandlerEffBLO.UpdateFile(fileEff);
        }
    }
}
