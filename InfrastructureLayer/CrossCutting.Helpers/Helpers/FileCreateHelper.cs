using CrossCutting.Helpers.Extensions;
using OfficeOpenXml;
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
        public static Stream Excel<T>(IList<T> fileData, Func<T, object> mapper = null, Stream stream = null, string blankSheetName = "MySheet")
        {
            if (stream == null)
            {
                stream = new MemoryStream();
            }

            using (ExcelPackage excel = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = excel.Workbook.Worksheets.Add(blankSheetName);

                bool headerWritten = false;

                int rowTotal = fileData.Count;

                ws.InsertRow(1, rowTotal);

                for (int i = 0; i < rowTotal; i++)
                {
                    T fileRow = fileData[i];

                    IDictionary<string, object> row = mapper == null ? fileRow.ToDictionary() : mapper(fileRow).ToDictionary();

                    if (!headerWritten)
                    {
                        for (int j = 0; j < row.Count; j++)
                        {
                            // cells start at [1] index not [0] (the magic +1 )
                            ws.Cells[i + 1, j + 1].Value = row.Keys.ElementAt(j);
                        }

                        headerWritten = true;
                    }

                    for (int j = 0; j < row.Count; j++)
                    {
                        // the magic +2 is the position with the header row
                        object value = row.Values.ElementAt(j);
                        if (value != null)
                        {
                            if (value is DateTime time)
                            {
                                ws.Cells[i + 2, j + 1].Value = time.ToString("dd/MM/yyyy hh:mm");
                            }
                            else if (!value.GetType().IsClass || value.GetType().IsSealed)
                            {
                                ws.Cells[i + 2, j + 1].Value = value;
                            }
                        }
                    }
                }

                //Save the new workbook. We haven't specified the filename so use the Save as method.
                excel.Save();
            }
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
            using (ExcelPackage excel = new ExcelPackage(stream))
            {
                ExcelWorksheet ws = excel.Workbook.Worksheets.Add(blankSheetName);

                bool headerWritten = false;

                int rowTotal = fileData.Count();

                ws.InsertRow(1, rowTotal);

                for (int i = 0; i < rowTotal; i++)
                {
                    IDictionary<string, object> row = fileData.ElementAt(i);
                    if (!headerWritten)
                    {
                        for (int j = 0; j < row.Count; j++)
                        {
                            // cells start at [1] index not [0] (the magic +1 )
                            ws.Cells[i + 1, j + 1].Value = row.Keys.ElementAt(j);
                        }

                        headerWritten = true;
                    }

                    for (int j = 0; j < row.Count; j++)
                    {
                        // the magic +2 is the position with the header row
                        object value = row.Values.ElementAt(j);
                        if (value != null)
                        {
                            if (value is DateTime time)
                            {
                                ws.Cells[i + 2, j + 1].Value = time.ToShortDateString();
                            }
                            else if (!value.GetType().IsClass)
                            {
                                ws.Cells[i + 2, j + 1].Value = value;
                            }
                        }
                    }
                }

                //Save the new workbook. We haven't specified the filename so use the Save as method.
                excel.Save();
            }
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
