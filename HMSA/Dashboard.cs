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


        private void Dashboard_Load(object sender, EventArgs e)
        {
            btnAddPatient.Show();

            btnAddMoreInfo.Show();
            btnHistory.Show();
            btnHospitalInfo.Show();
            HospitalImage.Hide();
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
                HospitalImage.Visible=false;

            }
            else if (MyConnection.type == "U") {
                pictureBox1.Visible = false;
                btnAddPatient.Visible = true;
                btnAddMoreInfo.Visible = false;
                btnHistory.Visible = false;
                HospitalImage.Visible = true;
                btnHospitalInfo.Visible = true;

            }

        }





        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
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
                    cmd = new SqlCommand("insert into AddPatient(Name,City,Contact,Age,Gender ,Blood_Group,Major_Disease,pid) values('" + txtName.Text + "', '" + txtAddress.Text + "', '" + txtContactNumber.Text + "', '" + txtAge.Text + "', '" + Gender + "', '" + txtBlood.Text + "', '" + txtAny.Text + "', '" + txtPid.Text + "')", con);
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
            btnAddPatient.BackColor = System.Drawing.Color.FromArgb(38, 154, 237);
            btnAddPatient.ForeColor = System.Drawing.Color.White;

            btnAddMoreInfo.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnAddMoreInfo.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnHistory.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnHistory.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnHospitalInfo.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnHospitalInfo.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            HospitalImage.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            HospitalImage.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }



        private void btnAddMoreInfo_Click(object sender, EventArgs e)
        {
            //hidepanels();
            btnAddMoreInfo.BackColor = System.Drawing.Color.FromArgb(38, 154, 237);
            btnAddMoreInfo.ForeColor = System.Drawing.Color.White;

            btnAddPatient.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnAddPatient.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnHistory.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnHistory.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnHospitalInfo.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnHospitalInfo.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

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


            if (textpid.Text != "")
            {
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }




            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textpid.Text == "" || txtSymptoms.Text == "" || txtDignosis.Text == "" || txtMedicines.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Plese fill in the blanks");
            }
            else
            { 

                try
                {



                    int pid = int.Parse(textpid.Text);



                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into PatientMore(pid,Symptoms,Diagonosis,Medicines,Ward ,Ward_Type) values('" + textpid.Text + "', '" + txtSymptoms.Text + "', '" + txtDignosis.Text + "', '" + txtMedicines.Text + "', '" + comboBox1.Text + "', '" + comboBox2.Text + "')";
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
            }


        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            btnHistory.BackColor = System.Drawing.Color.FromArgb(38, 154, 237);
            btnHistory.ForeColor = System.Drawing.Color.White;

            btnAddPatient.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnAddPatient.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnAddMoreInfo.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnAddMoreInfo.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnHospitalInfo.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnHospitalInfo.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

            panel3.Visible = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //cmd.CommandText = "select * from AddPatient" ;
            //SELECT * FROM AddPatient FULL OUTER JOIN PatientMore ON AddPatient.pid = PatientMore.pid;
            cmd.CommandText = "SELECT AddPatient.pid as PatientID, AddPatient.Name, AddPatient.Contact, AddPatient.City, AddPatient.Age, AddPatient.Gender, AddPatient.Blood_Group, AddPatient.Major_Disease, Symptoms, Diagonosis as Diagnosis, Medicines, Ward as Requirements, Ward_Type FROM AddPatient LEFT OUTER JOIN PatientMore ON AddPatient.pid = PatientMore.pid;";
            //cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void btnHospitalInfo_Click(object sender, EventArgs e)
        {
            btnHospitalInfo.BackColor = System.Drawing.Color.FromArgb(38, 154, 237);
            btnHospitalInfo.ForeColor = System.Drawing.Color.White;

            btnAddPatient.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnAddPatient.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnAddMoreInfo.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnAddMoreInfo.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnHistory.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnHistory.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            HospitalImage.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            HospitalImage.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

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


            HospitalImage.BackColor = System.Drawing.Color.FromArgb(38, 154, 237);
            HospitalImage.ForeColor = System.Drawing.Color.White;

            btnAddPatient.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnAddPatient.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);
            btnHospitalInfo.BackColor = System.Drawing.Color.FromArgb(237, 238, 239);
            btnHospitalInfo.ForeColor = System.Drawing.Color.FromArgb(135, 135, 135);

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int pid = int.Parse(textpid.Text);
                Form2 fm = new Form2(pid);
                fm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            if (txtName.Text == "Enter Full Name")
            {
                txtName.Text = "";
                txtName.ForeColor = Color.Black;
            }
        }

        private void txtName_Leave_1(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                txtName.Text = "Enter Full Name";
                txtName.ForeColor = Color.Gray;
            }
        }


        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            if (txtAddress.Text == "Enter City")
            {
                txtAddress.Text = "";
                txtAddress.ForeColor = Color.Black;
            }
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            if (txtAddress.Text == "")
            {
                txtAddress.Text = "Enter City";
                txtAddress.ForeColor = Color.Gray;
            }
        }

        private void txtContactNumber_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtContactNumber_Enter(object sender, EventArgs e)
        {
            if (txtContactNumber.Text == "Enter Contact Number")
            {
                txtContactNumber.Text = "";
                txtContactNumber.ForeColor = Color.Black;
            }
        }

        private void TxtContact_Leave(object sender, EventArgs e)
        {
            if (txtAddress.Text == "")
            {
                txtAddress.Text = "Enter Contact Number";
                txtAddress.ForeColor = Color.Gray;
            }
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtAge_Enter(object sender, EventArgs e)
        {
            if (txtAge.Text == "Enter Age")
            {
                txtAge.Text = "";
                txtAge.ForeColor = Color.Black;
            }
        }

        private void txtAge_Leave(object sender, EventArgs e)
        {
            if (txtAge.Text == "")
            {
                txtAge.Text = "Enter Age";
                txtAge.ForeColor = Color.Gray;
            }
        }


        private void txtPid_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtPid_Enter(object sender, EventArgs e)
        {
            if (txtPid.Text == "Enter Patient ID")
            {
                txtPid.Text = "";
                txtPid.ForeColor = Color.Black;
            }
        }


        private void txtPid_Leave(object sender, EventArgs e)
        {
            if (txtAge.Text == "")
            {
                txtAge.Text = "Enter Patient ID";
                txtAge.ForeColor = Color.Gray;
            }
        }


        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtAny_Enter(object sender, EventArgs e)
        {
            if (txtAny.Text == "Enter Any Major Disease")
            {
                txtAny.Text = "";
                txtAny.ForeColor = Color.Black;
            }
        }

        private void txtAny_Leave(object sender, EventArgs e)
        {
            if (txtAny.Text == "")
            {
                txtAny.Text = "Enter Any Major Disease";
                txtAny.ForeColor = Color.Gray;
            }
        }

        private void txtBlood_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBlood_Enter(object sender, EventArgs e)
        {
            if (txtBlood.Text == "Enter Blood Group")
            {
                txtBlood.Text = "";
                txtBlood.ForeColor = Color.Black;
            }
        }

        private void txtBlood_Leave(object sender, EventArgs e)
        {
            if (txtBlood.Text == "")
            {
                txtBlood.Text = "Enter Blood Group";
                txtBlood.ForeColor = Color.Gray;
            }
        }

        private void textpid_Enter(object sender, EventArgs e)
        {
            if (textpid.Text == "Search Patient ID")
            {
                textpid.Text = "";
                textpid.ForeColor = Color.Black;
            }
        }

        private void textpid_Leave(object sender, EventArgs e)
        {
            if (textpid.Text == "Search Patient ID")
            {
                textpid.Text = "Search Patient I";
                textpid.ForeColor = Color.Gray;
            }
        }

        private void txtSymptoms_TextChanged(object sender, EventArgs e)
        {

        }


        private void txtSymptoms_Enter(object sender, EventArgs e)
        {
            if (txtSymptoms.Text == "Enter Symptoms")
            {
                txtSymptoms.Text = "";
                txtSymptoms.ForeColor = Color.Black;
            }
        }

        private void txtSymptoms_Leave(object sender, EventArgs e)
        {
            if (txtSymptoms.Text == "Enter Symptoms")
            {
                txtSymptoms.Text = "Search Patient ID";
                txtSymptoms.ForeColor = Color.Gray;
            }
        }

        private void txtDignosis_TextChanged(object sender, EventArgs e)
        {

        }


        private void txtDignosis_Enter(object sender, EventArgs e)
        {
            if (txtDignosis.Text == "Enter Diagnosis")
            {
                txtDignosis.Text = "";
                txtDignosis.ForeColor = Color.Black;
            }
        }

        private void txtDignosis_Leave(object sender, EventArgs e)
        {
            if (txtDignosis.Text == "")
            {
                txtDignosis.Text = "Enter Diagnosis";
                txtDignosis.ForeColor = Color.Gray;
            }
        }

        private void txtMedicines_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMedicines_Enter(object sender, EventArgs e)
        {           
            if (txtMedicines.Text == "Enter Medicine")
            {
                txtMedicines.Text = "";
                txtMedicines.ForeColor = Color.Black;
            }
        }

        private void txtMedicines_Leave(object sender, EventArgs e)
        {
            if (txtMedicines.Text == "")
            {
                txtMedicines.Text = "Enter Medicine";
                txtMedicines.ForeColor = Color.Gray;
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtAddress.Clear();
            txtContactNumber.Clear();
            txtAge.Clear();
            txtBlood.Clear();
            txtAny.Clear();
            txtPid.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click_1(object sender, EventArgs e)
        {

        }
    }
}
