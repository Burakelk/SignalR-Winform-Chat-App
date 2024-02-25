using DonemProje.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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

        private void LoginButton_Click(object sender, EventArgs e)
        {

            //if (!(EmailLogintxt.Text.Contains('@') || EmailLogintxt.Text.Contains(".com")))
            //{
            //    EmailErr.SetError(EmailLogintxt, "Lütfen geçerli bir E-posta giriniz");
            //    return;
            //}
            //else
            //{
            //    EmailErr.Clear();
            //}

            //try
            //{

            //    using (var context = new DatabaseContext())
            //    {
            //        // Kullanıcı adını ve şifreyi metin kutularından al.
            //        string ePosta = EmailLogintxt.Text.ToLower();
            //        string password = PasswordLogintextbox.Text;

            //        // Kullanıcı bilgilerini sorgulama.
            //        var user = context.Users.Where(x => x.Email == ePosta).FirstOrDefault();

            //        if (user == null)
            //        {
            //            MessageBox.Show("Böyle bir kullanıcı bulunamadı");
            //            return;

            //        }
            //        if (user.Email == ePosta && user.Password != PasswordLogintextbox.Text)
            //        {
            //            PasswordInCorrectErr.SetError(PasswordLogintextbox, "Hatalı şifre");
            //            return;
            //        }
            //        else if (user.Email == ePosta && user.Password != PasswordLogintextbox.Text)
            //        {
            //            PasswordInCorrectErr.Clear();

            //        }


            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Bir hata bile karşılaşıldı");
            //    return;
            //}
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Hide();
        }

        private void PasswordShowButton_Click(object sender, EventArgs e)
        {
            PasswordLogintextbox.PasswordChar = PasswordLogintextbox.PasswordChar == '●' ? '\0' : '●';
            PasswordShowButton.Image = PasswordLogintextbox.PasswordChar == '●' ? Properties.Resources.gozKapali : Properties.Resources.gozAcik;

           
        }
    }
}
