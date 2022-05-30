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

namespace HMSA
{
    public partial class Form1 : Form
    {
        MyConnection db = new MyConnection();
        //SqlConnection Con;

        //public SqlConnection Con { get; private set; }

        //String path = "Data Source=(localdb)\\MSSqlLocalDB; Initial Catalog=hospital; Integrated Security=True";
        //SqlConnection con;
        //SqlCommand cmd;
        public Form1()
        {
            InitializeComponent();
            //Con = new SqlConnection();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelLogin.Hide();
            panelRegister.Hide();
            panel2.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //   if (txtuser.Text == txtUserName.Text && txtCpass.Text == txtPassword.Text)
            //   {
            //       SqlCommand cmd = new SqlCommand();
            //       cmd.Connection = con;
            //       cmd.CommandText = "select * from Register where username = '" + txtuser.Text + "'and password = '" + txtpass.Text + "'" ;
            //       //cmd.ExecuteNonQuery();
            //       SqlDataAdapter da = new SqlDataAdapter(cmd);
            //       DataSet ds = new DataSet();
            //       da.Fill(ds);
            //       SqlDataReader dbr;

            //       con.Open();

            //       dbr = cmd.ExecuteReader();

            //       int count = 0;

            //       while (dbr.Read())

            //       {

            //           count = count + 1;

            //       }

            //       if (count == 1)

            //       {

            //           MessageBox.Show("username and password is correct");

            //       }

            //       else if (count > 1)

            //       {

            //           MessageBox.Show("Duplicate username and password", "login page");

            //       }

            //       else

            //       {

            //           MessageBox.Show(" username and password incorrect", "login page");

            //       }

            //   }

            //   else

            //   {

            //       MessageBox.Show(" dashboard opening", "login page");

            //   }
            //   this.Hide();
            //           Dashboard db = new Dashboard();
            //          db.Show();
            //}
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e) { }
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.CommandText = "insert into Register(username,password,confirm_password) " +
        //            "values('" + txtuser.Text + "', '" + txtpass.Text + "', '" + txtCpass.Text + "')";
        //        //cmd.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        MessageBox.Show("User has been Registerd");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //if (txtuser.Text == txtUserName.Text && txtCpass.Text == txtPassword.Text)
            //{
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = con;
            //    cmd.CommandText = "select * from Register where username = '" + txtuser.Text + "'and password = '" + txtpass.Text + "'";
            //    //cmd.ExecuteNonQuery();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    SqlDataReader dbr;

            //    con.Open();

            //    dbr = cmd.ExecuteReader();

            //    int count = 0;

            //    while (dbr.Read())

            //    {

            //        count = count + 1;

            //    }

            //    if (count == 1)

            //    {

            //
            //    }

            //    else if (count > 1)

            //    {

            //
            //    }

            //    else

            //    {

            //
            //    }

            //}

            //else

            //{


            //}
            //this.Hide();
            //Dashboard db = new Dashboard();
            //db.Show();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //panelLogin.Visible = false;
            panelRegister.Visible = true;
        }

        private void BtnRegistration_Click(object sender, EventArgs e)
        {
            try
            {
                using (db.con)
                {
                    SqlCommand cmd = new SqlCommand("insert into tb_login(id,name,username,password,type) " +
                            "values('" + id.Text + "','" + name.Text + "','" + txtUser.Text + "', '" + txtPass.Text + "', '" + txtCpass.Text + "')", db.con);
                    db.con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    //string name;
                    MessageBox.Show("User has been Registerd");

                    panelLogin.Visible = true;
                    //panelRegister.Visible = false;

                    //try
                    //{
                    //    SqlCommand cmd = new SqlCommand();
                    //    cmd.Connection = con;
                    //    cmd.CommandText = "insert into Register(username,password,confirm_password) " +
                    //        "values('" + txtUser.Text + "', '" + txtPass.Text + "', '" + txtCpass.Text + "')";
                    //    //cmd.ExecuteNonQuery();
                    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //    DataSet ds = new DataSet();
                    //    da.Fill(ds);
                    //    //string name;
                    //    MessageBox.Show("User has been Registerd");

                    //        panelLogin.Visible = true;
                    //    //panelRegister.Visible = false;

                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //}

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
        }

            private void btnLogin_Click_1(object sender, EventArgs e)
        {
            try 
            {
                using (db.con) {
                    SqlCommand cmd = new SqlCommand("sp_role_login", db.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    db.con.Open();
                    cmd.Parameters.AddWithValue("@uname", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@upass", txtPassword.Text);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        rd.Read();


                        if (rd[4].ToString() != "Admin")
                        {
                            MessageBox.Show("Invalid user");
                        }

                        else 
                        {
                            
                            MyConnection.type = "A";

                            Dashboard d = new Dashboard();
                            d.Show();
                            this.Hide();
                        }
                       


                    }
                    
                    else 
                    {
                        MessageBox.Show("Error Login");
                    }
                    
                }

            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
            //if (txtUser.Text == txtUserName.Text && txtCpass.Text == txtPassword.Text)
            //{
            //    SqlCommand cmd = new SqlCommand();
            //    cmd.Connection = con;
            //    cmd.CommandText = "select * from Register where username = '" + txtUser.Text + "'and password = '" + txtPass.Text + "'";
            //    //cmd.ExecuteNonQuery();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    SqlDataReader dbr;

            //    con.Open();

            //    dbr = cmd.ExecuteReader();

            //    int count = 0;

            //    while (dbr.Read())

            //    {

            //        count = count + 1;

            //    }

            //    if (count == 1)

            //    {


            //    }

            //    else if (count > 1)

            //    {


            //    }

            //    else

            //    {


            //    }

            //}

            //else

            //{


            //}
            //this.Hide();
            //Dashboard db = new Dashboard();
            //db.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            panelLogin.Visible = false;
            panelRegister.Visible = true;
        }

        //private void hyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        //{
        //    panelLogin.Visible = false;
        //    panelRegister.Visible = true;
        //}

        private void button1_Click_2(object sender, EventArgs e)
        {
            panelLogin.Visible = true;
            //panelRegister.Visible = false;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            panelLogin.Visible = false;
            panelRegister.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panelLogin.Visible = true;
            panelRegister.Visible = true;
            //AL.Show();
            //UL.Hide();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panelRegister.Visible = true;
            panelLogin.Visible = false;

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                using (db.con)
                {
                    SqlCommand cmd = new SqlCommand("sp_role_login", db.con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    db.con.Open();
                    cmd.Parameters.AddWithValue("@uname", textBox4.Text);
                    cmd.Parameters.AddWithValue("@upass", textBox3.Text);
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        rd.Read();


                        if (rd[4].ToString() != "User")
                        {
                            MessageBox.Show("Invalid User");
                        }

                        else 
                        {

                            MyConnection.type = "U";
                            Dashboard d = new Dashboard();
                            d.Show();
                            this.Hide();

                        }


                       
                    }
                    else
                    {
                        MessageBox.Show("Login Error");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panelLogin.Visible = false;
            panel2.Visible = false;

            panelRegister.Visible = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panelRegister_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtCpass_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

