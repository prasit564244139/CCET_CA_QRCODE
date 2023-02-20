using QRCoder;
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

namespace CCET_CA_QRCODE
{
    public partial class Form1 : Form
    {
        static DataTable DT = new DataTable();
        static SqlConnection conn = new SqlConnection();
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
            lbl_USER.Text = c_VALUES.c_USER;
            textBox1.Focus();
        }

        private void pic_LOGOUT_Click(object sender, EventArgs e)
        {
            c_VALUES.c_USER = "";
            this.Close();
            th = new Thread(opennewform);
            th.Start();
        }
        
        
        private void opennewform(object obj)
        {
            Application.Run(new f_LOGIN());
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
                DT.Clear();
                QUERY_Data("SELECT * FROM TBL_GG_STORE");
                dataGridView1.DataSource = DT;
        }
    }
}
