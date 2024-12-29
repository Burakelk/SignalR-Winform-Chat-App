using Microsoft.Data.SqlClient;
using System;
using System.Collections;
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
    public partial class FriendRequestListUserControl : UserControl
    {
        string Username;
        string userID;
        string TargetUserName;
        public FriendRequestListUserControl(string username)
        {
            Username = username;
            InitializeComponent();
        }
        public FriendRequestListUserControl()
        {

            InitializeComponent();
        }
        string connectionString = " Data Source=LAPTOP-5188NCUM;Initial Catalog=users;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        private void AddFriendCaseDB()
        {
            MainPage mainPage = new MainPage();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string insert = @"INSERT INTO User_Relations_table (USER_ID, TARGET_USER_ID, _CASE)
SELECT 
    u1.USER_ID AS USER_ID,
    u2.USER_ID AS TARGET_USER_ID,
    'F' AS _CASE
FROM 
    users_table u1
INNER JOIN 
    users_table u2
ON 
    1=1 -- Bu, herhangi bir özel bağlama gerek olmadığını ifade eder.
WHERE 
    u1.USERNAME = @ME AND u2.USERNAME = @Friend;
";
                    SqlCommand command = new SqlCommand(insert, connection);
                    command.Parameters.AddWithValue("@Friend", mainPage.SelectedUserForFriend);
                    command.Parameters.AddWithValue("@ME", mainPage.Name);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Yeni arkadaşlık kaydı eklendi");

                }
                catch (Exception)
                {

                    throw;
                }

            }

        }
        public string GetCheckedRadioButtonName( )
        {
            MainPage mainPage= new MainPage();
            // Paneldeki tüm RadioButton kontrollerini dolaş
            foreach (Control control in FriendReqListGroupBox.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    // İşaretli olan RadioButton'un adını döndür
                    return radioButton.Name;
                }
            }

            // Eğer işaretli bir RadioButton yoksa null döndür
            return null;
        }
        private void AcceptButton_Click(object sender, EventArgs e)
        {
             TargetUserName= GetCheckedRadioButtonName();
            if (string.IsNullOrEmpty(TargetUserName))
            {
                MessageBox.Show("Bir seçenek işaretle");
                return;
            }
            MainPage mainPage = new MainPage();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    string query = @" UPDATE o
SET o._CASE = 'F'

FROM User_Relations_table o

INNER JOIN users_table u1

    ON o.USER_ID = u1.USER_ID

INNER JOIN users_table u2

    ON o.TARGET_USER_ID = u2.USER_ID

WHERE o._CASE = 'W' AND (u1.USERNAME = @RECIEVER AND u2.USERNAME = @ME);";



                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RECIEVER",TargetUserName);
                    command.Parameters.AddWithValue("@ME", Username);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Arkadaşlık kaydı güncellendi");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    connection?.Close();

                }

            }
        }
        private  void RejectFriendReq()
        {



        }
        private void rejectButton_Click(object sender, EventArgs e)
        {
            RejectFriendReq();

        }
    }
}
