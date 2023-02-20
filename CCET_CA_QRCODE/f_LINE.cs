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

namespace CCET_CA_QRCODE
{
    public partial class f_LINE : Form
    {
        static DataTable DT = new DataTable();
        static DataTable DT3 = new DataTable();
        static DataTable DT2 = new DataTable();
        static SqlConnection conn = new SqlConnection();
        Image[] images;
        String Qrgen, TextQR;

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
        }

        public void button2_Click(object sender, EventArgs e)
        {
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
                        QRCodeData data = QR.CreateQrCode(TextQR, QRCodeGenerator.ECCLevel.Q);
                        QRCode code = new QRCode(data);
                        //Insert(code.GetGraphic(5), pictureBox2);
                        dtRow[4] = code.GetGraphic(5);
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
                    QUERY_Data("SELECT * FROM TBL_GG_STORELOG WHERE QR = '" + textBox1.Text.Trim().ToString().Replace("'", "") + "'", "SELECT");
                    if (DT.Rows.Count > 0) { dataGridView2.DataSource = DT; }
                    textBox1.Clear();
                    textBox1.Focus();
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //MessageBox.Show(row.Cells["QR"].Value.ToString());
                    GEN_QR(row.Cells["QR"].Value.ToString());
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

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
