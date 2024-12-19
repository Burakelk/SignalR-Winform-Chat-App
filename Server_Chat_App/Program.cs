using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;



namespace Server_Chat_App
{

    class Program
    {
        private const string Url = "http://localhost:8080";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting SignalR Chat Server...");
            using (WebApp.Start(Url, Configuration))
            {
                Console.WriteLine($"Server running at {Url}");
                Console.ReadLine(); // Sunucunun açık kalması için
            }
        }

        public static void Configuration(IAppBuilder app)
        {
            GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(30); // 30 saniye bağlantı süresi
            GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(60); // 60 saniye kopma süresi
            GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(10); // 10 saniyede bir keep-alive gönder
            app.MapSignalR();

        }
    }



}