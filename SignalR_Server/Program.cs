
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.AspNet.SignalR;
using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;

using System.Linq;

using System.Threading.Tasks;
namespace SignalR_Server
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
