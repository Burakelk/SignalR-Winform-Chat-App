using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR_Server
{
    public class ChatHub : Hub
    {
        // Kullanıcılar: KullanıcıAdı -> ConnectionId eşlemesi
        private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

        public override Task OnConnected()
        {
          
            Console.WriteLine($"New connection: {Context.ConnectionId}");
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            // Kullanıcıyı bağlantıdan çıkar
            var username = Users.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (!string.IsNullOrEmpty(username))
            {
                Users.TryRemove(username, out _);
                Console.WriteLine($"server : {username}  { Context.ConnectionId} disconnected.");
            }

            return base.OnDisconnected(stopCalled);
        }

        public void RegisterUser(string username)
        {
            // Kullanıcıyı kaydet
            if (Users.ContainsKey(username) || Users.Values.Contains(Context.ConnectionId))
            {
                Console.WriteLine($"Bu kullanıcı zaten var giriş yapılmadı: {username} ({Context.ConnectionId})");
                SendMessageToUser("server",username, "Bu kullanıcı şuan aktif.");
                return ;
            }
            Users[username] = Context.ConnectionId;
            Console.WriteLine($"User registered: {username} ({Context.ConnectionId})");
        }

        public void SendMessageToUser(string sender, string receiver, string message)
        {
            if (Users.TryGetValue(receiver, out var connectionId))
            {
                // Hedef kullanıcıya mesaj gönder
                Clients.Client(connectionId).receiveMessage(sender, message);
                Console.WriteLine($"Message from {sender} to {receiver}: {message}");
            }
            else
            {
                // Kullanıcı bulunamazsa hata mesajı gönder
                Clients.Client(Context.ConnectionId).receiveMessage("server", $"{receiver} is not online.");
            }
        }

        public void SendRequestToUser(string sender, string receiver)
        {
            if (Users.TryGetValue(receiver, out var connectionId))
            {

                Clients.Client(connectionId).receiveRequest(sender,'W');   // W = wait for accept friend request
                Console.WriteLine($"Friend request from {sender} to {receiver} ");
            }
            else
            {
                // Kullanıcı bulunamazsa hata mesajı gönder
                Clients.Client(Context.ConnectionId).receiveMessage("server", $"{receiver} is not online.");
            }
        }

        public async Task SendMediaToUser(string senderUsername, string receiverUsername, string chunkData, int chunkCount, string typeOfFile)
        {
            // Alıcının bağlantı ID'sini al
            var receiverConnectionId = GetConnectionIdByUsername(receiverUsername);
            Console.WriteLine(chunkData.Length);
            // Hedef kullanıcıya fotoğraf parçasını gönder
            await Clients.Client(receiverConnectionId).receiveMedia(senderUsername, chunkData, chunkCount, typeOfFile);

        }

        // Kullanıcının bağlantı ID'sini almak için bir metod (Örnek)
        private string GetConnectionIdByUsername(string username)
        {
            Users.TryGetValue(username, out var connectionId);



            return connectionId;
        }

    }
}
