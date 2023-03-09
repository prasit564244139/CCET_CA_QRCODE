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
    public partial class f_TYPE : Form
    {
        static DataTable DT = new DataTable();
        static SqlConnection conn = new SqlConnection();
        static String SQL;
        static void QUERY_Data(String SQL,String respontext)
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
                }else if (respontext == "UPDATE")
                {
                    int respon = cmd.ExecuteNonQuery();
                    if (respon > 0)
                    {
                        //MessageBox.Show(respontext + " : Sucess..");
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
            catch(Exception ex)
            {
                MessageBox.Show("SQL query error please check lan connect or input type.\n" + ex.ToString() );
            }
            finally
            {
                conn.Close();
            }
        }

        static void QUERY_UPDATE(String SQL, String respontext)
        {
            try
            {
                conn.ConnectionString = "Data Source=10.51.0.145;Initial Catalog=mes;User ID=calcomp;Password=calcomp";
                SqlCommand cmd = new SqlCommand(SQL, conn);
                conn.Open();
                if (respontext == "UPDATE")
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public f_TYPE()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_ADD_Click(object sender, EventArgs e)
        {
            if(txt_SPEC.Text.Trim() == "" || txt_GEN.Text.Trim() == "" || txt_RAM.Text.Trim() == "")
            {
                MessageBox.Show("Please enter input all fields.");
            }
            else
            {
                txt_SPEC.Text = txt_SPEC.Text.Trim().Replace("'", "");
                txt_GEN.Text = txt_GEN.Text.Trim().Replace("'","");
                txt_RAM.Text = txt_RAM.Text.Trim().Replace("'","");
                QUERY_Data("INSERT INTO TBL_GG_STORE_TYPE (SPEC,RAM,USERNAME,INS_DT) VALUES ('" + txt_SPEC.Text + "_" + txt_GEN.Text + "','" + txt_RAM.Text.ToString() + "','" + txt_USER.Text.ToString() + "',GETDATE())","Insert");
                QUERY_Data("SELECT * FROM TBL_GG_STORE_TYPE", "select");


                txt_SPEC.Clear();
                txt_RAM.Clear();
                txt_GEN.Clear();
                txt_SPEC.Focus();
            }
        }

        private void f_TYPE_Load(object sender, EventArgs e)
        {
            txt_USER.Text = c_VALUES.c_USER;
            txt_USER.ReadOnly = true;
            txt_USER.BackColor = Color.LimeGreen;
            QUERY_Data("SELECT * FROM TBL_GG_STORE_TYPE", "select");
            dataGridView1.DataSource = DT;
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            DataView dv = DT.DefaultView;
            dv.RowFilter = string.Format("SPEC like '%{0}%'", textBox2.Text.Trim());
            dataGridView1.DataSource = dv;
        }

        private void txt_USER_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void txt_SPEC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txt_SPEC.Text.Trim()))
            {
                txt_GEN.Focus();
            }
        }

        private void txt_GEN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txt_GEN.Text.Trim()))
            {
                txt_RAM.Focus();
            }
        }

        private void txt_RAM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(txt_GEN.Text.Trim()))
            {
                btn_ADD.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SQL = "UPDATE TBL_GG_STORE_TYPE ";
                SQL += "SET SPEC = '" + txt_SPEC.Text + "_" + txt_GEN.Text + "',RAM = '" + txt_RAM.Text.Trim() + "' ,USERNAME = '" + txt_USER.Text.Trim() + "',INS_DT = GETDATE() ";
                SQL += "WHERE ID_SPEC = '" + txt_ID.Text.Trim() + "' ";
                QUERY_UPDATE(SQL, "UPDATE");
                QUERY_Data("SELECT * FROM TBL_GG_STORE_TYPE", "select");
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = DT;

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
                txt_ID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                string[] sAry = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim().Split('_');
                txt_SPEC.Text = sAry[0].Trim();
                txt_GEN.Text = sAry[1].Trim();
                txt_RAM.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
