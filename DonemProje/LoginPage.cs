using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DonemProje
{
    public partial class LoginPage : Form
    {
        RegisterPage registerPage = new RegisterPage();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {



        }

        private void RegisterLoginPageButton_Click(object sender, EventArgs e)
        {
            registerPage.Show();
            this.Hide();
        }
    }
}
