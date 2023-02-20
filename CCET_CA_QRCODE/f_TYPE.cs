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
                QUERY_Data("INSERT INTO TBL_GG_STORE_TYPE (SPEC,RAM,USERNAME,INS_DT) VALUES ('" + txt_SPEC.Text + "/" + txt_GEN.Text + "','" + txt_RAM.Text.ToString() + "','" + txt_USER.Text.ToString() + "',GETDATE())","Insert");
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
    }
}
