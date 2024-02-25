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
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ProfileButton_Click(object sender, EventArgs e)
        {
            profileUserControl1.BringToFront();
        }

        private void ChatButton_Click(object sender, EventArgs e)
        {
            chatUserControl1.BringToFront();
        }

        private void FindNewButton_Click(object sender, EventArgs e)
        {
            findNewUserControl1.BringToFront();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }

        public void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
