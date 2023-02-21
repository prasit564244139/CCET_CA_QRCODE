using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;

namespace CCET_CA_QRCODE
{
    public partial class f_QRCODE : Form
    {
        static DataTable DT = new DataTable();
        static SqlConnection conn = new SqlConnection();
        String Qrgen,TextQR,SQL;
        PictureBox picBox1 = new PictureBox();
        Bitmap getimage;

        static void QUERY_Data(String SQL, String respontext,Bitmap image,PictureBox picBox1)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT.Clear();
                DT.Columns.Clear();
                conn.Open();

                if (respontext == "Insert")
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show(respontext + " : Sucess..");
                        if(image != null)
                        {
                            Bitmap getimage = image;
                            picBox1.Image = image;
                        }
                    }
                    else
                    {
                        MessageBox.Show(respontext + "Fail..");
                        if (image == null)
                        {
                            Bitmap getimage = image;
                            picBox1.Image = null;
                        }
                    }
                }
                else
                {
                    DT.Load(cmd.ExecuteReader());
                }                
            }        
            catch (Exception EX)
            {
                MessageBox.Show(respontext + "Fail..",EX.ToString());
                //MessageBox.Show("SQL query error please check lan connect or input type.\n" + EX.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public f_QRCODE()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            //string text = textBox1.Text+"@"+textBox2.Text.Trim()+"@"+textBox2.Text.Trim();

            //text = text.Replace("@", System.Environment.NewLine);
            //QRCodeGenerator QR = new QRCodeGenerator();
            //QRCodeData data = QR.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            //QRCode code = new QRCode(data);
            //pictureBox1.Image = code.GetGraphic(5);
        }

        private void f_QRCODE_Load(object sender, EventArgs e)
        {
            //tx.Items.Add("555555");
            comboBox1.Items.Add("WINDOWN_XP");
            comboBox1.Items.Add("WINDOWN_7");
            comboBox1.Items.Add("WINDOWN_10");
            comboBox1.Items.Add("WINDOWN_11");
            SQL = "SELECT DISTINCT RTRIM(LTRIM(SPEC)) AS SPEC,RTRIM(LTRIM(RAM)) AS RAM FROM TBL_GG_STORE_TYPE ORDER BY SPEC";
            QUERY_Data(SQL, "SELECT", null, null);
            foreach (DataRow dtRow in DT.Rows)
            {
                comboBox2.Items.Add(dtRow["SPEC"].ToString()+"@"+ dtRow["RAM"].ToString());
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                GEN_QR();
                //Insert();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GEN_QR();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Insert(Bitmap image,PictureBox picBox1)
        {
            //string spec = comboBox2.Text.Trim().Replace("'","").ToString();
            string spec = comboBox2.Text.Substring(0, comboBox2.Text.IndexOf('@')).Trim();
            string[] sAry = comboBox2.Text.Split('@');
            string name = textBox2.Text.Trim().Replace("'","").ToString();
            string key = comboBox1.Text.Trim().Replace("'","").ToString();
            try
            {
                QUERY_Data("INSERT INTO TBL_GG_STORE " +
                "(NAME,KEY_MAC,STATUS,USERNAME,QR,ID_SPEC,INS_DT,LAST_UPD) " +
                "SELECT '"+name+"','"+key+"','store','"+ c_VALUES.c_USER + "','"+ TextQR + "',ID_SPEC,GETDATE(),GETDATE() " +
                "FROM TBL_GG_STORE_TYPE WHERE RTRIM(LTRIM(SPEC)) = '"+spec+ "' AND RTRIM(LTRIM(RAM)) = '"+ sAry[1].Trim() + "' ", "Insert",image,picBox1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = null;
                picBox1.Image = null;
                textBox2.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(textBox2.Text.Trim()))
            {
                comboBox2.Focus();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Focus();
        }

        private void GEN_QR()
        {
            try
            {
                TextQR = textBox2.Text.Trim() + "@" + comboBox2.Text.Trim() + "@" + comboBox1.Text.Trim();
                Qrgen = TextQR.Replace("@", System.Environment.NewLine);
                //Qrgen = text;
                QRCodeGenerator QR = new QRCodeGenerator();
                QRCodeData data = QR.CreateQrCode(Qrgen, QRCodeGenerator.ECCLevel.Q);
                QRCode code = new QRCode(data);
                Insert(code.GetGraphic(5),pictureBox1);
                //pictureBox1.Image = code.GetGraphic(5);
                //getimage = code.GetGraphic(5);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    e1.Graphics.DrawImage(pictureBox1.Image, 0, 0);
                };
                try
                {
                    p.Print();
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
    }
}
