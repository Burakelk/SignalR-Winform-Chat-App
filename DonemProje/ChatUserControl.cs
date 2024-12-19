using Microsoft.AspNet.SignalR.Client;
using System;

using System.ComponentModel;
using System.Data;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DonemProje
{
    public partial class ChatUserControl : UserControl
    {
        int MeBubbleCount = 0;
        public ChatUserControl()
        {
            InitializeComponent();
        }
        private HubConnection _connection;
        private IHubProxy _hubProxy;
        private string _username;
        private static Dictionary<string, List<string>> _photoChunks = new Dictionary<string, List<string>>();
        private void ChatUserControl_Load(object sender, EventArgs e)
        {

        }
        private async void SendMessage(string receiver, string message)
        {
            try
            {
                // Sunucuya özel mesaj gönder
                await _hubProxy.Invoke("SendMessageToUser", _username, receiver, message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message send error: {ex.Message}");
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
           
            MeBubble meBubble = new MeBubble
            {
                Name = $"MeBubble_{MeBubbleCount}",

            };
            meBubble.label1.Text = ChatTextbox.Text;
            meBubble.label1.Dock = DockStyle.Right;
            meBubble.Size = new Size(670, meBubble.label1.Height + 5);
        
            ChatPanelChatUserControl.Controls.Add(meBubble);
            meBubble.Location=new Point(meBubble.Location.X+500,meBubble.Location.Y -20);
            meBubble.Dock = DockStyle.Top;
          
            meBubble.BringToFront();
            meBubble.Focus();
            MeBubbleCount++;

        }

       
    }
}
