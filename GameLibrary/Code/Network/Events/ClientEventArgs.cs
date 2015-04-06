using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Faseway.GameLibrary.Network.Events
{
    public class ClientEventArgs : EventArgs
    {
        // Properties
        public IConnection Connection { get; private set; }
        public Client Client { get; private set; }

        // Constructor
        public ClientEventArgs(Client client)
        {
            Connection = client;
            Client = client;
        }
    }
}
