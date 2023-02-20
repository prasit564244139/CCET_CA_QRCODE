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
    public partial class f_LOGIN : Form
    {
        static DataTable DT = new DataTable();
        static SqlConnection conn = new SqlConnection();
        static void QUERY_Data(String SQL) { 
            conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
            SqlCommand cmd = new SqlCommand(SQL, conn);
            DT.Clear();
            conn.Open();
            DT.Load(cmd.ExecuteReader());
            conn.Close();
        }

    public f_LOGIN()
        {
            InitializeComponent();
        }

        c_VALUES c_VALUES = new c_VALUES();
        //Thread th;

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter your username");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    textBox2.Focus();
                }
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Please enter your username");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                }
                else
                {
                    QUERY_Data("SELECT * FROM TBL_GG_EMP WHERE USERNAME = '"+textBox1.Text+"'");
                    dataGridView1.Visible = false;
                    //MessageBox.Show(DT.Rows[0]["password"].ToString());
                    if(DT.Rows.Count > 0 && DT.Rows[0]["password"].ToString().Trim() == textBox2.Text)
                    {
                        //c_VALUES c_VALUES = new c_VALUES();
                        //c_VALUES.c_USER = textBox1.Text.Trim();
                        //this.Close();
                        //th = new Thread(opennewform);
                        //th.Start();
                        c_VALUES c_VALUES = new c_VALUES();
                        c_VALUES.c_USER = textBox1.Text.Trim();
                        Form1 fMAIN = new Form1();
                        fMAIN.re_FROM();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password is wrong");
                        textBox2.Text = "";
                        textBox2.Focus();
                    }
                }
                //c_VALUES c_VALUES = new c_VALUES();
                //c_VALUES.c_USER = textBox1.Text.Trim();
                //this.Close();
                //th = new Thread(opennewform);
                //th.Start();
            }
        }

        private void opennewform(object obj)
        {
            Application.Run(new Form1());
        }

        private void f_LOGIN_Load(object sender, EventArgs e)
        {
            QUERY_Data("SELECT * FROM TBL_GG_EMP");
            dataGridView1.DataSource = DT;
        }
    }
}
