using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace PC.Core.Commerce
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
			webhost.UseUrls("http://*:3000");
#endif

			var host = webhost.Build();
			host.Run();
        }
    }
}
