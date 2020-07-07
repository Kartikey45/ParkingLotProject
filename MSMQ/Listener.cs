using Experimental.System.Messaging;
using System;

namespace MSMQ
{
    public class Listener
    {
        public static void Main(string[] args)
        {
            //create object of smtp class 
            SMTP smtp = new SMTP();

            Console.WriteLine("Message : ");

            // Message queue
            MessageQueue MyQueue;

            MyQueue = new MessageQueue(@".\Private$\MyQueue");

            //message recieve from the Queue
            Message MyMessage = MyQueue.Receive();

            //Message in binary formate
            MyMessage.Formatter = new BinaryMessageFormatter();

            //Mail send 
            smtp.SendMail(MyMessage.Body.ToString());

            //Print message of the body
            Console.WriteLine(MyMessage.Body.ToString());

            Console.ReadLine();
        }
    }
}
