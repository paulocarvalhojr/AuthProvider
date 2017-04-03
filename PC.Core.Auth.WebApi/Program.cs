using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace PC.Core.Auth.WebApi
{
	public class Program
    {
        public static void Main(string[] args)
        {
			var webhost = new WebHostBuilder()
				.UseKestrel()
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseIISIntegration()
				.UseStartup<Startup>();

#if DEBUG
			webhost.UseUrls("http://*:5000");
#endif

			var host = webhost.Build();
			host.Run();
        }
    }
}
