using System;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Threading;

using Faseway.GameLibrary;
using Faseway.GameLibrary.Extra;
using Faseway.GameLibrary.Logging;
using Faseway.GameLibrary.Network.Events;
using Faseway.GameLibrary.Network.Packets;

using Timer = System.Timers.Timer;

namespace Faseway.GameLibrary.Network
{
    public class Client : IConnection
    {
        // Variables
        private readonly Socket _socket;
        private readonly Thread _thread;
        private readonly Timer _timer;

        // Properties
        /// <summary>
        /// Gets the remote end point.
        /// </summary>
        public IPEndPoint EndPoint
        {
            get { return (IPEndPoint)_socket.RemoteEndPoint; }
        }
        /// <summary>
        /// Gets a value idicating whether the client is connected.
        /// </summary>
        public bool IsConnected
        {
            get { return _socket.Connected; }
        }

        // Events
        /// <summary>
        /// Occurs when the client connectes.
        /// </summary>
        public event EventHandler<ClientEventArgs> Connected;
        /// <summary>
        /// Occurs when the client disconnects.
        /// </summary>
        public event EventHandler<ClientEventArgs> Disconnected;
        /// <summary>
        /// Occurs when the client recieved a packet.
        /// </summary>
        public event EventHandler<PacketEventArgs> RecievedPacket;
        /// <summary>
        /// Occurs when the client sent a packet.
        /// </summary>
        public event EventHandler<PacketEventArgs> SentPacket;

        // Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Network.Client"/> class.
        /// </summary>
        public Client()
            : this(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Faseway.GameLibrary.Network.Client"/> class.
        /// <param name="socket">The <see cref="System.Net.Sockets.Socket"/> to be connected with.</param>
        /// </summary>
        public Client(Socket socket)
        {
            _socket = socket;
            _thread = new Thread(Receive);
            _timer = new Timer();
        }

        // Methods
        #region Public

        /// <summary>
        ///  Establishes a connection to a remote host. The host is specified by a host
        ///  name and a port number.
        /// </summary>
        /// <param name="host">The name of the remote host.</param>
        /// <param name="port">The port number of the remote host.</param>
        /// <returns>Returns true if the client is connected. Otherwise, false.</returns>
        public void Connect(string host, int port)
        {
            try
            {
                _socket.Connect(host, port);

                Listen();
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10061)
                {
                    Logger.Log("Client coult not connect to {0}:{1}", host, port);
                }
                else
                {
                    Logger.Log("Client coult not connect to {0}:{1} (Code: {2})", host, port, ex.ErrorCode);
                    Logger.Log(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Client coult not connect to {0}:{1}", host, port);
                Logger.Log(ex.Message);
            }

        }

        /// <summary>
        /// Starts listening.
        /// </summary>
        public void Listen()
        {
            if (!IsConnected) return;

            // start the receive thread
            _thread.Name = string.Format("<Faseway:GameLibrary:Client-{0}>", _socket.RemoteEndPoint);
            _thread.IsBackground = true;
            _thread.Start();

            // start timer
            _timer.Interval = 5000;
            _timer.Elapsed += new ElapsedEventHandler(OnElapsed);
            _timer.Start();
        }

        /// <summary>
        /// Sends the specified number of bytes of data to a connected <see cref="System.Net.Sockets.Socket"/>,
        /// using the specified <see cref="System.Net.Sockets.SocketFlags"/>.
        /// </summary>
        /// <param name="buffer">An array of type <see cref="System.Byte"/> that contains the data to be sent.</param>
        /// <returns>The number of bytes sent to the <see cref="System.Net.Sockets.Socket"/>.</returns>
        private int Send(byte[] buffer)
        {
            if (!IsConnected) return 0;

            return _socket.Send(buffer, buffer.Length, SocketFlags.None);
        }

        ///// <summary>
        ///// Sends the specified <see cref="Faseway.Service.GameService.Network.Packet"/>.
        ///// </summary>
        ///// <param name="packet">The <see cref="Faseway.Service.GameService.Network.Packet"/> to send.</param>
        ///// <returns>The number of bytes sent to the <see cref="System.Net.Sockets.Socket"/>.</returns>
        //public int Send(Packet packet)
        //{
        //    OnSentPacket(packet);

        //    return Send(packet.GetBytes());
        //}

        /// <summary>
        /// Closes the <see cref="System.Net.Sockets.Socket"/> connection, releases all associated resources
        /// and terminates the connection.
        /// </summary>
        public void Disconnect()
        {
            if (!IsConnected) return;

            OnDisconnected();

            _timer.Stop();
            _socket.Close();
        }

        #endregion

        #region Protected

        protected virtual void OnElapsed(object sender, ElapsedEventArgs e)
        {

        }

        protected virtual void OnConnected()
        {
            Connected.SafeInvoke(this, new ClientEventArgs(this));
        }

        protected virtual void OnDisconnected()
        {
            Disconnected.SafeInvoke(this, new ClientEventArgs(this));
        }

        protected virtual void OnReceive(byte[] buffer)
        {
            //var decoded = Handler.Encoding.Decode(buffer);
            //var packet = new Packet(decoded);

            //OnRecievedPacket(packet);
            //Handler.InvokePacket(this, packet);
        }

        protected virtual void OnRecievedPacket(Packet packet)
        {
#if DEBUG
            Logger.Log("Packet -> {0}", packet.GetString());
#endif

            RecievedPacket.SafeInvoke(this, new PacketEventArgs(this, packet, PacketDirection.Incoming));
        }

        protected virtual void OnSentPacket(Packet packet)
        {
#if DEBUG
            Logger.Log("Packet <- {0}", packet.GetString());
#endif

            SentPacket.SafeInvoke(this, new PacketEventArgs(this, packet, PacketDirection.Outgoing));
        }

        #endregion

        #region Private

        /// <summary>
        /// Receives data from a bound <see cref="System.Net.Sockets.Socket"/>.
        /// </summary>
        private void Receive()
        {
            OnConnected();

            while (IsConnected)
            {
                try
                {
                    var buffer = new byte[2048];
                    var len = _socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);

                    if (len < 1) break;

                    OnReceive(buffer);
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode == 10053 || ex.ErrorCode == 10054) continue;

                    Logger.Log("Client receive error (code: {0})", ex.ErrorCode);
                    Logger.Log(ex.Message);
                    Logger.Log(ex.StackTrace);
                }
                catch (Exception ex)
                {
                    Logger.Log("Client receive error (code: {0})", 0);
                    Logger.Log(ex.Message);
                    Logger.Log(ex.StackTrace);
                }
            }

            Disconnect();
        }

        #endregion
    }
}
