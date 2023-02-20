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

namespace CCET_CA_QRCODE
{
    public partial class f_LINE : Form
    {
        static DataTable DT = new DataTable();
        static DataTable DT2 = new DataTable();
        static SqlConnection conn = new SqlConnection();
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

        public f_LINE()
        {
            InitializeComponent();
        }

        private void f_LINE_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            QUERY_Data2("SELECT * FROM TBL_GG_STORE", "SELECTDT3");
            dataGridView3.DataSource = DT2;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //textBox1.Text.Trim().ToString().Replace("'", "");
            string qrtext = textBox1.Text.Trim().ToString().Replace("'", "");
            qrtext = qrtext.Replace(Environment.NewLine, "@");
            QUERY_Data("SELECT * FROM TBL_GG_STORE WHERE QR = '" + qrtext + "'", "SELECT");
            if (DT.Rows.Count > 0) { dataGridView1.DataSource = DT; textBox2.Text = DT.Rows[0]["STATUS"].ToString(); } else { textBox2.Clear(); }
            QUERY_Data("SELECT * FROM TBL_GG_STORELOG WHERE QR = '" + textBox1.Text.Trim().ToString().Replace("'", "") + "'", "SELECT");
            if (DT.Rows.Count > 0) { dataGridView2.DataSource = DT; }
            textBox1.Clear();
            textBox1.Focus();
        }
    }
}
