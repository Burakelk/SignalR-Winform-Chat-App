using Microsoft.AspNet.SignalR.Client;

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DonemProje
{
    public partial class FindNewUserControl : UserControl
    {
        private ChatUserControl chatUserControl;
        private HubConnection _connection;
        private IHubProxy _hubProxy;
        public FindNewUserControl()
        {
            InitializeComponent();
        }

        private void FindNewUserControl_Load(object sender, EventArgs e)
        {

        }

        private async void findFriendsButton_Click(object sender, EventArgs e)
        {


            try
            {
                // Sunucuya özel mesaj gönder
                await _hubProxy.Invoke("SendMessageToUser");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message send error: {ex.Message}");
            }

        }

        private void findFriendsButton_Click_1(object sender, EventArgs e)
        {
            

           // mainPage.SendFriendRequest();
        }
    }
}
