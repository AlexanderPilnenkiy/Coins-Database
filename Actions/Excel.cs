using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Coins_Database.DataAccessLayer;

namespace Coins_Database.Actions
{
    class Excel
    {
        static void Congratulations(XLWorkbook workbook, IXLWorksheet wsDetailedData)
        {
            wsDetailedData.ColumnWidth = 33;
            workbook.SaveAs(@"C:\data.xlsx");
        }

        public static void RatingReport(XLWorkbook workbook, string listname, List<Rating> dataList)
        {
            var wsDetailedData = workbook.AddWorksheet(listname);
            wsDetailedData.Cell(1, 1).InsertTable(dataList);
            wsDetailedData.Cell(1, 1).Value = "№";
            wsDetailedData.Cell(1, 2).Value = "Учитель";
            wsDetailedData.Cell(1, 3).Value = "Рейтинг";
            Congratulations(workbook, wsDetailedData);
        }

        public static void EventsReport(XLWorkbook workbook, string listname, List<Events> dataList)
        {
            var wsDetailedData = workbook.AddWorksheet(listname);
            wsDetailedData.Cell(1, 1).InsertTable(dataList);
            wsDetailedData.Cell(1, 1).Value = "Мероприятие";
            wsDetailedData.Cell(1, 2).Value = "Тип мероприятия";
            wsDetailedData.Cell(1, 3).Value = "Место проведения";
            wsDetailedData.Cell(1, 4).Value = "Дата проведения";
            Congratulations(workbook, wsDetailedData);
        }
    }
}
