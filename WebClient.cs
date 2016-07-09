using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebServer
{
    class WebClient
    {
        TCPServer server;
        TcpClient client;

        string request;
        string requestHeader;
        byte[] buffer;
        int requestBytesAmount;

        string requestUri;

        string extension;
        string contentType;

        public WebClient(TcpClient client, TCPServer server)
        {
            this.server = server;
            this.client = client;

            request = "";
            buffer = new byte[1024];
        }     

        public bool RecieveRequest()
        {
            while ((requestBytesAmount = client.GetStream().Read(buffer, 0, buffer.Length)) > 0)
            {
                request += Encoding.ASCII.GetString(buffer, 0, requestBytesAmount);
                if (request.IndexOf("\r\n\r\n") >= 0 || request.Length > 4096)
                {
                    break;
                }
            }
            requestHeader = request.Split('\r','\n')[0];
            if (!RequestIsValid())
                return false;
            else
                return true;
        }

        public void ProcessRequest()
        {
            if (requestHeader.Contains("GET"))
                ProcessGet();
            else
            if (requestHeader.Contains("POST"))
                ProcessPost();
            else
            if (requestHeader.Contains("PUT"))
                ProcessPut();
            else
            if (requestHeader.Contains("DELETE"))
                ProcessDelete();
            else
            if (requestHeader.Contains("OPTIONS"))
                ProcessOptions();
        }

        private void ProcessOptions()
        {
                server.UpdateLog(requestHeader);
            string Headers = "HTTP/1.1 200 OK\nContent-Type: " + contentType + "\n\n";
            byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
                client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
        }
        private void ProcessDelete()
        {
            server.UpdateLog(requestHeader);
            string filePath = server.rootDir + requestUri;
            if (!File.Exists(filePath))
            {
                SendError(client, 404);
                return;
            }
            else
            {
                File.Delete(filePath);
                string Headers = "HTTP/1.1 200 OK\n\n";
                byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
                client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
                server.UpdateLog("Отправлен ответ " + Headers);
            }
        }

        private void ProcessPost()
        {

            server.UpdateLog(requestHeader);

            string filePath = server.rootDir + requestUri;
            if (!File.Exists(filePath))
            {
                SendError(client, 404);
                return;
            }
            extension = requestUri.Substring(requestUri.LastIndexOf('.'));
            GetContentType();

            FileStream FS;
            try
            {
                FS = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.Write);                
            }
            catch (Exception)
            {
                SendError(client, 500);
                return;
            }
            string data = (request.Substring(request.IndexOf("\r\n\r\n"))).Trim();
            if (data != "")
            {
                server.UpdateLog("Данные POST: " + data);
            }
            else
            {
                server.UpdateLog("Внимание! Массив POST пуст.)");
            }
           using (StreamWriter FSWriter = new StreamWriter(FS))
           {
                FSWriter.WriteLine(data);
                string Headers = "HTTP/1.1 200 OK\nContent-Type: " + contentType + "\n\n";
                byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
                client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
                server.UpdateLog("Отправлен ответ " + Headers);
            }
            FS.Close();
            client.Close();
        }

        private void ProcessPut()
        {
            server.UpdateLog(requestHeader);

            string filePath = server.rootDir + requestUri;
            
            extension = requestUri.Substring(requestUri.LastIndexOf('.'));
            GetContentType();

            FileStream FS;
            try
            {
                FS = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
            }
            catch (Exception)
            {
                SendError(client, 500);
                return;
            }
            string data = (request.Substring(request.IndexOf("\r\n\r\n"))).Trim();
            if (data != "")
            {
                server.UpdateLog("Данные POST: " + data);
            }
            else
            {
                server.UpdateLog("Внимание! Массив POST пуст.)");
            }
            using (StreamWriter FSWriter = new StreamWriter(FS))
            {
                FSWriter.WriteLine(data);
                string Headers = "HTTP/1.1 200 OK\nContent-Type: " + contentType + "\nContent-Length: " + FS.Length + "\n\n";
                byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
                client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);
                server.UpdateLog("Отправлен ответ " + Headers);
            }
            FS.Close();
            client.Close();
        }

        private void ProcessGet()
        {
            server.UpdateLog(requestHeader);
            server.UpdateLog("Запрос на ресурс " + requestUri + " от " + client.Client.RemoteEndPoint);
            string filePath = server.rootDir + requestUri;
            if (!File.Exists(filePath))
            {
                SendError(client, 404);
                return;
            }
            extension = requestUri.Substring(requestUri.LastIndexOf('.'));
            GetContentType();

            FileStream FS;
            try
            {
                FS = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception)
            {
                SendError(client, 500);
                return;
            }

            string Headers = "HTTP/1.1 200 OK\nContent-Type: " + contentType + "\nContent-Length: " + FS.Length + "\n\n";
            byte[] HeadersBuffer = Encoding.ASCII.GetBytes(Headers);
            client.GetStream().Write(HeadersBuffer, 0, HeadersBuffer.Length);


            while (FS.Position < FS.Length)
            {
                requestBytesAmount = FS.Read(buffer, 0, buffer.Length);
                client.GetStream().Write(buffer, 0, requestBytesAmount);
            }
            server.UpdateLog("Ресурс отправлен.");
            FS.Close();
            client.Close();
        }

        private bool RequestIsValid()
        {
            Match requestMatch = Regex.Match(request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");
            if (requestMatch == Match.Empty)
            {
                SendError(client, 400);
                return false;
            }
            requestUri = requestMatch.Groups[1].Value;
            requestUri = Uri.UnescapeDataString(requestUri);

            if (requestUri.IndexOf("..") >= 0)
            {
                SendError(client, 400);
                return false;
            }
            if (requestUri.EndsWith("/"))
            {
                requestUri += "index.html";
            }
            return true;
        }

        private void GetContentType()
        {
            switch (extension)
            {
                case ".htm":
                case ".html":
                    contentType = "text/html";
                    break;
                case ".css":
                    contentType = "text/stylesheet";
                    break;
                case ".js":
                    contentType = "text/javascript";
                    break;
                case ".jpg":
                    contentType = "image/jpeg";
                    break;
                case ".jpeg":
                case ".png":
                case ".gif":
                    contentType = "image/" + extension.Substring(1);
                    break;
                default:
                    if (extension.Length > 1)
                    {
                        contentType = "application/" + extension.Substring(1);
                    }
                    else
                    {
                        contentType = "application/unknown";
                    }
                    break;
            }
        }

        private void SendError(TcpClient Client, int code)
        {
            
            string CodeStr = code.ToString() + " " + ((HttpStatusCode)code).ToString();
            string Html = "<html><body><h1>" + CodeStr + "</h1></body></html>";
            string Str = "HTTP/1.1 " + CodeStr + "\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;

            byte[] Buffer = Encoding.ASCII.GetBytes(Str);
            Client.GetStream().Write(Buffer, 0, Buffer.Length);

            server.UpdateLog(String.Format("Ошибка при обработке запроса. Отправлен статус-код {0} \"{1}\".", code, ((HttpStatusCode)code).ToString()));
            Client.Close();
        }
    }

}
