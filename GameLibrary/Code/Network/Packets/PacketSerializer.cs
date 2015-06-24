using System;
using System.IO;

namespace Faseway.GameLibrary.Network.Packets
{
    public class PacketSerializer
    {
        // Variables
        private readonly object _locker;

        // Constructor
        public PacketSerializer()
        {
            _locker = new object();
        }

        // Methods
        public void Serialize(Packet packet, Stream stream)
        {
            var buffer = new MemoryStream();
            var writer = new BinaryWriter(buffer);

            writer.Write(packet.Timestamp);
            writer.Write(packet.Header);

            foreach (object frame in packet.Buffer)
            {
                if (frame.GetType() == typeof(bool))
                {
                    writer.Write((bool)frame);
                }
                else if (frame.GetType() == typeof(byte))
                {
                    writer.Write((byte)frame);
                }
                else if (frame.GetType() == typeof(byte[]))
                {
                    writer.Write((byte[])frame);
                }
                else if (frame.GetType() == typeof(char))
                {
                    writer.Write((char)frame);
                }
                else if (frame.GetType() == typeof(char[]))
                {
                    writer.Write((char[])frame);
                }
                else if (frame.GetType() == typeof(decimal))
                {
                    writer.Write((decimal)frame);
                }
                else if (frame.GetType() == typeof(double))
                {
                    writer.Write((double)frame);
                }
                else if (frame.GetType() == typeof(float))
                {
                    writer.Write((float)frame);
                }
                else if (frame.GetType() == typeof(int))
                {
                    writer.Write((int)frame);
                }
                else if (frame.GetType() == typeof(long))
                {
                    writer.Write((long)frame);
                }
                else if (frame.GetType() == typeof(sbyte))
                {
                    writer.Write((sbyte)frame);
                }
                else if (frame.GetType() == typeof(short))
                {
                    writer.Write((short)frame);
                }
                else if (frame.GetType() == typeof(string))
                {
                    writer.Write((string)frame);
                }
                else if (frame.GetType() == typeof(uint))
                {
                    writer.Write((uint)frame);
                }
                else if (frame.GetType() == typeof(ulong))
                {
                    writer.Write((ulong)frame);
                }
                else if (frame.GetType() == typeof(ushort))
                {
                    writer.Write((ushort)frame);
                }
            }

            byte[] data = buffer.ToArray();
            lock (_locker)
            {
                stream.Write(data, 0, data.Length);
            }
        }
    }
}
