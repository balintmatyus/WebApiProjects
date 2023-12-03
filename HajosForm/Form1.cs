using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace HajosForm
{
    public partial class Form1 : Form
    {
        Excel.Application xlApp; // A Microsoft Excel alkalmaz�s
        Excel.Workbook xlWB;     // A l�trehozott munkaf�zet
        Excel.Worksheet xlSheet; // Munkalap a munkaf�zeten bel�l

        Models.HajosContext hajosContext = new Models.HajosContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Excel elind�t�sa �s az applik�ci� objektum bet�lt�se
                xlApp = new Excel.Application();

                // �j munkaf�zet
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                // �j munkalap
                xlSheet = xlWB.ActiveSheet;

                // T�bla l�trehoz�sa
                CreateTable(); // Ennek meg�r�sa a k�vetkez� feladatr�szben k�vetkezik

                // Control �tad�sa a felhaszn�l�nak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezel�s a be�p�tett hiba�zenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba eset�n az Excel applik�ci� bez�r�sa automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        void CreateTable()
        {
            string[] fejl�cek = new string[] {
                "K�rd�s",
                "1. v�lasz",
                "2. v�lasz",
                "3. v�lasz",
                "Helyes v�lasz",
                "k�p"
            };

            for (int i = 0; i < fejl�cek.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = fejl�cek[i];
            }

            var mindenK�rd�s = hajosContext.Questions.ToList();
            object[,] adatT�mb = new object[mindenK�rd�s.Count(), fejl�cek.Count()];

            for (int i = 0; i < mindenK�rd�s.Count(); i++)
            {
                adatT�mb[i, 0] = mindenK�rd�s[i].Question1;
                adatT�mb[i, 1] = mindenK�rd�s[i].Answer1;
                adatT�mb[i, 2] = mindenK�rd�s[i].Answer2;
                adatT�mb[i, 3] = mindenK�rd�s[i].Answer3;
                adatT�mb[i, 4] = mindenK�rd�s[i].CorrectAnswer;
                adatT�mb[i, 5] = mindenK�rd�s[i].Image;
            }

            int sorokSz�ma = adatT�mb.GetLength(0);
            int oszlopokSz�ma = adatT�mb.GetLength(1);

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSz�ma, oszlopokSz�ma);
            adatRange.Value2 = adatT�mb;

            adatRange.Columns.AutoFit();

            Excel.Range fejll�cRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);
            fejll�cRange.Font.Bold = true;
            fejll�cRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejll�cRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            fejll�cRange.EntireColumn.AutoFit();
            fejll�cRange.RowHeight = 40;
            fejll�cRange.Interior.Color = Color.Fuchsia;
            fejll�cRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Start Excel and get Application object.
            var excelApp = new Excel.Application();
            excelApp.Visible = true;

            // Create a new, empty workbook and add a worksheet.
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            // Add data to cells of the first worksheet in the new workbook.
            worksheet.Cells[1, "A"] = "Data Set";
            worksheet.Cells[1, "B"] = "Value";
            worksheet.Cells[2, "A"] = "Point 1";
            worksheet.Cells[2, "B"] = 10;
            worksheet.Cells[3, "A"] = "Point 2";
            worksheet.Cells[3, "B"] = 20;
            worksheet.Cells[4, "A"] = "Point 3";
            worksheet.Cells[4, "B"] = 30;
            worksheet.Cells[5, "A"] = "Point 4";
            worksheet.Cells[5, "B"] = 40;

            // Create a range for the data.
            Excel.Range chartRange = worksheet.get_Range("A1", "B5");

            // Add a chart to the worksheet.
            Excel.ChartObjects xlCharts = (Excel.ChartObjects)worksheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage = myChart.Chart;

            // Set chart range.
            chartPage.SetSourceData(chartRange);

            // Set chart properties.
            chartPage.ChartType = Excel.XlChartType.xlLine;
            chartPage.ChartWizard(Source: chartRange,
                Title: "Example Chart",
                CategoryTitle: "Data Set",
                ValueTitle: "Value");

            // Save the workbook and quit Excel.
            //workbook.SaveAs(@"C:\YourPath\ExcelChartExample.xlsx");
            //excelApp.Quit();
        }
    }
}
