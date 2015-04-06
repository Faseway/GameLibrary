using System;
using System.Net;
using System.Net.Sockets;

namespace Faseway.GameLibrary.Network
{
    public interface IConnection
    {
        IPEndPoint EndPoint { get; }
    }
}
