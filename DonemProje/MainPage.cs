using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Microsoft.Data.SqlClient;

namespace DonemProje
{
    public partial class MainPage : Form
    {
        string connectionString = " Data Source=LAPTOP-5188NCUM;Initial Catalog=users;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public string UserName;
        public int kullanıcıID;
        List<string[]> results = new List<string[]>();
        public MainPage(string userName,int KullanıcıID)
        {
            UserName=userName;
            this.kullanıcıID = KullanıcıID;


            InitializeComponent();
        }
        private void MainPage_Load(object sender, EventArgs e)
        {
            UserNameMainPageLabel.Text = UserName;
        }
        private void ProfileButton_Click(object sender, EventArgs e)
        {
            profileUserControl1.BringToFront();
        }

        private void ChatButton_Click(object sender, EventArgs e)
        {
            chatUserControl1.BringToFront();
            ChatUserControl chatUserControl = new ChatUserControl();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                   // string query = "SELECT TARGET_USER_ID FROM User_Relations_table WHERE _CASE = @CASE AND USER_ID=@USERID ";
                    string query = @" SELECT 
u2.username AS TargetUserName
FROM 
    User_Relations_table o
INNER JOIN 
    users_table u
ON 
    u.USER_ID = o.USER_ID
INNER JOIN 
    users_table u2
ON 
    o.TARGET_USER_ID = u2.USER_ID
WHERE 
    o._CASE = @CASE
    AND o.USER_ID = @USERID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CASE", "F");
                        command.Parameters.AddWithValue("@USERID", kullanıcıID);
                      
               
                  
                        try
                        {
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                
                                string[] row = new string[reader.FieldCount];
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    MessageBox.Show(reader[i].ToString());
                                    row[i] = reader[i]?.ToString(); // Değeri al ve null kontrolü yap
                               
                                }

                                results.Add(row); // Listeye ekle
                            }

                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Hata: " + ex.Message);
                        }


                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("HATA İLE KARŞILAŞILDI" + ex.ToString());
                    
                }
                finally
                {
                    connection.Close();
                }

            }


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

        private void findNewUserControl1_Load(object sender, EventArgs e)
        {

        }

      
    }
}
