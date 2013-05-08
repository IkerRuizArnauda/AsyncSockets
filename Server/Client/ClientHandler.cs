/* Author: Iker Ruiz Arnauda
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using AsyncSimpleServer.Logging;

namespace AsyncSimpleServer.Server.Client
{
    class ClientHandler
    {
        //Clients collection
        private Dictionary<Socket, Client> _clients = new Dictionary<Socket, Client>();
        public Dictionary<Socket, Client> Clients { get { return _clients; } set { _clients = value; } }

        //Client Socket
        private Socket _clientSocket;
        public Socket ClientSocket { get { return _clientSocket; } set { _clientSocket = value; } }

        //Random number nickname for testing.
        Random rand = new Random();

        //ClientHandler Logger
        Logger Logger = LogManager.CreateLogger();

        //Ctor
        public ClientHandler()
        { }

        //Adds client to collection
        public void AddClient(Socket socket)
        {
            Clients.Add(socket,new Client(rand.Next(1000,2000).ToString(),socket));
        }

        //Checks for client within our collection
        public bool HasClient(Socket socket)
        {
            return Clients.ContainsKey(socket);
        }
    }
}
