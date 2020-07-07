using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotProject
{
    public class Sender
    {
        // Method For Message 
        public void Message(string SendMessage)
        {
            //Declaring Message Queue in the manager
            MessageQueue MyQueue;

            if (MessageQueue.Exists(@".\Private$\myQueue"))
            {
                MyQueue = new MessageQueue(@".\Private$\myQueue");
            }
            else
            {
                MyQueue = MessageQueue.Create(@".\Private$\myQueue");
            }

            //Message
            Message MyMessage = new Message();

            //Message in Binary formate
            MyMessage.Formatter = new BinaryMessageFormatter();

            //Message body
            MyMessage.Body = SendMessage;

            //Message lable
            MyMessage.Label = "UserRegistration";

            //Message fetched on the basis of priority
            MyMessage.Priority = MessagePriority.Normal;

            //Message send to the Queue
            MyQueue.Send(MyMessage);
        }
    }
}
