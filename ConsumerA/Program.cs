using EasyNetQ;
using System;

namespace ConsumerA
{
    public static class Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;prefetchcount=1"))
            {
                bus.PubSub.Subscribe<TextMessage>("consumerA", HandleTextMessage);
                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleTextMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            Console.ResetColor();
        }
    }

    public class TextMessage
    {
        public string Text { get; set; }
    }
}
