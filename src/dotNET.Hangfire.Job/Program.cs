using dotNET.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace dotNET.Hangfire.Job
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .UseUrls(Zconfig.Getconfig("hangfireurl")).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}