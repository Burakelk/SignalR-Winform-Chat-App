using Microsoft.AspNet.SignalR.Client;
using J3QQ4;
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
using System.IO;
using AxWMPLib;
using WMPLib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using Microsoft.IdentityModel.Tokens;

namespace DonemProje
{
    public partial class MainPage : Form
    {
        string TargetUsername = null;
        readonly string  connectionString = " Data Source=LAPTOP-5188NCUM;Initial Catalog=users;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        public string UserName;
        public int UserID;
        string SelectedReqUsername;
        List<string> Friends = new List<string>();
        List<string> FriendsReq = new List<string>();
        List<string> BlockedFriends = new List<string>();
        Control SelectedControl;
        public string SelectedUserForFriend;
        public string SelectedUserForBlock;
        private ChatUserControl chatUserControl;

        private HubConnection _connection;
        private IHubProxy _hubProxy;
        private static Dictionary<string, List<string>> _photoChunks = new Dictionary<string, List<string>>();
        public MainPage(string userName, int KullanıcıID)
        {
            UserName = userName;
            UserID = KullanıcıID;


            InitializeComponent();
        }
        public MainPage()
        {
           
            InitializeComponent();
        }

        private void CreateChatUserControl(string name)
        {
            chatUserControl = new ChatUserControl()
            {
                Name = name
            };
            MainPanelMainPage.Controls.Add(chatUserControl);
            chatUserControl.UserNameLabelChatUserControl.Text = name;
            chatUserControl.Hide();
        }
        private void MainPage_Load(object sender, EventArgs e)
        {
            UserNameMainPageLabel.Text = UserName.Trim();
            StartConnection();
            ShowChatElements(false);

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
                        if (!MainPanelMainPage.Controls.ContainsKey(senderName))
                        {
                            CreateChatUserControl(senderName);

                        }
                        YouBubble youBubble = new YouBubble();


                        if (chatUserControl != null && MainPanelMainPage.Controls.ContainsKey(senderName))
                        {
                            youBubble.MessageLabel.Text = message;
                            chatUserControl = (ChatUserControl)MainPanelMainPage.Controls.Find(senderName, true).First();
                            chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(youBubble);
                            
                            youBubble.Dock = DockStyle.Top;
                            youBubble.BringToFront();
                            youBubble.Focus();
                        }
                    }));
                });
           
                _hubProxy.On<string, string, int, string>("receiveMedia", (senderName, base64Chunk, totalChunk, typeOfFile) =>
                {
                    Image i = Properties.Resources.default_image;

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

                            Invoke(new Action(() =>
                            {


                                if (chatUserControl != null && chatUserControl.Name == senderName)
                                {
                                    YouBubbleMedia youBubbleMedia = new YouBubbleMedia();

                                    AxWindowsMediaPlayer axWindowsMediaPlayer1 = new AxWindowsMediaPlayer();
                                    chatUserControl = (ChatUserControl)MainPanelMainPage.Controls.Find(senderName, true).First();
                                    axWindowsMediaPlayer1.Enabled = true;
                                    youBubbleMedia.Controls.Add(axWindowsMediaPlayer1);
                                    axWindowsMediaPlayer1.CreateControl();
                                    axWindowsMediaPlayer1.Size = new Size(youBubbleMedia.Size.Width, youBubbleMedia.Size.Height);

                                    axWindowsMediaPlayer1.Dock = DockStyle.Left;

                                    axWindowsMediaPlayer1.URL = tempFilePath;
                                    axWindowsMediaPlayer1.Ctlcontrols.play();





                                    youBubbleMedia.Dock = DockStyle.Fill;
                                    chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(youBubbleMedia);
                                    youBubbleMedia.Dock = DockStyle.Top;
                                    youBubbleMedia.BringToFront();
                                    youBubbleMedia.Focus();

                                    //axWindowsMediaPlayer.Ctlcontrols.play();
                                    if (File.Exists(tempFilePath))
                                    {
                                        File.Delete(tempFilePath);

                                    }

                                }
                            }));



                        }
                        else if (typeOfFile == "photo")
                        {
                            i = null;
                            MemoryStream ms = new MemoryStream(imageBytes);
                            i = Image.FromStream(ms);
                            Invoke(new Action(() =>
                            {


                                if (chatUserControl != null && chatUserControl.Name == senderName)
                                {
                                    PictureBox pictureBox = new PictureBox();

                                    YouBubbleMedia youBubbleMedia = new YouBubbleMedia();
                                    pictureBox.Image = i;
                                    pictureBox.Size = new Size(youBubbleMedia.Size.Width, youBubbleMedia.Size.Height);
                                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                                    pictureBox.Dock = DockStyle.Left;
                                    pictureBox.BorderStyle = BorderStyle.FixedSingle;

                                    youBubbleMedia.Controls.Add(pictureBox);
                                    youBubbleMedia.Dock = DockStyle.Fill;
                                    chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(youBubbleMedia);
                                    youBubbleMedia.Dock = DockStyle.Top;
                                    youBubbleMedia.BringToFront();
                                    youBubbleMedia.Focus();

                                }
                            }));
                        }
                        else
                        {
                            MessageBox.Show("Dosyanın türü bilinmiyor");
                        }





                        _photoChunks[TargetUsername].Clear();
                        _photoChunks.Remove(TargetUsername);
                        _photoChunks.Clear();
                    }


                });


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: " + ex.ToString());

            };

        }

        private void ShowChatElements(bool show)
        {
            if (show)
            {
                this.EmojiPanelMainPage.Show();
                this.EmojiPanelMainPage.Show();
                this.MainSendButtonPanel.Show();
                this.MainSendFilePanel.Show();
                this.MainTextboxPanel.Show();

                ShowEmojies();
            }
            else
            {
                this.EmojiPanelMainPage.Hide();
                this.EmojiPanelMainPage.Hide();
                this.MainSendButtonPanel.Hide();
                this.MainSendFilePanel.Hide();
                this.MainTextboxPanel.Hide();
       
            }
        }
        //private void ProfileButton_Click(object sender, EventArgs e)
        //{
        //    ShowChatElements(false);
        //    ProfileUserControl profileUserControl = new ProfileUserControl();

        //    if (MainPanelMainPage.Controls.ContainsKey("ProfilUserControl"))
        //    {
        //        MainPanelMainPage.Controls.Remove(profileUserControl);


        //    }
        //    profileUserControl = new ProfileUserControl()
        //    {
        //        Name = "ProfilUserControl"
        //    };
        //    this.MainPanelMainPage.Controls.Add(profileUserControl);
        //    profileUserControl.Dock = DockStyle.Fill;


        //    profileUserControl.BringToFront();
        //}
        private void ShowUserControl(Control controlToShow)
        {
            foreach (Control item in MainPanelMainPage.Controls)
            {
                item.Visible = false;
            }
            controlToShow.Visible = true;
            SelectedControl = chatUserControl.ChatScreenPanelChatUserControl;


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
        private void FriendList()
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
                        command.Parameters.AddWithValue("@USERID", UserID);

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
                            if (this.MainPanelMainPage.Controls.ContainsKey(friendsListMemberUserControl.Name))
                            {
                                TargetUsername = friendsListMemberUserControl.Name;
                                SelectedControl = MainPanelMainPage.Controls.Find(friendsListMemberUserControl.Name, true).FirstOrDefault();
                                MainPanelMainPage.Controls.Find(friendsListMemberUserControl.Name, true).FirstOrDefault();
                                chatUserControl = (ChatUserControl)MainPanelMainPage.Controls.Find(friendsListMemberUserControl.Name, true).First();
                                ShowUserControl(SelectedControl);

                                ShowChatElements(true);

                                return;
                            }
                            ShowChatElements(true);

                            chatUserControl = new ChatUserControl()
                            {
                                Name = friendsListMemberUserControl.Name
                            };
                            chatUserControl._username = UserName;

                            this.MainPanelMainPage.Controls.Add(chatUserControl);
                            chatUserControl.UserNameLabelChatUserControl.Text = friendsListMemberUserControl.Name;
                            chatUserControl.Visible = true;
                            chatUserControl.Dock = DockStyle.Fill;


                            TargetUsername = friendsListMemberUserControl.Name;
                         
                            ShowUserControl(chatUserControl);
                      

                        };

                        friendsListMemberUserControl.usernameFriendLabel.Text = Friends[i].ToString();
                        this.FriendListPanelMainPage.Controls.Add(friendsListMemberUserControl);
                        friendsListMemberUserControl.Dock = DockStyle.Top;
                        friendsListMemberUserControl.BringToFront();
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
        private void ChatButton_Click(object sender, EventArgs e)
        {

            #region oldCode
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
                        command.Parameters.AddWithValue("@USERID", UserID);

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
                            if (this.MainPanelMainPage.Controls.ContainsKey(friendsListMemberUserControl.Name))
                            {
                                TargetUsername = friendsListMemberUserControl.Name;
                                SelectedControl = MainPanelMainPage.Controls.Find(friendsListMemberUserControl.Name, true).FirstOrDefault();
                                MainPanelMainPage.Controls.Find(friendsListMemberUserControl.Name, true).FirstOrDefault();
                                chatUserControl = (ChatUserControl)MainPanelMainPage.Controls.Find(friendsListMemberUserControl.Name, true).First();
                                ShowUserControl(SelectedControl);



                                return;
                            }
                            this.EmojiPanelMainPage.Show();
                            this.EmojiPanelMainPage.Show();
                            this.MainSendButtonPanel.Show();
                            this.MainSendFilePanel.Show();
                            this.MainTextboxPanel.Show();
                          

                            chatUserControl = new ChatUserControl()
                            {
                                Name = friendsListMemberUserControl.Name
                            };

                            this.MainPanelMainPage.Controls.Add(chatUserControl);
                            chatUserControl.Visible = true;
                            chatUserControl.Name = friendsListMemberUserControl.Name;
                            chatUserControl.UserNameLabelChatUserControl.Text = friendsListMemberUserControl.Name;
                            chatUserControl.Dock = DockStyle.Fill;
                            // ShowUserControl(chatUserControl);

                            TargetUsername = friendsListMemberUserControl.Name;
                           

                            ShowUserControl(chatUserControl);
                            chatUserControl.Select();

                        };

                        friendsListMemberUserControl.usernameFriendLabel.Text = Friends[i].ToString();
                        this.FriendListPanelMainPage.Controls.Add(friendsListMemberUserControl);
                        friendsListMemberUserControl.Dock = DockStyle.Top;
                        friendsListMemberUserControl.BringToFront();
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
            #endregion
            FriendList();


        }

        private void FindNewButton_Click(object sender, EventArgs e)
        {

            ShowChatElements(false);
            FindNewUserControl findNewUserControl = new FindNewUserControl(UserName,UserID);
            this.MainPanelMainPage.Controls.Add(findNewUserControl);
            findNewUserControl.Dock = DockStyle.Fill;
            findNewUserControl.BringToFront();
        }

      
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


            if (chatUserControl != null && !string.IsNullOrEmpty(textboxChat.Text) && textboxChat.Text.Length < 250)
            {
                MeBubble meBubble = new MeBubble();
                meBubble.MessageLabel.Text = textboxChat.Text;
                MainPanelMainPage.Controls.Find(TargetUsername, true).First();
                chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(meBubble);
                SendMessage(TargetUsername, textboxChat.Text);
                meBubble.Size = new Size(326, meBubble.MessageLabel.Size.Height + 20);
                meBubble.Dock = DockStyle.Top;
                meBubble.BringToFront();
                meBubble.Focus();
                textboxChat.Text = "";
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

                //tipine göre dosyaları filtrele ve fotoraf mı video mu olduğunu kaydet ve server'a gönder.

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

                        if (chatUserControl != null && typeOfFile == "photo")
                        {
                            SendPhotoToServer(TargetUsername, UserName, filePath, typeOfFile);
                            MeBubbleMedia meBubbleMedia = new MeBubbleMedia();
                            PictureBox pictureBox = new PictureBox();
                            pictureBox.Image = Image.FromFile(filePath);
                            meBubbleMedia.Controls.Add(pictureBox);
                            pictureBox.Size = new Size(meBubbleMedia.Size.Width, meBubbleMedia.Size.Height);

                            meBubbleMedia.Dock = DockStyle.Fill;

                            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox.Dock = DockStyle.Right;
                            pictureBox.BorderStyle = BorderStyle.FixedSingle;
                            chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(meBubbleMedia);
                            meBubbleMedia.Dock = DockStyle.Top;
                            meBubbleMedia.BringToFront();
                            meBubbleMedia.Focus();
                        }
                        else if (chatUserControl != null && typeOfFile == "video")
                            try
                            {
                                AxWindowsMediaPlayer axWindowsMediaPlayer1 = new AxWindowsMediaPlayer();
                                MeBubbleMedia meBubbleMedia = new MeBubbleMedia();
                                axWindowsMediaPlayer1.Enabled = true;
                                meBubbleMedia.Controls.Add(axWindowsMediaPlayer1);
                                axWindowsMediaPlayer1.CreateControl();
                                axWindowsMediaPlayer1.Size = new Size(meBubbleMedia.Size.Width, meBubbleMedia.Size.Height);

                                axWindowsMediaPlayer1.Dock = DockStyle.Right;

                                axWindowsMediaPlayer1.URL = filePath;
                                axWindowsMediaPlayer1.Ctlcontrols.play();
                                SendPhotoToServer(TargetUsername, UserName, filePath, typeOfFile);




                                meBubbleMedia.Dock = DockStyle.Fill;
                                chatUserControl.ChatScreenPanelChatUserControl.Controls.Add(meBubbleMedia);
                                meBubbleMedia.Dock = DockStyle.Top;
                                meBubbleMedia.BringToFront();
                                meBubbleMedia.Focus();



                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show("Hata ile karşılaşıldı " + ex.ToString());
                            }
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}");
                    }

                }
            }
        }

        private void ShowEmojies()
        {
            if (EmojiPanelMainPage.Controls.Count > 0)
            {
                EmojiPanelMainPage.Controls.Clear();
            }
            Emoji emoji1 = new Emoji();

            foreach (var emoji in emoji1.emojies)
            {
                Button emojiButton = new Button
                {
                    Text = emoji,
                    Font = new Font("Segoe UI Emoji", 12),
                    Size = new Size(40, 40),
                    Margin = new Padding(5, 5, 5, 5)
                };

                // Emoji butonuna tıklama olayı
                emojiButton.Click += (s, e) =>
                {
                    textboxChat.Text += emojiButton.Text; // Emoji'yi TextBox'a ekle

                };

                EmojiPanelMainPage.Controls.Add(emojiButton);
            }


        }

        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {



        }

        
        private void FetchFriendRequests()
        {
            FriendRequestListUserControl friendRequestListUserControl = new FriendRequestListUserControl();
            if (MainPanelMainPage.Controls.ContainsKey("friendRequestListUserControl"))
            {
                MainPanelMainPage.Controls.Remove(friendRequestListUserControl);


            }
            friendRequestListUserControl = new FriendRequestListUserControl()
            {
                
                Name = "friendRequestListUserControl"
            };
            friendRequestListUserControl.Username = this.UserName;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Arkadaşlık sorgusu
                    string query1 = @"
            SELECT  
                u.USERNAME as TargetUserName
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
                o._CASE = @CASEFriend AND o.TARGET_USER_ID = @USERID";

                    using (SqlCommand command1 = new SqlCommand(query1, connection))
                    {
                        command1.Parameters.AddWithValue("@CASEFriend", 'W');
                        command1.Parameters.AddWithValue("@USERID", UserID);

                        connection.Open();

                        using (SqlDataReader reader1 = command1.ExecuteReader())
                        {
                            while (reader1.Read())
                            {
                                string friendName = reader1["TargetUserName"].ToString();
                                FriendsReq.Add(friendName);
                            }
                        }
                    }

                    // Bloklu kullanıcı sorgusu
                    string query2 = @"
            SELECT  
                u2.USERNAME as TargetUserName
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
                o._CASE = @CASE AND o.USER_ID = @USERID";

                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@CASE", 'B');
                        command2.Parameters.AddWithValue("@USERID", UserID);

                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                string friendName = reader2["TargetUserName"].ToString();
                                BlockedFriends.Add(friendName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hata: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }




            for (int i = 0; i < FriendsReq.Count; i++)
                    {

                        RadioButton radioButton = new RadioButton
                        {
                            Name = $"{FriendsReq[i]}",
                            Text = FriendsReq[i],
                            Cursor = Cursors.Hand,
                            BackColor = Color.WhiteSmoke,

                        };
                radioButton.CheckedChanged += (sender, args) =>
                {
                    if (radioButton.Checked)
                    {
                        SelectedReqUsername= radioButton.Text;
                    }
                };

                friendRequestListUserControl.FriendReqListGroupBox.Controls.Add(radioButton);
                radioButton.Dock = DockStyle.Top;


            }

                   
                    for (int i = 0; i < BlockedFriends.Count; i++)
                    {

                        RadioButton radioButton = new RadioButton
                        {
                            Name = $"{BlockedFriends[i]}",
                            Text = BlockedFriends[i],
                            Cursor = Cursors.Hand,
                            BackColor = Color.WhiteSmoke,

                        };
                radioButton.CheckedChanged += (sender, args) =>
                {
                    if (radioButton.Checked) 
                    {
                        SelectedUserForBlock = radioButton.Text;
                        
                    }
                };
                friendRequestListUserControl.BlockedUserListGroupBox.Controls.Add(radioButton);
                        radioButton.Dock = DockStyle.Top;


                    }
                    FriendsReq.Clear();
                    BlockedFriends.Clear();
                    MainPanelMainPage.Controls.Add(friendRequestListUserControl);
                    friendRequestListUserControl.Dock = DockStyle.Fill;
                    friendRequestListUserControl.BringToFront();
                
             
            
        }
        private void FriendReqButton_Click(object sender, EventArgs e)
        {
            ShowChatElements(false);

            FetchFriendRequests();

        }
    }
}
