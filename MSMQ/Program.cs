using Experimental.System.Messaging;
using System;

namespace MSMQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var listener = new Listener(@".\Private$\MyQueue");
            listener.MessageReceived += new MessageReceivedEventHandler(listener_MessageReceived);
            listener.Start();
            Console.WriteLine("Read Message");
            Console.ReadLine();
            listener.Stop();
        }

        public static void listener_MessageReceived(object sender, MessageEventArgs args)
        {
            Console.WriteLine(args.MessageBody);
        }
    }
}
