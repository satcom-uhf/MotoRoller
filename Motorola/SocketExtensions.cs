using System.Collections.Generic;
using System.Net.Sockets;

namespace Motorola
{
    internal static class SocketExtensions
    {

        public static IEnumerable<Socket> IncommingConnections(this Socket server)
        {
            while (true)
                yield return server.Accept();
        }

    }
}
