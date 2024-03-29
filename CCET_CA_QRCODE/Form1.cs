﻿using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using Json2DataTable;


namespace CCET_CA_QRCODE
{
    public partial class Form1 : Form
    {
        static System.Data.DataTable DT = new System.Data.DataTable();
        static System.Data.DataTable DT_PO = new System.Data.DataTable();
        static SqlConnection conn = new SqlConnection();
        JsonToDataTable j2dt = new JsonToDataTable();
        static string SQL;
        static void QUERY_Data(String SQL)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT.Clear();
                conn.Open();
                DT.Load(cmd.ExecuteReader());
                cmd.ExecuteReader().NextResult();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("SQL query error please check lan connect or input type.\n");
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        c_VALUES c_VALUES = new c_VALUES();
        Thread th;

        private void btnQR_Click(object sender, EventArgs e)
        {
            f_QRCODE F_QR = new f_QRCODE();
            F_QR.ShowDialog();
            
        }

        private void btTYPE_Click(object sender, EventArgs e)
        {
            f_TYPE f_TYPE = new f_TYPE();
            f_TYPE.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f_LINE f_LINE = new f_LINE();
            f_LINE.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(c_VALUES.c_USER))
            {
                lbl_USER.Text = c_VALUES.c_USER;
                DT.Clear();
                SQL = "SELECT RTRIM(LTRIM(A.NAME)) AS NAME,RTRIM(LTRIM(A.KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(A.STATUS)) AS STATUS, ";
                SQL += "RTRIM(LTRIM(A.LAST_UPD)) AS LAST_UPD ";
                SQL += "FROM TBL_GG_STORE A";
                //SQL += "WHERE A.ID_SPEC = B.ID_SPEC ";
                QUERY_Data(SQL);
                dataGridView1.DataSource = DT;
                textBox1.Focus();

            }
            else
            {
                f_LOGIN f_LOGIN = new f_LOGIN();
                f_LOGIN.ShowDialog();
                re_FROM();
            }

        }

        public void re_FROM()
        {
            try
            {
                if (!string.IsNullOrEmpty(c_VALUES.c_USER))
                {
                    lbl_USER.Text = c_VALUES.c_USER;
                    DT.Clear();
                    if (DT.Columns.Count > 0)
                    {
                        DT.Columns.Clear();
                    }
                    //DT.Columns.Add("PO", typeof(string));
                    SQL = "SELECT RTRIM(LTRIM(A.NAME)) AS NAME,RTRIM(LTRIM(A.KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(A.STATUS)) AS STATUS,RTRIM(LTRIM(A.MODEL)) AS MODEL, ";
                    SQL += "RTRIM(LTRIM(A.QR)) AS QR ";
                    SQL += "FROM TBL_GG_STORE A";
                    //SQL += "WHERE A.ID_SPEC = B.ID_SPEC "; ,RTRIM(LTRIM(A.LAST_UPD)) AS LAST_UPD
                    QUERY_Data(SQL);
                    //DT.Columns.Add("PO", typeof(string));
                    DT.Columns.Add("PROCESS", typeof(string));
                    DT.Columns.Add("RAM", typeof(string));
                    
                    foreach (DataRow dtRow in DT.Rows)
                    {
                        //GET_PO(dtRow["NAME"].ToString().Trim());
                        string[] sAry = dtRow["QR"].ToString().Split('@');
                        //MessageBox.Show(sAry[0]);
                        dtRow["PROCESS"] = sAry[1].ToString();
                        dtRow["RAM"] = sAry[2].ToString();
                        //dtRow["PO"] = GET_PO("aa");
                    }
                    dataGridView1.DataSource = DT;
                    dataGridView1.Columns["QR"].Visible = false;
                    
                    textBox1.Focus();
                }
                else
                {
                    System.Windows.Forms.Application.Exit();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string GET_PO(string PC_NAME)
        {
            try
            {
                String STICKER;
                SQL = "SELECT STICKER_COMPUTER_NAME FROM DPCT10 WHERE STICKER_COMPUTER_NAME = '"+ PC_NAME.Trim()+ "' ";
                Query_PO("LOA",SQL);
                if (DT_PO.Rows.Count > 0)
                {
                    STICKER = DT_PO.Rows[0][0].ToString().Trim();
                }
                else
                {
                    STICKER = "NOT DATA";
                }
                return STICKER;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "ERROR";
            }
        }

        public void Query_PO(String DB, String SQL)
        {
            //Json2DataTable j2dt = New Json2DataTable.JsonToDataTable();
            try
            {
                dataGridView1.DataSource = null;
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

        private void pic_LOGOUT_Click(object sender, EventArgs e)
        {
            c_VALUES.c_USER = "";
            lbl_USER.Text = c_VALUES.c_USER;
            f_LOGIN f_LOGIN = new f_LOGIN();
            f_LOGIN.ShowDialog();
            re_FROM();
        }
        
        
        private void opennewform(object obj)
        {
            System.Windows.Forms.Application.Run(new f_LOGIN());
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Text = textBox1.Text.Replace("'", "");
                try
                {
                    DT.Clear();
                    QUERY_Data("SELECT * FROM TBL_GG_STORE WHERE QR = '" + textBox1.Text.Trim() + "'");
                    dataGridView1.DataSource = DT;
                    textBox1.Clear();
                }
                catch (Exception ex)
                {
                    textBox1.Clear();
                    MessageBox.Show("SQL query error please check lan connect or input type.\n code : " + ex.ToString());
                }
                if (DT.Rows.Count > 0)
                {
                    dataGridView1.DataSource = DT;
                }

            }

        }

        private void btn_SHOW_ALL_Click(object sender, EventArgs e)
        {
            DT.Columns.Clear();
            DT.Clear();
            //SQL = "SELECT RTRIM(LTRIM(A.NAME)) AS NAME,RTRIM(LTRIM(A.KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(A.STATUS)) AS STATUS, ";
            //SQL += "RTRIM(LTRIM(A.QR)) AS QR,RTRIM(LTRIM(A.LAST_UPD)) AS LAST_UPD ";
            //SQL += "FROM TBL_GG_STORE A";
            //QUERY_Data(SQL);
            //dataGridView1.DataSource = DT;
            re_FROM();
        }

        private void btn_EXCEL_Click(object sender, EventArgs e)
        {
            if(dataGridView1.DataSource == null)
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
                    worksheet.Cells[1, i].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    worksheet.Cells[1, i].VerticalAlignment = XlHAlign.xlHAlignCenter;

                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        worksheet.Cells[i + 2, j + 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        worksheet.Cells[i + 2, j + 1].VerticalAlignment = XlHAlign.xlHAlignCenter;
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                f_STATUS f_STATUS = new f_STATUS();
                f_STATUS.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_PO_Click(object sender, EventArgs e)
        {
            try
            {
                f_PO f_PO = new f_PO();
                f_PO.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
