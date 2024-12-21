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
using Guna.UI2.WinForms;
using Microsoft.AspNetCore.Connections;
using TheArtOfDevHtmlRenderer.Adapters;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System.Data.Common;
using Microsoft.AspNet.SignalR.Messaging;
using ConnectionState = Microsoft.AspNet.SignalR.Client.ConnectionState;

namespace DonemProje
{
    public partial class MainPage : Form
    {
        string TargetUsername = null;
        string connectionString = " Data Source=LAPTOP-5188NCUM;Initial Catalog=users;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public string UserName;
        public int kullanıcıID;
        List<string> Friends = new List<string>();
        private ChatUserControl chatUserControl;
        private HubConnection _connection;
        private IHubProxy _hubProxy;
        private static Dictionary<string, List<string>> _photoChunks = new Dictionary<string, List<string>>();
        public MainPage(string userName, int KullanıcıID)
        {
            UserName = userName;
            this.kullanıcıID = KullanıcıID;


            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            UserNameMainPageLabel.Text = UserName;
            StartConnection();
            this.MainSendButtonPanel.Hide();
            this.MainSendFilePanel.Hide();
            this.MainTextboxPanel.Hide();

            try
            {
                //Gelen mesaj varsa gerçek zamanlı dinlenir.
                _hubProxy.On<string, string>("receiveMessage", (senderName, message) =>
                {
                    if (sender == "server")
                    {
                        MessageBox.Show("Bu kullanıcı şuan aktif. başka cihazlardan çıkış yapın");
                        Application.Exit();
                    }

                    Invoke(new Action(() =>
                    {

                        YouBubble youBubble = new YouBubble();


                        if (chatUserControl != null)
                        {
                            youBubble.MessageLabel.Text = message;

                            chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(youBubble);
                            youBubble.Dock = DockStyle.Top;
                            youBubble.BringToFront();
                            youBubble.Focus();
                        }
                    }));
                });
                _hubProxy.On<string, string, int, string>("receiveMedia", (senderName, base64Chunk, totalChunk, typeOfFile) =>
                    {
                        Image i=null;
                        // Fotoğraf parçalarını saklamak
                        if (!_photoChunks.ContainsKey(TargetUsername))
                        {
                            _photoChunks[TargetUsername] = new List<string>();
                        }

                        // Parçayı ekle
                        _photoChunks[TargetUsername].Add(base64Chunk);




                        // Tüm parçalar alındıysa, fotoğrafı oluştur
                        if (_photoChunks[TargetUsername].Count == totalChunk)
                        {
                            // Base64 string'lerini birleştir
                            string fullBase64 = string.Join("", _photoChunks[TargetUsername]);

                            // Base64'ü byte dizisine dönüştür
                            byte[] imageBytes = Convert.FromBase64String(fullBase64);

                            // Fotoğrafı kaydet
                            File.WriteAllBytes($"{TargetUsername}_received_image.jpg", imageBytes);

                            if (typeOfFile == "video")
                            {
                                string tempFilePath = Path.Combine(Path.GetTempPath(), "temp_video.mp4");
                                File.WriteAllBytes(tempFilePath, imageBytes);

                                // Windows Media Player kontrolü ile bu geçici dosyayı oynatalım

                                 //axWindowsMediaPlayer1.URL = tempFilePath;
                                // axWindowsMediaPlayer1.Ctlcontrols.play();
                                if (File.Exists(tempFilePath))
                                {
                                    File.Delete(tempFilePath);

                                }
                            }
                            else if (typeOfFile == "photo")
                            {

                                MemoryStream ms = new MemoryStream(imageBytes);
                                i = Image.FromStream(ms);

                            }
                            else
                            {
                                MessageBox.Show("Dosyanın türü bilinmio la");
                            }
                            // Fotoğraf alındı ve birleştirildi
                            MessageBox.Show("Fotoğraf alındı ve kaydedildi!");



                            _photoChunks[TargetUsername].Clear();
                            _photoChunks.Remove(TargetUsername);

                        }

                        Invoke(new Action(() =>
                        {

                         
                            if (chatUserControl != null)
                            {   
                                YouBubbleMedia youBubbleMedia = new YouBubbleMedia();
                                
                                youBubbleMedia.pictureBoxYouBubble.Image = i;
                                youBubbleMedia.Size = youBubbleMedia.pictureBoxYouBubble.Size;
                                chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(youBubbleMedia);
                                youBubbleMedia.Dock = DockStyle.Top;
                                youBubbleMedia.BringToFront();
                                youBubbleMedia.Focus();
                            }


                        }));
                    });


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: " + ex.ToString());

            };

        }
        private void ProfileButton_Click(object sender, EventArgs e)
        {
            this.MainSendButtonPanel.Hide();
            this.MainSendFilePanel.Hide();
            this.MainTextboxPanel.Hide();
            this.MainPanelMainPage.Controls.Clear();
            ProfileUserControl profileUserControl = new ProfileUserControl();
            this.MainPanelMainPage.Controls.Add(profileUserControl);
            profileUserControl.Dock = DockStyle.Fill;


            profileUserControl.BringToFront();
        }
        private async void StartConnection()
        {
            _connection = new HubConnection("http://localhost:8080");
            _hubProxy = _connection.CreateHubProxy("ChatHub");
            try
            {
                // Bağlantıyı başlat
                await _connection.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}");
            }

            // Kullanıcıyı sunucuda kaydet
            await _hubProxy.Invoke("RegisterUser", UserName);
        }
        private void ChatButton_Click(object sender, EventArgs e)
        {


            if (this.FriendListPanelMainPage.Controls.Count > 0)
            {
                this.FriendListPanelMainPage.Controls.Clear();
            }

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

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        int i = 0;
                        while (reader.Read())
                        {

                            string friendName = reader["TargetUserName"].ToString();
                            Friends.Add(friendName);

                            i++;

                        }

                        reader.Close();
                    }


                    for (int i = 0; i < Friends.Count; i++)
                    {


                        FriendsListMemberUserControl friendsListMemberUserControl = new FriendsListMemberUserControl
                        {
                            Name = $"{Friends[i]}",
                            Cursor = Cursors.Hand
                        };
                        friendsListMemberUserControl.Click += (s, args) =>
                        {
                            if (this.MainPanelMainPage.Controls.Count > 0)
                            {
                                this.MainPanelMainPage.Controls.Clear();
                            }
                            this.MainSendButtonPanel.Show();
                            this.MainSendFilePanel.Show();
                            this.MainTextboxPanel.Show();
                            chatUserControl = new ChatUserControl();
                            this.MainPanelMainPage.Controls.Add(chatUserControl);
                            chatUserControl.UserNameLabelChatUserControl.Text = friendsListMemberUserControl.Name;
                            chatUserControl.Dock = DockStyle.Fill;
                            TargetUsername = friendsListMemberUserControl.Name;
                            MessageBox.Show($"Tıklanan Label:{friendsListMemberUserControl.Name} ");
                        };
                        friendsListMemberUserControl.usernameFriendLabel.Text = Friends[i].ToString();
                        this.FriendListPanelMainPage.Controls.Add(friendsListMemberUserControl);
                        friendsListMemberUserControl.Dock = DockStyle.Top;


                    }

                    Friends.Clear();


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




        }
        private void DeleteChatPanel()
        {

        }
        private void FindNewButton_Click(object sender, EventArgs e)
        {
            this.MainPanelMainPage.Controls.Clear();
            this.MainSendButtonPanel.Hide();
            this.MainSendFilePanel.Hide();
            this.MainTextboxPanel.Hide();
            FindNewUserControl findNewUserControl = new FindNewUserControl();
            this.MainPanelMainPage.Controls.Add(findNewUserControl);
            findNewUserControl.Dock = DockStyle.Fill;
            findNewUserControl.BringToFront();
        }

        // Alıcı tarafında fotoğraf parçalarını alıp birleştiriyoruz

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            StopConnection();
            LoginPage loginPage = new LoginPage();
            loginPage.Show();
            this.Close();
        }
        private async void SendMessage(string receiver, string message)
        {
            try
            {
                // Sunucuya özel mesaj gönder
                await _hubProxy.Invoke("SendMessageToUser", UserName, receiver, message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message send error: {ex.Message}");
            }
        }

        public void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void findNewUserControl1_Load(object sender, EventArgs e)
        {

        }

        private void FriendListPanelMainPage_Click(object sender, PaintEventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sendButtonChat_Click(object sender, EventArgs e)
        {






            if (chatUserControl != null)
            {
                MeBubble meBubble = new MeBubble();
                meBubble.MessageLabel.Text = textboxChat.Text;


                chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(meBubble);
                SendMessage(TargetUsername, textboxChat.Text);
                meBubble.Size = new Size(326, meBubble.MessageLabel.Size.Height + 20);
                meBubble.Dock = DockStyle.Top;
                meBubble.BringToFront();
                meBubble.Focus();
            }

        }

        private void StopConnection()
        {
            if (_connection != null && _connection.State == ConnectionState.Connected)
            {
                _connection.Stop();
                _connection.Dispose();
            }
        }
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            StopConnection();
            Application.Exit();
        }

        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopConnection();
            Application.Exit();
        }
        public byte[] GetImageBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
        public string ConvertBytesToBase64(byte[] byteArray)
        {
            return Convert.ToBase64String(byteArray);
        }
        public List<string> SplitBase64String(string base64String, int chunkSize)
        {
            List<string> chunks = new List<string>();

            for (int i = 0; i < base64String.Length; i += chunkSize)
            {
                int size = Math.Min(chunkSize, base64String.Length - i);
                string chunk = base64String.Substring(i, size);
                chunks.Add(chunk);
            }

            return chunks;
        }
        public async Task SendPhotoToServer(string receiverUsername, string senderUsername, string filePath, string typeOfFile)
        {


         
            byte[] fileBytes = GetImageBytes(filePath);

           
            string base64String = ConvertBytesToBase64(fileBytes);
            MessageBox.Show(base64String.Length.ToString());
          
            List<string> chunks = SplitBase64String(base64String, 8192); // 8KB'lık parçalara böl

            for (int i = 0; i < chunks.Count; i++)
            {
                // Her bir parça için hedef kullanıcıya mesaj gönder
                await _hubProxy.Invoke("SendMediaToUser", senderUsername, receiverUsername, chunks[i], chunks.Count, typeOfFile);
            }
        }
        private void sendFileButtonChat_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Title = "Bir dosya seçin";
                openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.tif|Video Dosyaları|*.mp4;*.avi;*.mp3";
                string typeOfFile = null;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    if (filePath.EndsWith(".jpg") || filePath.EndsWith(".png") || filePath.EndsWith(".jpeg") || filePath.EndsWith(".gif") || filePath.EndsWith(".tif"))
                        typeOfFile = "photo";
                    else if (filePath.EndsWith(".mp4") || filePath.EndsWith(".avi") || filePath.EndsWith(".mp3"))
                        typeOfFile = "video";



                    try
                    {
                        SendPhotoToServer(TargetUsername, UserName, filePath, typeOfFile);

                        MeBubbleMedia meBubbleMedia = new MeBubbleMedia();
                     
                        if (chatUserControl != null)
                        {
                            meBubbleMedia.pictureBoxMeBubble.Image = Image.FromFile(filePath);
                            chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(meBubbleMedia);
                            meBubbleMedia.Size = meBubbleMedia.pictureBoxMeBubble.Size;
                            meBubbleMedia.Dock = DockStyle.Top;
                            meBubbleMedia.BringToFront();
                            meBubbleMedia.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}");
                    }

                }
            }
        }
    }
}
