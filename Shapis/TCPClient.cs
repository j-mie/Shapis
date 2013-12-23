using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shapis
{
    public class TCPClient
    {
        #region Private
        private System.Net.Sockets.TcpClient client;

        private StreamReader reader;
        private StreamWriter writer;

        private bool Connect(string domain, int port)
        {
            try
            {
                client = new System.Net.Sockets.TcpClient();
                client.Connect(domain, port);
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream()) { NewLine = "\r\n" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Couldn't connect to {0}:{1} with reason {2}", domain, port,
                    ex));
            }
            return client.Connected;
        }

        private void Write(string text)
        {
            try
            {
                writer.WriteLine(text);
                writer.Flush();
            }
            catch (Exception)
            {
                throw new ApplicationException(string.Format("Error while writing: {0}", text));
            }
        }

        private ArrayList Response()
        {
            var list = new ArrayList();

            try
            {
                var response = reader.ReadLine();

                while (response != null)
                {
                    list.Add(response);
                    response = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Error while reading data: {0}", ex.Data));
            }

            return list;
        }
        #endregion
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpReader"/> class.
        /// </summary>
        public void TcpReader()
        {
            client = new TcpClient();
        }

        /// <summary>
        /// Reads the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="port">The port.</param>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public ArrayList Read(string url, int port, string command)
        {
            var result = new ArrayList();

            var connected = Connect(url, port);

            if (connected)
            {
                Write(command);

                result = Response();
            }

            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (client != null)
            {
                if (client.Connected)
                {
                    client.Close();
                }
            }
        }
    }
}
