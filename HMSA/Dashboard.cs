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
using Microsoft.Web.WebView2.Core;
using System.IO;
using Xamarin.Essentials;
using Sentry.Protocol;
//using CsQuery.Utility;
using Newtonsoft.Json;
using Microsoft.Office.Interop.Word;
//using System.Net.Sockets;


//using System.Windows.HospitalInformation;

namespace HMSA
{
    public partial class Dashboard : Form
    {

        String path = "Data Source=(localdb)\\MSSqlLocalDB; Initial Catalog=hospital; Integrated Security=True";
        SqlConnection con;/// <summary>
        ///  </summary>
        SqlCommand cmd;

        public string loginname { get; internal set; }
        

        public Dashboard()
        {
            InitializeComponent();
            
            webView.WebMessageReceived += webView_WebMessageReceived;
           
            con = new SqlConnection(path);
            panel2.Location = panel1.Location;
            panel3.Location = panel1.Location;
            //panel4.Location = panel1.Location;

        }

        private void button5_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }


        private void labelIndecator3_Click(object sender, EventArgs e)
        {
        }

        private void labelIndecator4_Click(object sender, EventArgs e)
        {
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            btnAddPatient.Show();

            btnAddMoreInfo.Show();
            btnHistory.Show();
            btnHospitalInfo.Show();
            button2.Hide();
            pictureBox1.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            if (MyConnection.type == "A")
            {
                btnAddPatient.Visible = true;
                btnAddMoreInfo.Visible = true;
                btnHistory.Visible = true;
                btnHospitalInfo.Visible = true;
                button2.Visible=false;

            }
            else if (MyConnection.type == "U") {
                pictureBox1.Visible = false;
                btnAddPatient.Visible = true;
                btnAddMoreInfo.Visible = false;
                btnHistory.Visible = false;
                button2.Visible = true;
                btnHospitalInfo.Visible = true;

            }

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }



        private void label14_Click(object sender, EventArgs e)
        {

        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }



        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtName.Text == "" || txtAddress.Text == "" || txtContactNumber.Text == "" || txtAge.Text == "" || txtBlood.Text == "" || txtAny.Text == "" || txtPid.Text == "")
            {
                MessageBox.Show("Plese fill in the blanks");
            }
            else
            {
                try
                {

                    string Gender;
                    if (radioButton1.Checked) { Gender = "Male"; }
                    else { Gender = "Female"; }
                    con.Open();
                    cmd = new SqlCommand("insert into AddPatient(Name,Full_Address,Contact,Age,Gender ,Blood_Group,Major_Disease,pid) values('" + txtName.Text + "', '" + txtAddress.Text + "', '" + txtContactNumber.Text + "', '" + txtAge.Text + "', '" + Gender + "', '" + txtBlood.Text + "', '" + txtAny.Text + "', '" + txtPid.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("data has been saved in database");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                //MessageBox.Show("");
                txtName.Clear();
                txtAddress.Clear();
                txtContactNumber.Clear();
                txtAge.Clear();
                txtBlood.Clear();
                txtAny.Clear();
                txtPid.Clear();
            }
        }

        private void btnAddPatient_Click_1(object sender, EventArgs e)
        {

            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }



        private void btnAddMoreInfo_Click(object sender, EventArgs e)
        {
            //hidepanels();
            panel2.Visible = true;
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }
        

        private void label14_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public void refreshreg(string cond)
        {
            if (cond == "true")
            {
                string sqlstmt = "select * from AddPatient where pid='" + textpid.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(sqlstmt, con);
                DataSet dset = new System.Data.DataSet();
                sda.Fill(dset, "AddPatient");
                dataGridView1.DataSource = dset.Tables[0];
            }
            
        }

        private void textpid_TextChanged(object sender, EventArgs e)
        {

            if (textpid.Text != "" )
            {
                
                int pid = int.Parse(textpid.Text);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from AddPatient where pid=" + pid + "";
                //cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //int a;

                dataGridView1.DataSource = ds.Tables[0];

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int pid = int.Parse(textpid.Text);


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into PatientMore(pid,Symptoms,Diagonosis,Medicines,Ward ,Ward_Type) values('" + textpid.Text + "', '" + txtSymptoms.Text + "', '" + txtDignosis.Text + "', '" + txtMedicines.Text + "', '" + comboBox1.Text + "',  '" + comboBox2.Text + "')";
                //cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                MessageBox.Show("data has been saved in database");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textpid.Clear();
            txtSymptoms.Clear();
            txtDignosis.Clear();
            txtMedicines.Clear();

        
    }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from AddPatient" ;
            //cmd.CommandText = "select * from AddPatient inner join PatientMore on AddPatient.pid=PatientMore.pid";
            //cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            //panel3.Visible = true;
            //panel4.Visible = false;


            //System.Net.IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
            //    using (TcpClient client = new TcpClient())
            //    {
            //        client.Connect(ipAddress, 21);
            //        lblStatus = "Connected...";
            //    }

            }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void btnHospitalInfo_Click(object sender, EventArgs e)
        {


            panel4.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void webView_Click(object sender, EventArgs e)
        {
            //this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            this.Close();
            //Dashboard_Load();
            //panel3.Visible = true;
        }



        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void lblRecivmsg_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thank You For Your FeedBack");
        }

        private void txtSendmsg_TextChanged(object sender, EventArgs e)
        {
            

            
        }

        private void Sendmsg_Click(object sender, EventArgs e)
        {
            var msg = txtSendmsg.Text;
            webView.ExecuteScriptAsync($"MessageReceived(' {msg} ')");
        }
        private void webView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            lblRecivmsg.Text = e.TryGetWebMessageAsString();
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            panel1.Visible = false;
            panel4.Visible = false;
            //pictureBox1.Visible = true;

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            int pid = int.Parse(textpid.Text);
            Form2 fm = new Form2(pid);
            fm.ShowDialog(this);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Refreshbtn_Click(object sender, EventArgs e)
        {
            if (textpid.Text != "")
            {
                int pid = int.Parse(textpid.Text);
                //if (pid || )
                //{
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from AddPatient where pid=" + pid + ""; 
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }


        }

        private void label18_Click(object sender, EventArgs e)
        {
            
        }

        private void txtBlood_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
