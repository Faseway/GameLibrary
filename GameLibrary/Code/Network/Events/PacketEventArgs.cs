using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Faseway.GameLibrary.Network;
using Faseway.GameLibrary.Network.Packets;

namespace Faseway.GameLibrary.Network.Events
{
    public class PacketEventArgs : EventArgs
    {
        // Properties
        public IConnection Connection { get; private set; }
        public Packet Packet { get; private set; }
        public PacketDirection Direction { get; private set; }

        // Constructor
        public PacketEventArgs(IConnection connection, Packet packet, PacketDirection direction)
        {
            Connection = connection;
            Packet = packet;
            Direction = direction;
        }
    }
}
