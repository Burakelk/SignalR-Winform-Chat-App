using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.X509Certificates;
using DonemProje.Model;

namespace DonemProje
{
    public partial class RegisterPage : Form
    {

        public RegisterPage()
        {
            InitializeComponent();
        }

        // hiç kod oluşturulmamışsa ilk fonksiyon çalışır
        string verificationCode = "";
        public string VerifCodeCreater()
        {
            // creating verification code 
            string VerfCode = "";
            for (int i = 0; i < 5; i++)
            {
                Random rnd = new Random();
                VerfCode += Convert.ToString(rnd.Next(10));
            }

            return VerfCode;
        }


        private void RegisterRegisterpageTxt_Click(object sender, EventArgs e)
        {
            #region ErrorCheck
            //  Kullanıcı adı için TextBox'ın boş olup olmadığını kontrol etme
            if (string.IsNullOrEmpty(UserNametxt.Text))
            {
                // Hata mesajını gösterme
                UserNameErr.SetError(UserNametxt, "Bu alan boş olamaz");

            }
            else
            {
                UserNameErr.Clear();

            }// buraya ekstra olarak Diğer kullanıcı adlarıyla aynı olamaması için veritabanı sorgulama eklenecek ***********************************

            // İsim ve soyisim içnin TextBox'ın boş olup olmadığını kontrol etme
            if (string.IsNullOrEmpty(fullNametxt.Text))
            {
                // Hata mesajını gösterme
                fullNameErr.SetError(fullNametxt, "Bu alan boş olamaz");

            }
            else
            {
                fullNameErr.Clear();

            }

            //E - mail için TextBox'ın boş olup olmadığını kontrol etme
            if (string.IsNullOrEmpty(EmailRegistertxt.Text))
            {
                // Hata mesajını gösterme
                EmailRegisterErr.SetError(EmailRegistertxt, "Bu alan boş olamaz");

            }
            else
            {
                EmailRegisterErr.Clear();

            }
            //şifre için TextBox'ın boş olup olmadığını kontrol etme
            if (string.IsNullOrEmpty(Password1textbox.Text))
            {
                // Hata mesajını gösterme
                Password1Err.SetError(Password1textbox, "Bu alan boş olamaz");

            }
            else
            {
                Password1Err.Clear();

            }
            if (string.IsNullOrEmpty(Password2textbox.Text))
            {
                // Hata mesajını gösterme
                Password2Err.SetError(Password2textbox, "Bu alan boş olamaz");

            }
            else
            {
                Password2Err.Clear();
            }

            // cinsiyet seçimi için kontrol
            if (maleRadiobutton.Checked == false && femaleRadiobutton.Checked == false)
            {
                GenderErr.SetError(femaleRadiobutton, "Bu alan boş olamaz");
            }
            else
            {
                GenderErr.Clear();
            }

            if (VerfCodeCheck.Visible == false)
            {
                ApprovErr.SetError(maskedTextBox1, "E-postanın doğrulanmış olması gerek");

            }

            if (UserNameErr.HasErrors || fullNameErr.HasErrors || EmailRegisterErr.HasErrors || Password1Err.HasErrors || Password2Err.HasErrors || GenderErr.HasErrors || ApprovErr.HasErrors)
            {

                return;
            }
            // şifrelerin aynı olup olmadığını kontrol etme
            if (Password1textbox.Text != Password2textbox.Text)
            {
                MessageBox.Show("Girilen Şifrelerin Aynı olması lazım");
                return;
            }
            DateTime birthDate = DateTimePickertxt.Value;
            if (DateTimePickertxt.Value > DateTime.Now)
            {
                MessageBox.Show("Lütfen geçerli bir tarih giriniz");
                return;
            }
            // Bugünün tarihini alma
            DateTime todayDate = DateTime.Today;

            // Yaşı hesaplama
            int age = todayDate.Year - birthDate.Year;

            // Ay ve gün farklarını da hesaba katma
            if (todayDate.Month < birthDate.Month)
            {
                age--;
            }
            else if (todayDate.Month == birthDate.Month && todayDate.Day < birthDate.Day)
            {
                age--;
            }

            if (age < 18)
            {
                MessageBox.Show("Yaşın henüz " + age + " bu uygulamayı kullanmak için yeterince büyük değilsin.\n" + (18 - age) + " yıl sonra tekrar bekleriz :)");
                return;
            }
            #endregion
            DatabaseContext db = new DatabaseContext();


            bool gender = femaleRadiobutton.Checked == true ? true : false;
            Users users = new Users();
            users.Username = UserNametxt.Text;
            users.FullName = fullNametxt.Text;
            users.Email = EmailRegistertxt.Text.ToLower();
            users.Password = Password1textbox.Text;
            users.DateOfBirth = birthDate;
            users.Gender = gender;  // erkekler için 0 kadınları için 1 değeri verir
            db.Add(users);
            db.SaveChanges();






            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();


        }

        private void verificationCodeSendertxt_Click(object sender, EventArgs e)
        {

            verificationCode = VerifCodeCreater();

            // Gönderen ve alıcı e-posta adreslerini ve şifreyi girin.
            string gonderen = "//your e-mail";
            string sifre = "//your e-email's app password";
            string alici = EmailRegistertxt.Text;
            if (alici == null)
            {
                MessageBox.Show("Lütfen geçerli bir e-posta giriniz");
                return;
            }
            // E-posta konusunu ve gövdesini girin.
            string konu = "Kodunuz: " + verificationCode;
            string govde = "Uygulamaya giriş yapmak için kodunuz:  " + verificationCode;

            // SmtpClient nesnesini oluşturun ve ayarlarını belirleyin.
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com"; // SMTP sunucu adresini girin.
            smtpClient.Port = 587; // SMTP sunucu portunu girin.
            smtpClient.EnableSsl = true; // SSL'yi etkinleştirin.
            smtpClient.Credentials = new NetworkCredential(gonderen, sifre);

            // E-posta mesajını oluşturun ve gönderin.

            try
            {
                MailMessage mailMessage = new MailMessage(gonderen, alici, konu, govde);
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                MessageBox.Show("Girdiğiniz e-posta adresinizi konrol ediniz.");
                return;
            }


            // Gönderme işleminin başarılı olduğunu gösteren bir mesaj gösterin.
            MessageBox.Show("E-posta başarıyla gönderildi! \nKodu almadıysanız girdiğiniz E-Posta adresini kontrol ediniz. ");

        }

        private void RegisterPage_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void verCodeCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void approveTheCodebutton_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == verificationCode)
            {
                VerfCodeCheck.Visible = true;
                MessageBox.Show("E-posta doğrulama başarılı!");
                ApprovErr.Clear();
            }
            else
            {
                MessageBox.Show("Kod hatalı, Lütfen tekrar deneyin.");
            }

        }

        private void UserNametxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordShowButton_Click(object sender, EventArgs e)
        {

<<<<<<< Updated upstream
=======
        
>>>>>>> Stashed changes
            Password1textbox.PasswordChar = Password1textbox.PasswordChar == '●' ? '\0' : '●';
            PasswordShowButton.Image = Password1textbox.PasswordChar == '●' ? Properties.Resources.gozKapali : Properties.Resources.gozAcik;

            Password2textbox.PasswordChar = Password2textbox.PasswordChar == '●' ? '\0' : '●';

        }

        private void GoBackLoginButton_Click(object sender, EventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }

        
    }
}
