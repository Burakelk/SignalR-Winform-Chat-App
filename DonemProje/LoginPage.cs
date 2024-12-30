

using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.AspNetCore.Http.Connections.Client;

namespace DonemProje
{

    public partial class LoginPage : Form
    {
        public string[] allInfo = new string[6];
        RegisterPage registerPage = new RegisterPage();
        int kullaniciID = -1;
        public LoginPage()
        {
            InitializeComponent();
        }
        string connectionString = " Data Source=LAPTOP-5188NCUM;Initial Catalog=users;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
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
        public bool ValidateUser(string userName, string password)
        {
            registerPage = new RegisterPage();

            string hashedPassword = registerPage.ComputeHash(password); // Kullanıcının girdiği şifreyi hash et

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string UserIDFetch = "SELECT USER_ID FROM users_table WHERE USERNAME = @USERNAME AND PASSW=@PASSW";
                try
                {


                    using (SqlCommand command = new SqlCommand(UserIDFetch, connection))
                    {
                        command.Parameters.AddWithValue("@USERNAME", userName);
                        command.Parameters.AddWithValue("@PASSW", hashedPassword);

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
        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (ValidateUser(EmailLogintxt.Text.Trim(), PasswordLogintextbox.Text))
            {

                MainPage mainPage = new MainPage(EmailLogintxt.Text.Trim(), kullaniciID); //kullanıcı ID si zaten alındı. Eğer giriş doğruysa kullanıcı adını da buradan gönderiyoruz.
                mainPage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
                EmailLogintxt.Text = "";
                PasswordLogintextbox.Text = "";
            }


        }

        private void PasswordShowButton_Click(object sender, EventArgs e)
        {
            PasswordLogintextbox.PasswordChar = PasswordLogintextbox.PasswordChar == '●' ? '\0' : '●';
            PasswordShowButton.Image = PasswordLogintextbox.PasswordChar == '●' ? Properties.Resources.gozKapali : Properties.Resources.gozAcik;


        }

        private void EmailLogintxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void ForgetPasswordButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            registerPage.Show();
        }
    }
}
