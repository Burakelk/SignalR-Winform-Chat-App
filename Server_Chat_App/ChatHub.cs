using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annesikasar_Server
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
                Console.WriteLine($"{username} disconnected.");
            }

            return base.OnDisconnected(stopCalled);
        }

        public void RegisterUser(string username)
        {
            // Kullanıcıyı kaydet
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
