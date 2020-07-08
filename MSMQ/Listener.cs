using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Experimental.System.Messaging;

namespace MSMQ
{
    //delegate initialized
    public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs args);

    public class Listener
    {
        private bool _listen;
        MessageQueue _queue;

        //Event declared
        public event MessageReceivedEventHandler MessageReceived;

        //constructor define
        public Listener(string queuePath)
        {
            _queue = new MessageQueue(queuePath);
        }

        //Method for receiving data from msmq
        private void StartListening()
        {
            if (!_listen)
            {
                return;
            }

            // The MSMQ class does not have a BeginRecieve method that can take in a 
            // MSMQ transaction object. This is a workaround – we do a BeginPeek and then 
            // recieve the message synchronously in a transaction.
            if (_queue.Transactional)
            {
                _queue.BeginPeek();
            }
            else
            {
                _queue.BeginReceive();

            }
        }

        // method for sending mail 
        private void FireRecieveEvent(object body)
        {
            SMTP Mail = new SMTP();

            if (MessageReceived != null)
            {
                MessageReceived(this, new MessageEventArgs(body));
                string message = body.ToString();
                Mail.SendMail(message);
            }
        }

        //method for recieving data from queue
        private void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            Message msg = _queue.EndReceive(e.AsyncResult);

            StartListening();

            FireRecieveEvent(msg.Body);
        }

        
        private void OnPeekCompleted(object sender, PeekCompletedEventArgs e)
        {
            _queue.EndPeek(e.AsyncResult);
            MessageQueueTransaction trans = new MessageQueueTransaction();
            Message msg = null;
            try
            {
                trans.Begin();
                msg = _queue.Receive(trans);
                trans.Commit();

                StartListening();

                FireRecieveEvent(msg.Body);
            }
            catch
            {
                trans.Abort();
            }
        }

        public void Start()
        {
            _listen = true;

            _queue.Formatter = new BinaryMessageFormatter();
            _queue.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);
            _queue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveCompleted);

            StartListening();
        }

        public void Stop()
        {
            _listen = false;
            _queue.PeekCompleted -= new PeekCompletedEventHandler(OnPeekCompleted);
            _queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(OnReceiveCompleted);
        }
    }
}
