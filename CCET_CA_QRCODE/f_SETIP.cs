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
    public partial class f_SETIP : Form
    {
        public f_SETIP()
        {
            InitializeComponent();
        }

        static DataTable DT = new DataTable();
        static SqlConnection conn = new SqlConnection();
        static String SQL;

        private void f_SETIP_Load(object sender, EventArgs e)
        {
            try
            {
                //SQL = "SELECT RTRIM(LTRIM(LOCATION)) AS LOCATION FROM TBL_GG_LOCATION";
                //QUERY_Data(SQL, "select");
                //foreach (DataRow dtRow in DT.Rows)
                //{
                //    txt_LINE.Items.Add(dtRow["LOCATION"].ToString());
                //}
                DATA_ALL();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        static void QUERY_Data(String SQL, String respontext)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                DT.Clear();
                conn.Open();
                if (respontext == "Insert")
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
        public void DATA_ALL()
        {
            try
            {
                DT.Clear();
                DT.Columns.Clear();
                dataGridView1.DataSource = null;
                SQL = "SELECT RTRIM(LTRIM(LINE)) AS LINE,RTRIM(LTRIM(IP)) AS IP FROM TBL_GG_IP";
                QUERY_Data(SQL, "SELECT");
                dataGridView1.DataSource = DT;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txt_IP.Text.Trim()))
                {
                    SQL = "INSERT INTO TBL_GG_IP (LINE,IP) VALUES('" + txt_LINE.Text.Trim() + "','" + txt_IP.Text.Trim() + "')";
                    QUERY_Data(SQL, "INSERT");
                    DATA_ALL();
                    txt_IP.Text = "";
                    txt_LINE.Text = "";
                    txt_IP.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                SQL = "DELETE FROM TBL_GG_IP WHERE LINE = '" + txt_LINE.Text.Trim() + "' and IP = '"+txt_IP.Text.Trim()+"'";
                QUERY_Data(SQL, "DELETE");
                DATA_ALL();
                txt_LINE.Text = "";
                txt_IP.Text = "";
                txt_IP.Focus();
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
                txt_LINE.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_IP.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
