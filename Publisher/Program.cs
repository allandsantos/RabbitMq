using Abee.Erp.ServiceBus.Events;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Publisher
{
    public static class Program
    {
        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            RunRabbit(host.Services);

            return host.RunAsync();
            
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.RegisterEasyNetQ("host=localhost");
                });


        static void RunRabbit(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var bus = provider.GetRequiredService<IBus>();
            var input = String.Empty;
            Console.WriteLine("Enter a message. 'Quit' to quit.");
            while ((input = Console.ReadLine()) != "Quit")
            {
                bus.PubSub.Publish(new CompanyCreated(
                    "C001",
                    "13451654",
                    "Abaré",
                    "LTDA"));

                Console.WriteLine("Message published!");
            }
        }
    }

    public class TextMessage
    {
        public string Text { get; set; }
    }
}
