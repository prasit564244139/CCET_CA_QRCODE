using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QRCoder;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing.Printing;

namespace CCET_CA_QRCODE
{
    public partial class f_LINE : Form
    {
        static System.Data.DataTable DT = new System.Data.DataTable();
        static System.Data.DataTable DT3 = new System.Data.DataTable();
        static System.Data.DataTable DT2 = new System.Data.DataTable();
        static SqlConnection conn = new SqlConnection();
        Image[] images;
        String Qrgen, TextQR,SQL;

        static void QUERY_Data(String SQL, String respontext)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT.Clear();
                conn.Open();
                DT.Load(cmd.ExecuteReader());

                if (respontext == "Insert")
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        //MessageBox.Show(respontext + " : Sucess..");
                    }
                    else
                    {
                        MessageBox.Show(respontext + "Fail..");
                    }
                }
                else if(respontext == "UPDATE")
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(respontext + " : Sucess..");
                    }
                    else
                    {
                        MessageBox.Show(respontext + "Fail..");
                    }
                }
                conn.Close();
            }
            catch(Exception EX)
            {
                MessageBox.Show("SQL query error please check lan connect or input type.\n" + EX.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        static void QUERY_Data2(String SQL, String respontext)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT2.Clear();
                conn.Open();
                DT2.Load(cmd.ExecuteReader());

                if (respontext == "Insert")
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(respontext + " : Sucess..");
                    }
                    else
                    {
                        MessageBox.Show(respontext + "Fail..");
                    }
                }
                conn.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show("SQL query error please check lan connect or input type.\n" + EX.ToString());
            }
        }

        static void QUERY_Data3(String SQL, String respontext)
        {
            try
            {

                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT3.Clear();
                conn.Open();
                DT3.Load(cmd.ExecuteReader());

                if (respontext == "Insert")
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(respontext + " : Sucess..");
                    }
                    else
                    {
                        MessageBox.Show(respontext + "Fail..");
                    }
                }
                conn.Close();
            }
            catch (Exception EX)
            {
                MessageBox.Show("SQL query error please check lan connect or input type.\n" + EX.ToString());
            }
        }

        public f_LINE()
        {
            InitializeComponent();
        }

        private void f_LINE_Load(object sender, EventArgs e)
        {
            dataGridView1.ForeColor = Color.Black;
            dataGridView2.ForeColor = Color.Black;
            dataGridView3.ForeColor = Color.Black;

            dataGridView3.DataSource = null;
            DT3.Clear();
            DT3.Columns.Clear();
            QUERY_Data3("SELECT RTRIM(LTRIM(NAME)) AS NAME,RTRIM(LTRIM(KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(STATUS)) AS STATUS,RTRIM(LTRIM(QR)) AS QR FROM TBL_GG_STORE", "SELECTDT3");
            DT3.Columns.Add("test", typeof(Bitmap));

            dataGridView3.DataSource = DT3;


            foreach (DataRow dtRow in DT3.Rows)
            {
                // On all tables' columns
                foreach (DataColumn column in DT3.Columns)
                {
                    string ColumnName = column.ColumnName;
                    //MessageBox.Show(column.ColumnName.ToString());
                    if (ColumnName == "QR")
                    {
                        //GEN_QRIMG(dtRow[column].ToString());

                        TextQR = dtRow[column].ToString().Trim();
                        Qrgen = TextQR.Replace("@", System.Environment.NewLine);
                        //Qrgen = text;
                        QRCodeGenerator QR = new QRCodeGenerator();
                        QRCodeData data = QR.CreateQrCode(Qrgen, QRCodeGenerator.ECCLevel.Q);
                        QRCode code = new QRCode(data);
                        //Insert(code.GetGraphic(5), pictureBox2);
                        dtRow[4] = code.GetGraphic(5);
                    }
                }
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //textBox1.Text.Trim().ToString().Replace("'", "");
            try
            {
                if (checkBox1.Checked == true)
                {
                    QUERY_Data("SELECT * FROM TBL_GG_STORE WHERE NAME = '" + textBox1.Text.Trim().ToString().Replace("'", "") + "'", "SELECT");
                    dataGridView1.DataSource = DT;
                    QUERY_Data2("SELECT * FROM TBL_GG_STORELOG WHERE NAME = '" + textBox1.Text.Trim().ToString().Replace("'", "") + "'", "SELECT");
                    dataGridView2.DataSource = DT2;
                    textBox1.Clear();
                    textBox1.Focus();
                }
                else
                {

                    string qrtext = textBox1.Text.Trim().ToString().Replace("'", "");
                    qrtext = qrtext.Replace(Environment.NewLine, "@");
                    QUERY_Data("SELECT * FROM TBL_GG_STORE WHERE QR = '" + qrtext + "'", "SELECT");
                    if (DT.Rows.Count > 0) { dataGridView1.DataSource = DT; textBox2.Text = DT.Rows[0]["STATUS"].ToString(); } else { textBox2.Clear(); }
                    QUERY_Data2("SELECT * FROM TBL_GG_STORELOG WHERE QR = '" + textBox1.Text.Trim().ToString().Replace("'", "") + "'", "SELECT");
                    if (DT.Rows.Count > 0) { dataGridView2.DataSource = DT2; }
                    textBox1.Clear();
                    textBox1.Focus();
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //MessageBox.Show(row.Cells["QR"].Value.ToString());
                    GEN_QR(row.Cells["QR"].Value.ToString());
                    textBox2.Text = row.Cells["STATUS"].Value.ToString().Trim();
                    //More code here
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {

            }
            
        }

        public void GEN_QR(String txtQR)
        {
            try
            {
                //TextQR = textBox1.Text + "@" + textBox2.Text.Trim() + "@" + textBox3.Text.Trim();
                TextQR = txtQR.Trim();
                Qrgen = TextQR.Replace("@", System.Environment.NewLine);
                //Qrgen = text;
                QRCodeGenerator QR = new QRCodeGenerator();
                QRCodeData data = QR.CreateQrCode(Qrgen, QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                //Insert(code.GetGraphic(5), pictureBox2);
                pictureBox2.Image = code.GetGraphic(5);
                //getimage = code.GetGraphic(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void GEN_QRIMG(String txtQR)
        {
            try
            {
                pictureBox2 = new PictureBox();
                //TextQR = textBox1.Text + "@" + textBox2.Text.Trim() + "@" + textBox3.Text.Trim();
                TextQR = txtQR.Trim();
                Qrgen = TextQR.Replace("@", System.Environment.NewLine);
                //Qrgen = text;
                QRCodeGenerator QR = new QRCodeGenerator();
                QRCodeData data = QR.CreateQrCode(TextQR, QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                //Insert(code.GetGraphic(5), pictureBox2);
                pictureBox2.Image = code.GetGraphic(5);
                //getimage = code.GetGraphic(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView3.DataSource == null)
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

                for (int i = 1; i < dataGridView3.Columns.Count + 2; i++)
                {
                    if (i < dataGridView3.Columns.Count + 1)
                    {
                        worksheet.Cells[1, i] = dataGridView3.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        worksheet.Cells[1, i].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    }
                    else
                    {
                       
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView3.Columns.Count; j++)
                    {
                        if ((dataGridView3.Rows[i].Cells[j].Value.ToString().Split('.'))[0] == "System")
                        {

                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1] = dataGridView3.Rows[i].Cells[j].Value.ToString();
                            worksheet.Cells[i + 2, j + 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            worksheet.Cells[i + 2, j + 1].VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                        }
                    }
                   
                    // Add the image to the new column of each row
                    DataGridViewImageCell cell = (DataGridViewImageCell)dataGridView3.Rows[i].Cells["test"];
                    System.Drawing.Bitmap bmp = (System.Drawing.Bitmap)cell.Value;
                    System.Drawing.Image resized = bmp.GetThumbnailImage(110, 110, null, IntPtr.Zero);
                    worksheet.Cells[i + 2, dataGridView3.Columns.Count -1].ColumnWidth = 40;
                    worksheet.Cells[i + 2, dataGridView3.Columns.Count ].ColumnWidth = 15;
                    worksheet.Cells[i + 2, dataGridView3.Columns.Count ].RowHeight = 84;
                    Excel.Range range = (Excel.Range)worksheet.Cells[i + 2, dataGridView3.Columns.Count ];
                    Clipboard.SetImage(resized);
                    worksheet.Paste(range, resized);
                    // wait for the paste operation to complete
                    while (app.CutCopyMode != 0)
                    {
                        System.Threading.Thread.Sleep(100);
                    }


                }
                app.Visible = true;


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //MessageBox.Show(row.Cells["NAME"].Value.ToString());
                String SN = row.Cells["NAME"].Value.ToString().Trim();
                SQL = "INSERT INTO TBL_GG_STORELOG (NAME,KEY_MAC,STATUS,USERNAME,QR,ID_SPEC,INS_DT,LAST_UPD) ";
                SQL += "SELECT RTRIM(LTRIM(NAME)) AS NAME,RTRIM(LTRIM(KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(STATUS)) AS STATUS,RTRIM(LTRIM(USERNAME)) AS USERNAME,RTRIM(LTRIM(QR)) AS QR,RTRIM(LTRIM(ID_SPEC)) AS ID_SPEC,RTRIM(LTRIM(INS_DT)) AS INS_DT,GETDATE() ";
                SQL += "FROM TBL_GG_STORE  ";
                SQL += "WHERE RTRIM(LTRIM(NAME)) = '" + SN + "' ";
                QUERY_Data(SQL, "Insert");
                SQL = "UPDATE TBL_GG_STORE ";
                SQL += "SET STATUS = '"+textBox2.Text.Trim()+"' ";
                SQL += "WHERE NAME = '" + SN + "' ";
                QUERY_Data(SQL, "UPDATE");
                textBox2.Text = "";
                textBox1.Text = "";
                QUERY_Data("SELECT * FROM TBL_GG_STORE WHERE NAME = '" + SN + "'", "SELECT");
                dataGridView1.DataSource = DT;
                QUERY_Data2("SELECT * FROM TBL_GG_STORELOG WHERE NAME = '" + SN + "'", "SELECT");
                dataGridView2.DataSource = DT2;
                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //    //MessageBox.Show(row.Cells["QR"].Value.ToString());
                //    GEN_QR(row.Cells["QR"].Value.ToString());
                //    //More code here
                //}

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    //MessageBox.Show(row.Cells["QR"].Value.ToString());
                    GEN_QR(dr.Cells["QR"].Value.ToString());
                    textBox2.Text = dr.Cells["STATUS"].Value.ToString().Trim();
                    //More code here
                }
                button2.PerformClick();
                textBox1.Focus();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    e1.Graphics.DrawImage(pictureBox2.Image, 0, 0);
                };
                try
                {
                    string defaultPrinterName = new PrinterSettings().PrinterName;
                    if(defaultPrinterName == "Microsoft Print to PDF")
                    {
                        SaveFileDialog dialog1 = new SaveFileDialog();
                        dialog1.Title = "Save file as...";
                        dialog1.Filter = "All files (*.pdf)|*.pdf|All files (*.*)|*.*";
                        dialog1.FileName = dataGridView1.Rows[0].Cells[1].Value.ToString().Trim();
                        dialog1.RestoreDirectory = true;
                        if (dialog1.ShowDialog() == DialogResult.OK)
                        {
                            MessageBox.Show(dialog1.FileName);
                        }
                    }
                    else
                    {
                        p.Print();

                    }

                }
                catch
                {
                    //throw new Exception("Exception Occured While Printing", ex.ToString());
                    MessageBox.Show("Print Error.");
                }
            }
            else
            {
                MessageBox.Show("Please GenQR or enter value idqr");
            }
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            DataView dv = DT3.DefaultView;
            dv.RowFilter = string.Format("NAME like '%{0}%'", textBox3.Text.Trim());
            dataGridView3.DataSource = dv;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}
