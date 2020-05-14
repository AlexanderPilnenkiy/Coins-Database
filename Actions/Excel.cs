using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ClosedXML.Excel;
using Coins_Database.DataAccessLayer;

namespace Coins_Database.Actions
{
    class Excel
    {
        public static bool SaveReport(XLWorkbook workbook)
        {
            if (workbook.Worksheets.Count > 0)
            {
                Stream myStream;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myStream.Close();
                    }
                    workbook.SaveAs(saveFileDialog1.FileName);
                    MessageBox.Show("Отчёт успешно сформирован");
                }
                return true;
            }
            else
            {
                MessageBox.Show("Нужно выбрать хотя бы один пункт для формирования отчёта");
                return false;
            }
        }

        public static void RatingReport(XLWorkbook workbook, string listname, List<Rating> dataList)
        {
            var wsDetailedData = workbook.AddWorksheet(listname);
            wsDetailedData.ColumnWidth = 33;
            wsDetailedData.Cell(1, 1).InsertTable(dataList);
            wsDetailedData.Cell(1, 1).Value = "№";
            wsDetailedData.Cell(1, 2).Value = "Учитель";
            wsDetailedData.Cell(1, 3).Value = "Рейтинг";
            wsDetailedData.ColumnWidth = 33;
        }

        public static void EventsReport(XLWorkbook workbook, string listname, List<Events> dataList)
        {
            var wsDetailedData = workbook.AddWorksheet(listname);
            wsDetailedData.ColumnWidth = 33;
            wsDetailedData.Cell(1, 1).InsertTable(dataList);
            wsDetailedData.Cell(1, 1).Value = "Мероприятие";
            wsDetailedData.Cell(1, 2).Value = "Тип мероприятия";
            wsDetailedData.Cell(1, 3).Value = "Место проведения";
            wsDetailedData.Cell(1, 4).Value = "Дата проведения";
            wsDetailedData.ColumnWidth = 33;
        }
    }
}
