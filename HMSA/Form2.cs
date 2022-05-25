using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMSA
{
    public partial class Form2 : Form
    {

        public static string name;

        public Form2(int pid)
        {
            string url = "http://localhost:3000/user/" + pid;

            InitializeComponent();
            webView21.Source = new Uri(url);
        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }

        private void webView21_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            string name = e.TryGetWebMessageAsString();
            //telling child class to use the existing dashboard obj of the parent class.
            Dashboard dashb = (Dashboard)this.Owner;
            dashb.refreshreg(name);
            this.Hide();
        }


        
    }
}
