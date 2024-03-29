﻿using System;
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
using Json2DataTable;

namespace CCET_CA_QRCODE
{
    public partial class f_LINE : Form
    {
        static System.Data.DataTable DT = new System.Data.DataTable();
        static System.Data.DataTable DT3 = new System.Data.DataTable();
        static System.Data.DataTable DT2 = new System.Data.DataTable();
        static System.Data.DataTable DT_PO = new System.Data.DataTable();
        static SqlConnection conn = new SqlConnection();
        JsonToDataTable j2dt = new JsonToDataTable();
        Image[] images;
        String Qrgen, TextQR,SQL;

        static void QUERY_Data(String SQL, String respontext)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT.Columns.Clear();
                DT.Clear();
                conn.Close();
                conn.Open();
                //DT.Load(cmd.ExecuteReader());

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
                else if (respontext == "DELETE")
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        //MessageBox.Show(respontext + " : Sucess..");
                    }
                    else
                    {
                        //MessageBox.Show(respontext + "Fail..");
                    }
                }else if (respontext == "SELECT")
                {
                    DT.Load(cmd.ExecuteReader());
                    if (DT.Rows.Count > 0)
                    {
                        //MessageBox.Show(respontext + " : Sucess..");
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
            comboBox1.Items.Clear();
            SQL = "SELECT RTRIM(LTRIM(LOCATION)) AS LOCATION FROM TBL_GG_LOCATION ";
            QUERY_Data(SQL, "SELECT");
            foreach (DataRow dtRow in DT.Rows)
            {
                comboBox1.Items.Add(dtRow["LOCATION"].ToString());
            }
            dataGridView1.ForeColor = Color.Black;
            dataGridView2.ForeColor = Color.Black;
            dataGridView3.ForeColor = Color.Black;

            dataGridView3.DataSource = null;
            DT3.Clear();
            DT3.Columns.Clear();
            QUERY_Data3("SELECT RTRIM(LTRIM(NAME)) AS NAME,RTRIM(LTRIM(KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(MODEL)) AS MODEL,RTRIM(LTRIM(STATUS)) AS STATUS,RTRIM(LTRIM(QR)) AS QR FROM TBL_GG_STORE", "SELECTDT3");
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
                        dtRow[5] = code.GetGraphic(5);
                    }
                }
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            re_all();

        }
        public void re_all()
        {
            dataGridView3.DataSource = null;
            DT3.Clear();
            DT3.Columns.Clear();
            QUERY_Data3("SELECT RTRIM(LTRIM(NAME)) AS NAME,RTRIM(LTRIM(KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(MODEL)) AS MODEL,RTRIM(LTRIM(STATUS)) AS STATUS,RTRIM(LTRIM(QR)) AS QR FROM TBL_GG_STORE", "SELECTDT3");
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
                        dtRow[5] = code.GetGraphic(5);
                    }
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //textBox1.Text.Trim().ToString().Replace("'", "");
            try
            {
                txt_PO.Text = "";
                if (checkBox1.Checked == true)
                {
                    QUERY_Data("SELECT * FROM TBL_GG_STORE WHERE NAME = '" + textBox1.Text.Trim().ToString().Replace("'", "") + "'", "SELECT");
                    dataGridView1.DataSource = DT;
                    QUERY_Data2("SELECT * FROM TBL_GG_STORELOG WHERE NAME = '" + textBox1.Text.Trim().ToString().Replace("'", "") + "'", "SELECT");
                    dataGridView2.DataSource = DT2;
                    GET_PO(textBox1.Text.Trim().ToString().Replace("'", ""));
                    textBox1.Clear();
                    textBox1.Focus();
                    
                }
                else
                {

                    string qrtext = textBox1.Text.Trim().ToString().Replace("'", "");
                    qrtext = qrtext.Replace(Environment.NewLine, "@");
                    QUERY_Data("SELECT * FROM TBL_GG_STORE WHERE QR = '" + qrtext + "'", "SELECT");
                    if (DT.Rows.Count > 0) {
                        dataGridView1.DataSource = DT;
                        comboBox1.Text = DT.Rows[0]["STATUS"].ToString();
                    } else {
                        comboBox1.Text = "";
                    }
                    QUERY_Data2("SELECT * FROM TBL_GG_STORELOG WHERE QR = '" + textBox1.Text.Trim().ToString().Replace("'", "") + "'", "SELECT");
                    if (DT.Rows.Count > 0) {
                        dataGridView2.DataSource = DT2;
                    }
                    GET_PO(DT.Rows[0]["NAME"].ToString());
                    textBox1.Clear();
                    textBox1.Focus();
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //MessageBox.Show(row.Cells["QR"].Value.ToString());
                    GEN_QR(row.Cells["QR"].Value.ToString());
                    comboBox1.Text = row.Cells["STATUS"].Value.ToString().Trim();
                    txt_MODEL.Text = row.Cells["MODEL"].Value.ToString().Trim();
                    txt_NAME.Text = row.Cells["NAME"].Value.ToString().Trim();
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

        public void GET_PO(String NAME_PC)
        {
            try
            {
                SQL = "SELECT * FROM DPCT10 WHERE STICKER_COMPUTER_NAME = '"+ NAME_PC.Trim()+ "'";
                Query_DT("LOA", SQL);
                if (DT_PO.Rows.Count > 0)
                {
                    txt_PO.Text = DT_PO.Rows[0][0].ToString().Trim();
                }
                else
                {
                    txt_PO.Text = "NOT PO";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Query_DT(String DB, String SQL)
        {
            //Json2DataTable j2dt = New Json2DataTable.JsonToDataTable();
            try
            {
                //dataGridView1.DataSource = null;
                j2dt.Url = "http://10.51.64.63:8085/service/query";
                j2dt.db = DB;
                j2dt.cmd = SQL;
                //dataGridView1.DataSource = j2dt.getData();
                DT_PO = j2dt.getData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void GEN_QR(String txtQR)
        {
            try
            {
                //TextQR = textBox1.Text + "@" + comboBox1.Text.Trim() + "@" + textBox3.Text.Trim();
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
                //TextQR = textBox1.Text + "@" + comboBox1.Text.Trim() + "@" + textBox3.Text.Trim();
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
                SQL = "INSERT INTO TBL_GG_STORELOG (NAME,KEY_MAC,STATUS,USERNAME,QR,INS_DT,LAST_UPD,MODEL) ";
                SQL += "SELECT  RTRIM(LTRIM(NAME)) AS NAME,RTRIM(LTRIM(KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(STATUS)) AS STATUS,RTRIM(LTRIM(USERNAME)) AS USERNAME,RTRIM(LTRIM(QR)) AS QR,RTRIM(LTRIM(INS_DT)) AS INS_DT,GETDATE(),RTRIM(LTRIM(MODEL)) AS MODEL ";
                SQL += "FROM TBL_GG_STORE  ";
                SQL += "WHERE RTRIM(LTRIM(NAME)) = '" + SN + "' ";
                QUERY_Data(SQL, "Insert");
                SQL = "UPDATE TBL_GG_STORE ";
                SQL += "SET STATUS = '"+comboBox1.Text.Trim()+"',MODEL = '"+ txt_MODEL.Text.Trim() + "' ,NAME = '"+txt_NAME.Text.Trim()+"' ";
                SQL += "WHERE NAME = '" + SN + "' ";
                QUERY_Data(SQL, "UPDATE");
                comboBox1.Text = "";
                textBox1.Text = "";
                QUERY_Data("SELECT * FROM TBL_GG_STORE WHERE NAME = '" + txt_NAME.Text.Trim() + "'", "SELECT");
                dataGridView1.DataSource = DT;
                QUERY_Data2("SELECT * FROM TBL_GG_STORELOG WHERE NAME = '" + txt_NAME.Text.Trim() + "'", "SELECT");
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
                    comboBox1.Text = dr.Cells["STATUS"].Value.ToString().Trim();
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Confirm Delete ??", "Dialog Box Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        String SN = row.Cells["NAME"].Value.ToString().Trim();
                        SQL = "DELETE FROM TBL_GG_STORE WHERE NAME = '" + SN + "'";
                        QUERY_Data(SQL, "DELETE");
                        SQL = "DELETE FROM TBL_GG_STORELOG WHERE NAME = '" + SN + "'";
                        QUERY_Data(SQL, "DELETE");
                        pictureBox2.Image = null;
                        dataGridView1.DataSource = null;
                        dataGridView2.DataSource = null;
                        comboBox1.Text = "";
                        txt_MODEL.Text = "";
                        re_all();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
            
        private void pic_print_Click(object sender, EventArgs e)
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
                    if (defaultPrinterName == "Microsoft Print to PDF")
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                f_LAN f_LAN = new f_LAN();
                f_LAN.ShowDialog();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}
