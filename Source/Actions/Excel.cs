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
        public static bool SaveReport(XLWorkbook Workbook)
        {
            if (Workbook.Worksheets.Count > 0)
            {
                Stream MyStream;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

                SaveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
                SaveFileDialog1.FilterIndex = 2;
                SaveFileDialog1.RestoreDirectory = true;

                if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((MyStream = SaveFileDialog1.OpenFile()) != null)
                    {
                        MyStream.Close();
                    }
                    Workbook.SaveAs(SaveFileDialog1.FileName);
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

        public static void RatingReport(XLWorkbook Workbook, string Listname, List<Rating> DataList)
        {
            var WsDetailedData = Workbook.AddWorksheet(Listname);
            WsDetailedData.ColumnWidth = 33;
            WsDetailedData.Cell(1, 1).InsertTable(DataList);
            WsDetailedData.Cell(1, 1).Value = "№";
            WsDetailedData.Cell(1, 2).Value = "Учитель";
            WsDetailedData.Cell(1, 3).Value = "Рейтинг";
            WsDetailedData.ColumnWidth = 33;
        }

        public static void EventsReport(XLWorkbook Workbook, string Listname, List<Events> DataList)
        {
            var WsDetailedData = Workbook.AddWorksheet(Listname);
            WsDetailedData.ColumnWidth = 33;
            WsDetailedData.Cell(1, 1).InsertTable(DataList);
            WsDetailedData.Cell(1, 1).Value = "Мероприятие";
            WsDetailedData.Cell(1, 2).Value = "Тип мероприятия";
            WsDetailedData.Cell(1, 3).Value = "Место проведения";
            WsDetailedData.Cell(1, 4).Value = "Дата проведения";
            WsDetailedData.ColumnWidth = 33;
        }
    }
}
