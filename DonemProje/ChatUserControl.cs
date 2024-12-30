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
        private HubConnection _connection;
        private IHubProxy _hubProxy;
        public string _username;

        private string _targetUsername;
        private static Dictionary<string, List<string>> _photoChunks = new Dictionary<string, List<string>>();
        private void ChatUserControl_Load(object sender, EventArgs e)
        {

        }


        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

            //MeBubble meBubble = new MeBubble
            //{
            //    Name = $"MeBubble_{MeBubbleCount}",

            //};
            //meBubble.label1.Text = ChatTextbox.Text;
            //meBubble.label1.Dock = DockStyle.Right;
            //meBubble.Size = new Size(670, meBubble.label1.Height + 5);

            //ChatPanelChatUserControl.Controls.Add(meBubble);
            //meBubble.Location = new Point(meBubble.Location.X + 500, meBubble.Location.Y - 20);
            //meBubble.Dock = DockStyle.Top;

            //meBubble.BringToFront();
            //meBubble.Focus();
            //MeBubbleCount++;

        }

        private void FriendsListPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BlockFriend()
        {
            DeleteFriendColum();
            AddBlock();
        }
        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {
            _targetUsername = UserNameLabelChatUserControl.Text; ;
            BlockFriend();
            MainPage mainPage = new MainPage();
            foreach (Control control in mainPage.FriendListPanelMainPage.Controls)
            {
                if (control is UserControl userControl && control.Name==_username)
                {

                    mainPage.FriendListPanelMainPage.Controls.Remove(control);

                }
                
            }
            foreach (Control control in mainPage.MainPanelMainPage.Controls)
            {
                if ( control.Name == _username)
                {

                    mainPage.MainPanelMainPage.Controls.Remove(control);

                }

            }

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
                    MessageBox.Show("Arkadaşlık isteği reddedildi");



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

        private void guna2ImageButton1_MouseHover(object sender, EventArgs e)
        {
            guna2ImageButton1.BackColor = Color.White;
        }

        private void guna2ImageButton1_Leave(object sender, EventArgs e)
        {
            guna2ImageButton1.BackColor = Color.Bisque;
        }
    }
}
