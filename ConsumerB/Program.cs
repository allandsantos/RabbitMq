using EasyNetQ;
using Messages;
using System;

namespace ConsumerB
{
    public static class  Program
    {
        static void Main(string[] args)
        {
            using (var bus = RabbitHutch.CreateBus("host=localhost;prefetchcount=1"))
            {
                bus.PubSub.Subscribe<TextMessage>("consumerB", HandleTextMessage);
                Console.WriteLine("Listening for messages. Hit <return> to quit.");
                Console.ReadLine();
            }
        }

        static void HandleTextMessage(TextMessage textMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Got message: {0}", textMessage.Text);
            Console.ResetColor();
        }
    }
}
