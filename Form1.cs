using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using DataTable = System.Data.DataTable;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace ExportSchma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName.Text) || string.IsNullOrEmpty(txtRoute.Text))
            {
                MessageBox.Show("請輸入路徑與檔案名稱");
                return;
            }

            if (!Directory.Exists(txtRoute.Text))
            {
                MessageBox.Show("不合法路徑");
                return;
            }

            string connectionString = txtConnectionString.Text;
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook workbook = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        DataTable tableSchema = connection.GetSchema("Tables");
                        (excelApp, workbook) = InitializeExcelApplication();

                        foreach (DataRow tableRow in tableSchema.Rows)
                        {
                            string tableName = tableRow["TABLE_NAME"].ToString();
                            tableName = TrimStringTo31Chars(tableName);
                            DataTable columnComments = GetColumnComments(connection, tableName);
                            ExportToExcel(workbook, tableName, columnComments);
                        }

                        var FullPath = Path.Combine(txtRoute.Text, txtFileName.Text + ".xlsx");
                        workbook.SaveAs(FullPath);

                        MessageBox.Show("Data exported to Excel successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        CleanupExcelResources(excelApp, workbook);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private (Microsoft.Office.Interop.Excel.Application, Workbook) InitializeExcelApplication()
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application
            {
                Visible = false, // 隱藏 Excel 應用程式
                DisplayAlerts = false // 禁用警告訊息
            };

            Workbook workbook = excelApp.Workbooks.Add();
            return (excelApp, workbook);
        }
        private void CleanupExcelResources(Microsoft.Office.Interop.Excel.Application excelApp, Workbook workbook)
        {
            if (workbook != null)
            {
                workbook.Close(false);
                Marshal.ReleaseComObject(workbook);
                workbook = null;
            }
            if (excelApp != null)
            {
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);
                excelApp = null;
            }
            GC.Collect();
        }
        private DataTable GetColumnComments(SqlConnection connection, string tableName)
        {
            string query = @"
        SELECT
            c.COLUMN_NAME,
            c.DATA_TYPE,
            c.CHARACTER_MAXIMUM_LENGTH,
            ep.value AS COLUMN_DESCRIPTION
        FROM
            INFORMATION_SCHEMA.COLUMNS c
            LEFT JOIN sys.extended_properties ep ON ep.major_id = OBJECT_ID(c.TABLE_NAME) AND ep.minor_id = COLUMNPROPERTY(OBJECT_ID(c.TABLE_NAME), c.COLUMN_NAME, 'ColumnId')
        WHERE
            c.TABLE_NAME = @TableName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TableName", tableName);
            // Create a DataTable to hold the results
            DataTable dataTable = new DataTable();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                dataTable.Load(reader);
            }

            return dataTable;
        }
        private void ExportToExcel(Workbook workbook, string tableName, DataTable dataTable)
        {
            // Add a new worksheet with the table name
            Worksheet worksheet = (Worksheet)workbook.Sheets.Add();
            worksheet.Name = tableName;
            List<string> ColumnName = new List<string> { "欄位名稱", "型別", "長度", "說明" };
            // Write column names to the Excel sheet
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = ColumnName[i];
            }

            // Write data to the Excel sheet
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataTable.Rows[i][j].ToString();
                }
            }

            // Auto-adjust column widths
            AutoFitColumns(worksheet);
        }
        private void AutoFitColumns(Worksheet worksheet)
        {
            // Get the used range of the worksheet
            Range usedRange = worksheet.UsedRange;

            // AutoFit columns for the used range
            usedRange.Columns.AutoFit();

            // Release the used range object to avoid memory leaks
            Marshal.ReleaseComObject(usedRange);
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    txtRoute.Text = folderDialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Sheet只選擇至31字元
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string TrimStringTo31Chars(string input)
        {
            return input.Substring(0, Math.Min(31, input.Length));
        }
    }
}