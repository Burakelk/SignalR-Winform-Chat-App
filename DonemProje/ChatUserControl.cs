using Azure.Core;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Data.SqlClient;
using System;

using System.ComponentModel;
using System.Data;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DonemProje
{
    public partial class ChatUserControl : UserControl
    {


        public ChatUserControl()
        {
            InitializeComponent();
        }
        string connectionString = " Data Source=LAPTOP-5188NCUM;Initial Catalog=users;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public string _username;
        private string _targetUsername;

        private void BlockFriend()
        {
            DeleteFriendColum();
            AddBlock();
        }
        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        { //kullanıcıyı engelleme butonu
            _targetUsername = UserNameLabelChatUserControl.Text; ;
            BlockFriend();
            MainPage mainPage = new MainPage();
          
          

        }
        private void DeleteFriendColum()
        {

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

WHERE o._CASE = 'F' AND ((u1.USERNAME = @RECIEVER AND u2.USERNAME = @ME)OR(u1.USERNAME = @ME AND u2.USERNAME =@RECIEVER ));";



                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RECIEVER", _targetUsername);
                    command.Parameters.AddWithValue("@ME", _username);

                    command.ExecuteNonQuery();
                    MessageBox.Show("This user is now blocked", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    connection?.Close();

                }

            }

        }

        private void AddBlock()
        {
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
                    command.Parameters.AddWithValue("@RECIEVER", _targetUsername);
                    command.Parameters.AddWithValue("@ME", _username);
                    command.ExecuteNonQuery();



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

    }
}
