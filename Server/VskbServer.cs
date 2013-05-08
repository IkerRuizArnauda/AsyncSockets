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
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using AsyncSimpleServer.Server.Client;
using AsyncSimpleServer.Logging;

namespace AsyncSimpleServer.Server
{
    public class VskbServer
    {
        //Server Socket
        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Client Collection
        ClientHandler ClientManager = new ClientHandler();
        //Server Logger
        Logger Logger = LogManager.CreateLogger();

        public VskbServer()
        {
            //Start the server
            StartServer();
        }

        private void StartServer()
        {
            try
            {
                //Bind our socket to an IP and Port
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any,3333));
                //Set it to listen for incoming connections, has a 10 queue buffer.
                _serverSocket.Listen(10);

                //If our server is correctly bound.
                if(_serverSocket.IsBound)
                    Logger.Info("Server is running and waiting for connections...");

                //Start accepting connections.
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception ex)
            {
                Logger.DebugException(ex, ex.Message);
            }
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            try
            {
                Socket _clientSocket = _serverSocket.EndAccept(AR);
                var remoteIpEndPoint = _clientSocket.RemoteEndPoint as IPEndPoint;

                //If this client is unknown to the server, add it to our clients collection.
                if (!ClientManager.HasClient(_clientSocket))
                {
                    ClientManager.AddClient(_clientSocket);                  
                }

                //Set client buffer size.
                ClientManager.Clients[_clientSocket].ClientBuffer = new byte[ClientManager.Clients[_clientSocket].ClientSocket.ReceiveBufferSize];
                //Listen to particular client socket.
                ClientManager.Clients[_clientSocket].Listen();

                Logger.Trace("Client connected from: " + remoteIpEndPoint.Address + ".");

                //Restart the cycle so we can accept new connections.
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);              
            }
            catch (Exception ex)
            {
                Logger.DebugException(ex, ex.Message);
            }
        }
    }
}
