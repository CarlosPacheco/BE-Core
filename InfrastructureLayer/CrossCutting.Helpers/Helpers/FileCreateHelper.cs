using CrossCutting.Helpers.Extensions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CrossCutting.Helpers.Helpers
{
    public static class FileCreateHelper
    {
        /// <summary>
        /// Create a excel file from the fileData
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileData"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Stream Excel<T>(IList<T> fileData, Func<T, object> mapper = null, string blankSheetName = "MySheet")
        {
            Stream stream = new MemoryStream();

            IWorkbook workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet(blankSheetName);

            bool headerWritten = false;

            int rowTotal = fileData.Count;

            for (int i = 0; i < rowTotal; i++)
            {
                IRow row = excelSheet.CreateRow(i);

                T fileRow = fileData[i];

                IDictionary<string, object> rowFileDic = mapper == null ? fileRow.ToDictionary() : mapper(fileRow).ToDictionary();

                if (!headerWritten)
                {
                    for (int j = 0; j < rowFileDic.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(rowFileDic.Keys.ElementAt(j));
                    }

                    headerWritten = true;
                    i++;
                }

                for (int j = 0; j < rowFileDic.Count; j++)
                {
                    object value = rowFileDic.Values.ElementAt(j);
                    if (value != null)
                    {
                        if (value is DateTime time)
                        {
                            row.CreateCell(j).SetCellValue(time.ToString("dd/MM/yyyy hh:mm"));
                        }
                        else if (!value.GetType().IsClass || value.GetType().IsSealed)
                        {
                            row.CreateCell(j).SetCellValue(value.ToString());
                        }
                    }
                }
            }

            //Save the new workbook. We haven't specified the filename so use the Save as method.
            workbook.Write(stream);

            stream.Position = 0;

            return stream;
        }

        /// <summary>
        /// Create a excel file from the fileData
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileData"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Stream Excel(IEnumerable<IDictionary<string, object>> fileData, Stream stream, string blankSheetName = "MySheet")
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet(blankSheetName);

            bool headerWritten = false;

            int rowTotal = fileData.Count();

            for (int i = 0; i < rowTotal; i++)
            {
                IRow row = excelSheet.CreateRow(i);

                IDictionary<string, object> rowDic = fileData.ElementAt(i);
                if (!headerWritten)
                {
                    for (int j = 0; j < rowDic.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(rowDic.Keys.ElementAt(j));
                    }

                    headerWritten = true;
                    i++;
                }

                for (int j = 0; j < rowDic.Count; j++)
                {
                    // the magic +2 is the position with the header row
                    object value = rowDic.Values.ElementAt(j);
                    if (value != null)
                    {
                        if (value is DateTime time)
                        {

                            row.CreateCell(j).SetCellValue(time.ToShortDateString());
                        }
                        else if (!value.GetType().IsClass)
                        {
                            row.CreateCell(j).SetCellValue(value.ToString());
                        }
                    }
                }
            }

            //Save the new workbook. We haven't specified the filename so use the Save as method.
            workbook.Write(stream);

            stream.Position = 0;

            return stream;
        }


        public static Stream Csv<T>(IList<T> fileData, Stream stream = null)
        {
            if (stream == null)
            {
                stream = new MemoryStream();
            }
            //Please don't closed the Stream! (the response need it open) Use this way and not this way -> stream.writeCsv(fileData)
            CsvSerializer.SerializeToStream(fileData, stream);
            stream.Position = 0;

            return stream;
        }
    }
}
