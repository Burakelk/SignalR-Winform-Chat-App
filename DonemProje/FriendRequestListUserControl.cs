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
        public string Username;
        string userID;
        string TargetUserName;
        string BlockTargetUserName;
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
                    command.Parameters.AddWithValue("@Friend", TargetUserName);
                    command.Parameters.AddWithValue("@ME", Username);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Yeni arkadaşlık kaydı eklendi");

                }
                catch (Exception)
                {

                    throw;
                }

            }

        }
        public void DeleteRadioButton()
        {
            MainPage mainPage = new MainPage();

            foreach (Control control in FriendReqListGroupBox.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    FriendReqListGroupBox.Controls.Remove(control); // arkadaşlık isteği listesindeki zaten işlem yapılmış butonları siler


                }
            }
          

        }
        public void DeleteRadioButtonBlockedGroup()
        {
            foreach (Control control in BlockedUserListGroupBox.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    BlockedUserListGroupBox.Controls.Remove(control);  // bloklanmış  kullanıcılar listesindeki zaten işlem yapılmış butonları siler

                }
            }
        }
        public string GetCheckedRadioButtonName()
        {
            MainPage mainPage = new MainPage();

            foreach (Control control in FriendReqListGroupBox.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {

                    return radioButton.Name;     
                }
            }

            // Eğer işaretli bir RadioButton yoksa null döndür
            return null;
        }
        public string GetCheckBlockedRadioButtonName()
        {
            MainPage mainPage = new MainPage();

            foreach (Control control in BlockedUserListGroupBox.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {

                    return radioButton.Name;
                }
            }

            // Eğer işaretli bir RadioButton yoksa null döndür
            return null;
        }
        private void AcceptButton_Click(object sender, EventArgs e)
        {
            if (GetCheckedRadioButtonName() != null)
            {
                TargetUserName = GetCheckedRadioButtonName();
            }

            if (string.IsNullOrEmpty(TargetUserName))
            {
                MessageBox.Show("Select an option","WARNING!");
                return;
            }


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
                    command.Parameters.AddWithValue("@RECIEVER", TargetUserName);
                    command.Parameters.AddWithValue("@ME", Username);

                    command.ExecuteNonQuery();
                    AddFriendCaseDB();
                    DeleteRadioButton();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
                finally
                {
                    connection?.Close();

                }

            }
        }
        private void RejectFriendReq()
        {
            if (GetCheckedRadioButtonName() != null)
            {
                TargetUserName = GetCheckedRadioButtonName();
            }

            if (string.IsNullOrEmpty(TargetUserName))
            {
                MessageBox.Show("Select an option");
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    string query = @" DELETE o

FROM User_Relations_table o

INNER JOIN users_table u1

    ON o.USER_ID = u1.USER_ID

INNER JOIN users_table u2

    ON o.TARGET_USER_ID = u2.USER_ID

WHERE o._CASE = 'W' AND (u1.USERNAME = @RECIEVER AND u2.USERNAME = @ME);";



                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RECIEVER", TargetUserName);
                    command.Parameters.AddWithValue("@ME", Username);

                    command.ExecuteNonQuery();
                    DeleteRadioButton();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
                finally
                {
                    connection?.Close();

                }

            }

        }
        private void rejectButton_Click(object sender, EventArgs e)
        {
            RejectFriendReq();

        }

        private void BlockButton_Click(object sender, EventArgs e)
        {

            if (GetCheckedRadioButtonName() != null)
            {
                TargetUserName = GetCheckedRadioButtonName();
            }
            if (string.IsNullOrEmpty(TargetUserName))
            {
                MessageBox.Show("Select an option");
                return;
            }
            RejectFriendReq(); // ÖNCE DATABASE DEN İSTEK REDDEDİLİR SONRA BLOCKLAMA İŞLEMİ GERÇEKLEŞTİRİLİR.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    string query = @" 
INSERT INTO User_Relations_table (USER_ID, TARGET_USER_ID, _CASE)
SELECT 
    u1.USER_ID,
    u2.USER_ID,
    'B'
FROM users_table u1
INNER JOIN users_table u2
    ON 1 = 1
WHERE u1.USERNAME = @ME AND u2.USERNAME = @RECIEVER;";



                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RECIEVER", TargetUserName);
                    command.Parameters.AddWithValue("@ME", Username);
                    command.ExecuteNonQuery();
                    DeleteRadioButton();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
                finally
                {
                    connection?.Close();

                }

            }



        }

        private void UnBlockUserButton_Click(object sender, EventArgs e)
        {
            if (GetCheckBlockedRadioButtonName() != null)
            {
                BlockTargetUserName = GetCheckBlockedRadioButtonName();
            }
            if (string.IsNullOrEmpty(BlockTargetUserName))
            {
                MessageBox.Show("Select an option");
                return;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    connection.Open();
                    string query = @" DELETE o

FROM User_Relations_table o

INNER JOIN users_table u1

    ON o.USER_ID = u1.USER_ID

INNER JOIN users_table u2

    ON o.TARGET_USER_ID = u2.USER_ID

WHERE o._CASE = 'B' AND (u1.USERNAME = @ME AND u2.USERNAME =@RECIEVER );";



                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RECIEVER", BlockTargetUserName);
                    command.Parameters.AddWithValue("@ME", Username);
                    command.ExecuteNonQuery();
                    DeleteRadioButtonBlockedGroup();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
                finally
                {
                    connection?.Close();

                }

            }
        }
    }
}
