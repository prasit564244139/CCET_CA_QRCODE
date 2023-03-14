using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing.Printing;
using Json2DataTable;
using System.Data.SqlClient;

namespace CCET_CA_QRCODE
{
    public partial class f_PO : Form
    {
        public f_PO()
        {
            InitializeComponent();
        }
        static System.Data.DataTable DT_PO = new System.Data.DataTable();
        static SqlConnection conn = new SqlConnection();
        JsonToDataTable j2dt = new JsonToDataTable();
        string SQL;

        private void f_PO_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    SQL = "SELECT * FROM DPCT10 WHERE STICKER_COMPUTER_NAME = '" + textBox2.Text.Trim() + "'";
                    Query_PO("LOA", SQL);
                    if (DT_PO.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = DT_PO;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    textBox2.Text = "";
                    textBox2.Focus();
                }
            }
           
        }

        public void Query_PO(String DB, String SQL)
        {
            try
            {
                DT_PO.Columns.Clear();
                DT_PO.Clear();
                dataGridView1.DataSource = null;
                j2dt.Url = "http://10.51.64.63:8085/service/query";
                j2dt.db = DB;
                j2dt.cmd = SQL;
                DT_PO = j2dt.getData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {

                        SQL = "SELECT * FROM DPCT10 WHERE VCH_NO = '" + textBox1.Text.Trim() + "'";
                        Query_PO("LOA", SQL);
                        if (DT_PO.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = DT_PO;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        textBox1.Text = "";
                        textBox1.Focus();
                    }
                }
            
        }

        private void pic_LOGOUT_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("No data");
            }
            else
            {
                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = false;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Exported from gridview";
                // storing header part in Excel  

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    worksheet.Cells[1, i].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    worksheet.Cells[1, i].VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        worksheet.Cells[i + 2, j + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Cells[i + 2, j + 1].VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    }
                }
                worksheet.Columns.AutoFit();
                app.Visible = true;
                // save the application  
                //workbook.SaveAs("d:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application  
                //app.Quit();
            }
        }
    }
}
