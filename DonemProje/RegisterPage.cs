﻿using System;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNet.SignalR.Client;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Guna.UI2.WinForms;
using Microsoft.IdentityModel.Tokens;
namespace DonemProje
{
    public partial class RegisterPage : Form
    {
        int kullaniciID;
        string connectionString = " Data Source=LAPTOP-5188NCUM;Initial Catalog=users;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public RegisterPage()
        {
            InitializeComponent();
        }

        // hiç kod oluşturulmamışsa ilk fonksiyon çalışır
        string verificationCode;
        string verificationCodeForResetPass;
        public string VerifCodeCreater()
        {
            // doğrulama kodu oluştur
            string VerfCode = null;
            for (int i = 0; i < 5; i++)
            {
                Random rnd = new Random();
                VerfCode += Convert.ToString(rnd.Next(10));
            }
            if (string.IsNullOrEmpty(VerfCode))
            {
                VerifCodeCreater();
            }
            return VerfCode;
        }

        public string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        private void RegisterAcceptanceOfValuen()
        {
            #region DbRegister
            // database e kullanıcıları kaydet
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string hashedPassword = ComputeHash(Password1textbox.Text); // Şifreyi hash et
                try
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM users_table WHERE USERNAME = @USERNAME";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@USERNAME", UserNametxt.Text);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {

                        string insertQuery = "INSERT INTO users_table (USERNAME, PASSW,NAME_SURNAME,E_MAIL,BIRTH_DAY,GENDER) VALUES (@USERNAME, @PASSW,@NAME_SURNAME,@E_MAIL,@BIRTH_DAY,@GENDER)";
                        SqlCommand command = new SqlCommand(insertQuery, connection);
                        command.Parameters.AddWithValue("@USERNAME", UserNametxt.Text.Trim());
                        command.Parameters.AddWithValue("@PASSW", hashedPassword);
                        command.Parameters.AddWithValue("@NAME_SURNAME", fullNametxt.Text);
                        command.Parameters.AddWithValue("@E_MAIL", EmailRegistertxt.Text);
                        command.Parameters.AddWithValue("@BIRTH_DAY", DateTimePickertxt.Value);
                        command.Parameters.AddWithValue("@GENDER", 'M');
                        command.ExecuteNonQuery();
                        MessageBox.Show("KAYIT BAŞARILI");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    connection?.Close();

                }
                #endregion
            }
        }
        private bool RegisterErrorCheck()
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

            }

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


            if (UserNameErr.HasErrors || fullNameErr.HasErrors || EmailRegisterErr.HasErrors || Password1Err.HasErrors || Password2Err.HasErrors  || ApprovErr.HasErrors)
            {

                return false;
            }
            // şifrelerin aynı olup olmadığını kontrol etme
            if (Password1textbox.Text != Password2textbox.Text)
            {
                MessageBox.Show("Girilen Şifrelerin Aynı olması lazım");
                return false;
            }
            DateTime birthDate = DateTimePickertxt.Value;
            if (DateTimePickertxt.Value > DateTime.Now)
            {
                MessageBox.Show("Lütfen geçerli bir tarih giriniz");
                return false;
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
                return false;

            }

            #endregion
            SqlConnection connection = new SqlConnection(connectionString);
            string checkQuery = "SELECT COUNT(*) FROM users_table WHERE USERNAME = @USERNAME";
            SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@USERNAME", UserNametxt.Text);
            connection.Open();
            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

            if (count > 0)
            {
                UserNameErr.SetError(UserNametxt, "Bu kullanıcı adı zaten kullanılıyor. Lütfen başka bir kullanıcı adı giriniz.");
                connection.Close();
                return false;

            }
            connection.Close();
            return true;

        }
        private void RegisterRegisterpageTxt_Click(object sender, EventArgs e)
        {

            if (RegisterErrorCheck())
            {
                try
                {
                    RegisterAcceptanceOfValuen();
                    LoginPage loginPage = new LoginPage();
                    loginPage.Show();
                    this.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Bir hata ile karşılaşıldı" + ex.ToString());
                    return;
                }
            }
            else
            {
                return;
            }

        }
        private void SendCode(string alici)
        {
            verificationCode = VerifCodeCreater();

            // Gönderen ve alıcı e-posta adreslerini ve şifreyi girin.
            string gonderen = "celikburak4999@gmail.com";
            string sifre = "fncm ofpw nhjq yrei";
         
            if (string.IsNullOrEmpty(alici))
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
                MessageBox.Show("Girdiğiniz e-posta adresi eksik veya hatalı lütfen konrol ediniz.");
                return;
            }


            // Gönderme işleminin başarılı olduğunu gösteren bir mesaj gösterin.
            MessageBox.Show("E-posta başarıyla gönderildi! \nKodu almadıysanız girdiğiniz E-Posta adresini kontrol ediniz. ");
        }
        private void verificationCodeSendertxt_Click(object sender, EventArgs e)
        {
            SendCode(EmailRegistertxt.Text);

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
            if (maskedTextBox1.Text == verificationCode && !string.IsNullOrEmpty(maskedTextBox1.Text))
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
         
        private void PasswordShowButton2_Click(object sender, EventArgs e)
        {
            PasswordForgatTxt.PasswordChar = PasswordForgatTxt.PasswordChar == '●' ? '\0' : '●';
            guna2ImageButton1.Image = PasswordForgatTxt.PasswordChar == '●' ? Properties.Resources.gozKapali : Properties.Resources.gozAcik;

            guna2TextBox3.PasswordChar = guna2TextBox3.PasswordChar == '●' ? '\0' : '●';
        }
        private void PasswordShowButton_Click(object sender, EventArgs e)
        {


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


        private void SendRandomCode_Click(object sender, EventArgs e)
        {
            SendCode(guna2TextBox2.Text);

        }

        private void VerifyCodeButton_Click(object sender, EventArgs e)
        {
            if (maskedTextBox2.Text == verificationCode && !string.IsNullOrEmpty(maskedTextBox2.Text))
            {
                guna2CirclePictureBox1.Visible = true;
                MessageBox.Show("E-posta doğrulama başarılı!");
                ApprErrForget.Clear();
            }
            else
            {
                ApprErrForget.SetError(maskedTextBox2,"Verify the code");
                MessageBox.Show("Kod hatalı, Lütfen tekrar deneyin.");
            }
        }
        private bool IsValuesRight()
        {
            if (UserNameErr.HasErrors )
            {
                return false;
            }
            if ( PasswordForgatTxt.Text.Trim() != guna2TextBox3.Text.Trim()|| string.IsNullOrEmpty(PasswordForgatTxt.Text))
            {
                return false;
            }
            return true;
        }
        private bool  isUserExist()
        {
            if (string.IsNullOrEmpty( UserNameForgettxt.Text))
            {
                return false;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string UserIDFetch = "SELECT USER_ID FROM users_table WHERE USERNAME = @USERNAME";
                try
                {


                    using (SqlCommand command = new SqlCommand(UserIDFetch, connection))
                    {
                        command.Parameters.AddWithValue("@USERNAME", UserNameForgettxt.Text.Trim());
                 

                        connection.Open();
                        int result = Convert.ToInt32(command.ExecuteScalar());
                        if (result != null && result != 0)
                        {
                            kullaniciID = result;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("HATA İLE KARŞILAŞILDI" + ex.ToString());
                    return false;
                }
                finally
                {
                    connection.Close();
                }

            }
        }
        private  void DBUpdatePass()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string hashedPassword = ComputeHash(PasswordForgatTxt.Text); // Şifreyi hash et
                try
                {
                    connection.Open();

                    string query = "UPDATE users_table SET PASSW = @NewPassw WHERE USER_ID = @USERID AND E_MAIL=@EMAIL";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@NewPassw", hashedPassword);
                        command.Parameters.AddWithValue("@USERID", kullaniciID);
                        command.Parameters.AddWithValue("@EMAIL", guna2TextBox2.Text);
                      

                        command.ExecuteNonQuery();

                      
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            

        }
        }
        private void UpdatePassword_Click(object sender, EventArgs e)
        {
            if (IsValuesRight())
            {
                if (!isUserExist())
                {
                    MessageBox.Show("This user name does not exist");
                    return;
                }
                else
                {
                    DBUpdatePass();
                    MessageBox.Show("Your password has been updated");
                    LoginPage loginPage = new LoginPage();
                    loginPage.Show();
                    this.Close();

                }
            }
            else {
                MessageBox.Show("Please enter values right");
            }

        }
    }
}
