using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;

namespace Shapis
{
    class TCPClient
    {
        #region Private
        private TcpClient _client;

        private StreamReader _reader;
        private StreamWriter _writer;

        private bool Connect(string domain, int port)
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(domain, port);
                _reader = new StreamReader(_client.GetStream());
                _writer = new StreamWriter(_client.GetStream()) { NewLine = "\r\n" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Couldn't connect to {0}:{1} with reason {2}", domain, port,
                    ex));
            }
            return _client.Connected;
        }

        private void Write(string text)
        {
            try
            {
                _writer.WriteLine(text);
                _writer.Flush();
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
                var response = _reader.ReadLine();

                while (response != null)
                {
                    list.Add(response);
                    response = _reader.ReadLine();
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
            _client = new TcpClient();
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
            if (_client != null)
            {
                if (_client.Connected)
                {
                    _client.Close();
                }
            }
        }
    }
}
