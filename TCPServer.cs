using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class TCPServer
    {
        Form1 GUI;
        bool isRunning;
        TcpListener listener;
        public string rootDir;

        public TCPServer() {}
        public TCPServer(int port, Form1 gui)
        {
            GUI = gui;

            rootDir = GUI.gui_dir.Text;
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            isRunning = true;
            Thread listenerThread = new Thread(Listen);
            listenerThread.Start();
            UpdateLog(String.Format("Сервер запущен по адресу {0}", Address()));
        }

        private void Listen()
        {
            while (isRunning)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread newClientThread = new Thread(new ParameterizedThreadStart(AcceptNewClient));
                    newClientThread.Start(client);
                }
                catch (SocketException e)
                {
                    if (e.ErrorCode.Equals(10004))
                        UpdateLog("Сервер остановлен.");
                }
                
            }
        }

        private void AcceptNewClient(Object client)
        {
            WebClient newClient = new WebClient((TcpClient)client, this);
            if (newClient.RecieveRequest())
                newClient.ProcessRequest();
        }

        public string Address()
        {
            return listener.LocalEndpoint.ToString();
        }

        public void TryToStop()
        {
            
            if (listener != null)
            {
                listener.Stop();
                isRunning = false;
            }
                
        }
        
        public void UpdateLog(string msg)
        {
            GUI.BeginInvoke(new Form1.InvokeDelegate(GUI.UpdateLog), new object[1] { msg });
        }
    }
}
