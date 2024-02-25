using Guna.UI2.WinForms;
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
    public partial class ProfileUserControl : UserControl
    {
        public ProfileUserControl()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MainPage mainPage = new MainPage();
            
            openFileDialog1.Filter = "select image(*Jpg; *.png; *Gif|*.Jpg; *.png; *Gif"; if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                mainPage.ProfilePictureBoxMainPage.Image = Image.FromFile(openFileDialog1.FileName);
                
            }
           
        }
    }
}
