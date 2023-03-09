using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Management;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Data.SqlClient;

namespace CCET_CA_QRCODE
{
    public partial class f_LAN : Form
    {
        public f_LAN()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //publie
        Thread myThread = null;
        static DataTable DT = new DataTable();
        static SqlConnection conn = new SqlConnection();
        static String SQL;
        c_VALUES c_VALUES = new c_VALUES();


        static void QUERY_Data(String SQL, String respontext)
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
                else if (respontext == "UPDATE")
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        //MessageBox.Show(respontext + " : Sucess..");
                    }
                    else
                    {
                        MessageBox.Show(respontext + "Fail..");
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

        private void f_LAN_Load(object sender, EventArgs e)
        {
            try
            {
                SQL = "SELECT RTRIM(LTRIM(LINE)) AS LINE FROM TBL_GG_IP";
                QUERY_Data(SQL, "select");
                foreach (DataRow dtRow in DT.Rows)
                {
                    cbb_LINE.Items.Add(dtRow["LINE"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmdScan_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                listVAddr.Items.Clear();
                //progressBar1.Value = 0;
                if (txt_IP.Text == string.Empty)
                {
                    //MessageBox.Show("No IP address entered.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "No IP address entered.";
                }
                else
                {
                    //Create new thread for pinging
                    //myThread = new Thread(() => scan(txtIP.Text));
                    myThread = new Thread(() => scan2(txt_IP.Text, txt_LASTIP.Text));
                    myThread.Start();

                    //if (myThread.IsAlive == true)
                    //{
                    //    cmdStop.Enabled = true;
                    //    cmdScan.Enabled = false;
                    //    txtIP.Enabled = false;
                    //    txtIP2.Enabled = false;
                    //}
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void scan2(string start, string end)
        {
                try
                {

                    //Split IP string into a 4 part array
                    string[] startIPString = start.Split('.');
                    int[] startIP = Array.ConvertAll<string, int>(startIPString, int.Parse); //Change string array to int array
                    string[] endIPString = end.Split('.');
                    int[] endIP = Array.ConvertAll<string, int>(endIPString, int.Parse);
                    int count = 0; //Count the number of successful pings
                    Ping myPing;
                    PingReply reply;
                    IPAddress addr;
                    IPHostEntry host;

                    //Progress bar
                    string[] IT_START = txt_IP.Text.Split('.');
                    string[] IT_LAST = txt_LASTIP.Text.Split('.');

                    progressBar1.Style = ProgressBarStyle.Continuous;
                    progressBar1.Maximum = (Int32.Parse(IT_LAST[3]) - Int32.Parse(IT_START[3])) + 1 ;
                    progressBar1.Value = 0;
                    listVAddr.Items.Clear();

                    //Loops through the IP range, maxing out at 255
                    for (int i = startIP[2]; i <= endIP[2]; i++)
                    { //3rd octet loop
                        for (int y = startIP[3]; y <= 255; y++)
                        { //4th octet loop
                            string ipAddress = startIP[0] + "." + startIP[1] + "." + i + "." + y; //Convert IP array back into a string
                            string endIPAddress = endIP[0] + "." + endIP[1] + "." + endIP[2] + "." + (endIP[3] + 1); // +1 is so that the scanning stops at the correct range

                            //If current IP matches final IP in range, break
                            if (ipAddress == endIPAddress)
                            {
                                break;
                            }

                            myPing = new Ping();
                            try
                            {
                                reply = myPing.Send(ipAddress, 500); //Ping IP address with 500ms timeout
                            }
                            catch (Exception ex)
                            {
                                break;
                            }

                            lblStatus.ForeColor = System.Drawing.Color.Green; //Set status label for current IP address
                            lblStatus.Text = "Scanning: " + ipAddress;

                            //Log pinged IP address in listview
                            //Grabs DNS information to obtain system info
                            if (reply.Status == IPStatus.Success)
                            {
                                try
                                {
                                    addr = IPAddress.Parse(ipAddress);
                                    host = Dns.GetHostEntry(addr);
                                    
                                    listVAddr.Items.Add(new ListViewItem(new String[] { ipAddress, host.HostName, "Up" })); //Log successful pings
                                    count++;
                                }
                                catch
                                {

                                    listVAddr.Items.Add(new ListViewItem(new String[] { ipAddress, "Could not retrieve", "Up" })); //Logs pings that are successful, but are most likely not windows machines
                                    count++;
                                }
                            }
                            else
                            {
                                //listVAddr.Items.Add(new ListViewItem(new String[] { ipAddress, "n/a", "Down" })); //Log unsuccessful pings
                            }
                            progressBar1.Value += 1; //Increase progress bar
                        }

                        startIP[3] = 1; //If 4th octet reaches 255, reset back to 1
                    }

                    //Re-enable buttons
                    cmdScan.Enabled = true;
                    //cmdStop.Enabled = false;
                    //txtIP.Enabled = true;
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    lblStatus.Text = "Done!";
                    MessageBox.Show("Scanning done!\nFound " + count + " hosts.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    for (int i = 0; i < listVAddr.Items.Count; i++)
                    {
                        if (listVAddr.Items[i].SubItems[1].Text != "Could not retrieve")
                        {
                            string[] PC_NAME = listVAddr.Items[i].SubItems[1].Text.Split('.');
                            
                            SQL = "SELECT RTRIM(LTRIM(NAME)) AS NAME FROM TBL_GG_STORE WHERE NAME = '" + PC_NAME[0] + "' ";
                            QUERY_Data(SQL, "select");
                            if (DT.Rows.Count == 1)
                            {
                                listBox1.Items.Add(listVAddr.Items[i].SubItems[1].Text);
                            }
                            
                        }
                        //MessageBox.Show(listVAddr.Items[i].SubItems[1].Text);
                    }

                //Catch exception that throws when stopping thread, caused by ping waiting to be acknowledged

            }
                catch (ThreadAbortException tex)
                {
                    Console.WriteLine(tex.StackTrace);
                    cmdScan.Enabled = true;
                    //cmdStop.Enabled = false;
                    //txtIP.Enabled = true;
                    //txtIP2.Enabled = true;
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Scanning stopped";
                }
                //Catch invalid IP types
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    cmdScan.Enabled = true;
                    //cmdStop.Enabled = false;
                    //txtIP.Enabled = true;
                    //txtIP2.Enabled = true;
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    lblStatus.Text = "Invalid IP range";
                }
        }



        private void btn_LINE_Click(object sender, EventArgs e)
        {
            try
            {
                
                f_SETIP f_SETIP = new f_SETIP();
                f_SETIP.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbb_LINE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SQL = "SELECT RTRIM(LTRIM(IP)) AS IP FROM TBL_GG_IP WHERE LINE = '"+ cbb_LINE.Text.Trim() +"' ";
                QUERY_Data(SQL, "select");
                txt_IP.Text = DT.Rows[0][0].ToString()+".1";
                txt_LASTIP.Text = DT.Rows[0][0].ToString() + ".255";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var listBoxItem in listBox1.Items)
                {
                    foreach (var listbox in listBox1.Items)
                    {
                        SQL = "INSERT INTO TBL_GG_STORELOG (NAME,KEY_MAC,STATUS,USERNAME,QR,INS_DT,LAST_UPD,MODEL) ";
                        SQL += "SELECT  RTRIM(LTRIM(NAME)) AS NAME,RTRIM(LTRIM(KEY_MAC)) AS KEY_MAC,RTRIM(LTRIM(STATUS)) AS STATUS,RTRIM(LTRIM(USERNAME)) AS USERNAME,RTRIM(LTRIM(QR)) AS QR,RTRIM(LTRIM(INS_DT)) AS INS_DT,GETDATE(),RTRIM(LTRIM(MODEL)) AS MODEL ";
                        SQL += "FROM TBL_GG_STORE  ";
                        SQL += "WHERE RTRIM(LTRIM(NAME)) = '" + listbox.ToString().Trim() + "' ";
                        QUERY_Data(SQL, "Insert");
                        SQL = "UPDATE TBL_GG_STORE ";
                        SQL += "SET STATUS = '"+ cbb_LINE .Text.Trim()+ "',USERNAME = '"+ c_VALUES.c_USER + "',LAST_UPD = GETDATE() ";
                        SQL += "WHERE NAME = '" + listbox.ToString().Trim() + "' ";
                        QUERY_Data(SQL, "UPDATE");
                    }
                    MessageBox.Show("Already");
                        

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
