using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace Auxilo_WP
{
    public class MessageWrapper
    {
        private string ipAddressString = "95.85.11.190";
        private int port = 5000;

        Socket socket;

        // Signaling object used to notify when an asynchronous operation is completed
        static ManualResetEvent clientDone = new ManualResetEvent(false);

        // Define a timeout in milliseconds for each asynchronous call. If a response is not received within this 
        // timeout period, the call is aborted.
        const int TIMEOUT_MILLISECONDS = 5000;

        // The maximum size of the data buffer to use with the asynchronous socket methods
        const int MAX_BUFFER_SIZE = 520; // Header + Body = (4 + 4) + 512

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Constructor.
        /// </summary>
        /// 
        //////////////////////////////////////////////////////////////
        public MessageWrapper(string ip, int port)
        {
            this.ipAddressString = ip;
            this.port = port;

        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Initializes the communication to the server.
        /// </summary>
        /// 
        //////////////////////////////////////////////////////////////
        public string Initialize()
        {
            
            string result = "";

            IPAddress IP_ADDRESS = IPAddress.Parse(ipAddressString);
            IPEndPoint endPoint = new IPEndPoint(IP_ADDRESS, port);

            socket = new Socket(IP_ADDRESS.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
            socketEventArg.RemoteEndPoint = endPoint;

            // Inline event handler for the Completed event.
            // Note: This event handler was implemented inline in order to make this method self-contained.
            socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
            {
                // Retrieve the result of this request
                result = e.SocketError.ToString();

                // Signal that the request is complete, unblocking the UI thread
                clientDone.Set();
            });

            // Sets the state of the event to nonsignaled, causing threads to block
            clientDone.Reset();

            // Make an asynchronous Connect request over the socket
            socket.ConnectAsync(socketEventArg);

            // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
            // If no response comes back within this time then proceed
            clientDone.WaitOne(TIMEOUT_MILLISECONDS);

            return result;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Sends the parsed message to the server.
        /// </summary>
        /// 
        /// <param name="message">The message (with header) in a byte array</param>
        /// 
        //////////////////////////////////////////////////////////////
        public string Send(byte[] message)
        {
            string response = "Operation Timeout";

            // We are re-using the socket object initialized in the Connect method
            if (socket != null)
            {
                // Create SocketAsyncEventArgs context object
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();

                // Set properties on context object
                socketEventArg.RemoteEndPoint = socket.RemoteEndPoint;
                socketEventArg.UserToken = null;

                // Inline event handler for the Completed event.
                // Note: This event handler was implemented inline in order to make this method self-contained.
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    response = e.SocketError.ToString();

                    // Unblock the UI thread
                    clientDone.Set();
                });

                // Add the data to be sent into the buffer
                socketEventArg.SetBuffer(message, 0, message.Length);

                // Sets the state of the event to nonsignaled, causing threads to block
                clientDone.Reset();

                // Make an asynchronous Send request over the socket
                socket.SendAsync(socketEventArg);

                // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
                // If no response comes back within this time then proceed
                clientDone.WaitOne(TIMEOUT_MILLISECONDS);
            }
            else
            {
                response = "Socket is not initialized";
            }

            return response + " Test";
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Receives data from the server.
        /// </summary>
        /// 
        //////////////////////////////////////////////////////////////
        public byte[] Receive()
        {
            byte[] response = null;

            // We are receiving over an established socket connection
            if (socket != null)
            {
                // Create SocketAsyncEventArgs context object
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = socket.RemoteEndPoint;

                // Setup the buffer to receive the data
                socketEventArg.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);

                // Inline event handler for the Completed event.
                // Note: This even handler was implemented inline in order to make 
                // this method self-contained.
                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                {
                    if (e.SocketError == SocketError.Success)
                    {
                        // Retrieve the data from the buffer
                        response = e.Buffer;
                    }
                    else
                    {
                        response = null;
                    }

                    clientDone.Set();
                });

                // Sets the state of the event to nonsignaled, causing threads to block
                clientDone.Reset();

                // Make an asynchronous Receive request over the socket
                socket.ReceiveAsync(socketEventArg);

                // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
                // If no response comes back within this time then proceed
                clientDone.WaitOne(TIMEOUT_MILLISECONDS);
            }
            else
            {
                response = null;
            }

            return response;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Closes the Socket connection and releases all associated resources
        /// </summary>
        /// 
        //////////////////////////////////////////////////////////////
        public void Close()
        {
            if (socket != null)
            {
                socket.Close();
            }
        }

    }
}
