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
        MyConnection db;
        public Form1()
        {
            InitializeComponent();
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

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e) { }


        private void btnLogin_Click(object sender, EventArgs e)
        {
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            panelRegister.Visible = true;
        }

        private void BtnRegistration_Click(object sender, EventArgs e)
        {

            try
            {
                MyConnection db = new MyConnection();
                using (db.con)
                {
                    SqlCommand cmd = new SqlCommand("insert into tb_login(id,name,username,password,type) " +
                            "values('" + id.Text + "','" + name.Text + "','" + txtUser.Text + "', '" + txtPass.Text + "', '" + txtCpass.Text + "')", db.con);
                    db.con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    //string name;
                    MessageBox.Show("User has been Registerd");
                    db.con.Close();
                    panelLogin.Visible = true;


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
                MyConnection db = new MyConnection();
                using (db.con)
                {
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


                        db.con.Open();
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

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            panelLogin.Visible = false;
            panelRegister.Visible = true;
        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            panelLogin.Visible = true;
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

