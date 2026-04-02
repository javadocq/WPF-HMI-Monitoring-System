using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using WPF_MES_Monitoring_System.Model;

namespace WPF_MES_Monitoring_System.ViewModel.Service
{
    public class SaveExcelService
    {
        public void GenerateExcel(List<MachineLog> logs)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"MachineLogs_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Today Machine Logs");



                    // 헤더 먼저 작성
                    string[] headers = { "Timestamp", "Machine Name", "Status", "Log Message", "Temperature", "Pressure", "Response Time (ms)" };
                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = headers[i];
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                        worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                        worksheet.Cell(1, i + 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    int currentRow = 2; // 데이터는 2행부터 시작
                    foreach (var log in logs)
                    {
                        worksheet.Cell(currentRow, 1).Value = log.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cell(currentRow, 2).Value = log.MachineName;
                        worksheet.Cell(currentRow, 3).Value = log.Status;
                        worksheet.Cell(currentRow, 4).Value = log.LogMessage;
                        worksheet.Cell(currentRow, 5).Value = log.Temperature;
                        worksheet.Cell(currentRow, 6).Value = log.Pressure;
                        worksheet.Cell(currentRow, 7).Value = log.ResponseTime;
                        currentRow++;
                    }


                    // 컬럼 너비 자동 조절
                    worksheet.Columns().AdjustToContents();

                    // 워크북 저장
                    workbook.SaveAs(saveFileDialog.FileName);
                }
            }
        }
    }
}
