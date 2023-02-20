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
        String Qrgen,TextQR;
        PictureBox picBox1 = new PictureBox();
        Bitmap getimage;

        static void QUERY_Data(String SQL, String respontext,Bitmap image,PictureBox picBox1)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT.Clear();
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
                MessageBox.Show(respontext + "Fail..");
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
            string spec = textBox1.Text.Trim().Replace("'","").ToString();
            string name = textBox2.Text.Trim().Replace("'","").ToString();
            string key = textBox3.Text.Trim().Replace("'","").ToString();
            try
            {
                QUERY_Data("INSERT INTO TBL_GG_STORE " +
                "(NAME,KEY_MAC,STATUS,USERNAME,QR,ID_SPEC,INS_DT,LAST_UPD) " +
                "SELECT '"+name+"','"+key+"','store','"+ c_VALUES.c_USER + "','"+ TextQR + "',ID_SPEC,GETDATE(),GETDATE() " +
                "FROM TBL_GG_STORE_TYPE WHERE RTRIM(LTRIM(SPEC)) = '"+spec+"' ", "Insert",image,picBox1);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void GEN_QR()
        {
            try
            {
                TextQR = textBox1.Text + "@" + textBox2.Text.Trim() + "@" + textBox3.Text.Trim();
                Qrgen = TextQR.Replace("@", System.Environment.NewLine);
                //Qrgen = text;
                QRCodeGenerator QR = new QRCodeGenerator();
                QRCodeData data = QR.CreateQrCode(TextQR, QRCodeGenerator.ECCLevel.Q);
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
            if (getimage != null)
            {
                PrintDocument p = new PrintDocument();
                p.PrintPage += delegate (object sender1, PrintPageEventArgs e1)
                {
                    e1.Graphics.DrawImage(getimage, 0, 0);
                };
                try
                {
                    p.Print();
                }
                catch (Exception ex)
                {
                    throw new Exception("Exception Occured While Printing", ex);
                }
            }
            else
            {
                MessageBox.Show("Please GenQR or enter value idqr");
            }
        }
    }
}
