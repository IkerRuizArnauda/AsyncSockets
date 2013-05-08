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
    class Client
    {
        //Client Name
        private String _name;
        public String Name { get { return _name; } set { _name = value; } }

        //Client Socket
        private Socket _clientSocket;
        public Socket ClientSocket { get { return _clientSocket; } set { _clientSocket = value; } }

        //Client Buffer
        private byte[] _clientBuffer;
        public byte[] ClientBuffer { get { return _clientBuffer; } set { _clientBuffer = value; } }

        //Client Logger
        Logger Logger = LogManager.CreateLogger();

        /// <summary>
        /// Creates a client with a name and a socket.
        /// This should allow us to communicate with a particular client at some point.
        /// Todo: Implemenet GUID instead of name?
        /// </summary>
        /// <param name="name"></param>
        /// <param name="socket"></param>
        public Client(String name ,Socket socket)
        {
            this.Name = name;
            this.ClientSocket = socket;
        }

        //Listen to this particular client socket communications.
        public void Listen()
        {
            this.ClientSocket.BeginReceive(ClientBuffer, 0, ClientBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
        }

        //Recieve the data on the client buffer.
        private void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                //Exact length of our recieved buffer.
                int received = ClientSocket.EndReceive(AR);

                //If we did not recieve anything, we return so we dont iterate over an empty buffer over and over again.
                if (received == 0)
                    return;

                //Get rid of null chars
                Array.Resize(ref _clientBuffer, received);

                //Get string from our buffer
                string text = Encoding.ASCII.GetString(ClientBuffer);

                //Print the data
                Print(text);

                //Buffer back to its original size.
                Array.Resize(ref _clientBuffer, ClientSocket.ReceiveBufferSize);   
                     
                //Listen Again
                ClientSocket.BeginReceive(ClientBuffer, 0, ClientBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                Logger.DebugException(ex, ex.Message);
            }
        }

        /// <summary>
        /// Data Recieved Print
        /// </summary>
        /// <param name="text"></param>
        private void Print(string text)
        {
            Logger.Info("Recieved data from: " + ClientSocket.RemoteEndPoint + " & ClientId:" + this._name);
            Logger.Debug("Data Recieved: " + text);
        }
    }
}
