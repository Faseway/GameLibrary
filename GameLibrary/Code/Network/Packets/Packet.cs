using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Network.Packets
{
    public class Packet
    {
        // Variables
        private readonly List<object> _objects;
        private int _index;

        // Properties
        public int ParameterCount { get; set; }
        public int Timestamp { get; set; }
        public int Header { get; set; }

        public List<object> Buffer { get; set; }

        // Construtor
        public Packet()
        {
            _objects = new List<object>();
            _index = 0;

            Buffer = new List<object>();
        }

        // Methods
        public string GetString()
        {
            var packet = string.Format("{0} {1}", Timestamp, Header);
            foreach (var frame in Buffer)
            {
                packet += frame + " ";
            }
            return packet;
        }
    }
}
