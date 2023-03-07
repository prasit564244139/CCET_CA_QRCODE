using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CCET_CA_QRCODE
{
    public partial class f_STATUS : Form
    {
        public f_STATUS()
        {
            InitializeComponent();
        }
        static DataTable DT = new DataTable();
        static SqlConnection conn = new SqlConnection();
        static String SQL;

        static void QUERY_Data(String SQL, String respontext)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT.Clear();
                conn.Open();
                if (respontext == "INSERT")
                {
                    int respon = cmd.ExecuteNonQuery();
                    if (respon > 0)
                    {
                        MessageBox.Show(respontext + " : Sucess..");
                    }
                    else
                    {
                        MessageBox.Show("Fail..");
                    }
                }
                else if(respontext == "DELETE")
                {
                    int respon = cmd.ExecuteNonQuery();
                    if (respon > 0)
                    {
                        MessageBox.Show(respontext + " : Sucess..");
                    }
                    else
                    {
                        MessageBox.Show("Fail..");
                    }
                }
                else
                {
                    DT.Load(cmd.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL query error please check lan connect or input type.\n" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }


        private void f_STATUS_Load(object sender, EventArgs e)
        {
            try
            {
                SQL = "SELECT RTRIM(LTRIM(LOCATION)) AS NAME FROM TBL_GG_LOCATION";
                QUERY_Data(SQL,"SELECT");
                dataGridView1.DataSource = DT;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    SQL = "INSERT INTO TBL_GG_LOCATION (LOCATION) VALUES('" + textBox1.Text.Trim() + "')";
                    QUERY_Data(SQL, "INSERT");
                    SQL = "SELECT RTRIM(LTRIM(LOCATION)) AS NAME FROM TBL_GG_LOCATION";
                    QUERY_Data(SQL, "SELECT");
                    dataGridView1.DataSource = DT;
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                SQL = "DELETE FROM TBL_GG_LOCATION WHERE LOCATION = '" + textBox1.Text.Trim() +"'";
                QUERY_Data(SQL, "DELETE");
                SQL = "SELECT RTRIM(LTRIM(LOCATION)) AS NAME FROM TBL_GG_LOCATION";
                QUERY_Data(SQL, "SELECT");
                dataGridView1.DataSource = DT;
                textBox1.Text = "";
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
