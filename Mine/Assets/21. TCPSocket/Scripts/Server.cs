using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace Myproject
{
    public class Server : MonoBehaviour
    {
        public Button connect;
        public RectTransform textArea;
        public TextMeshProUGUI textPrefab;
        private string ipAddress = "127.0.0.1";
        public int port = 9999;

        private bool isConnected = false;

        private Thread serverMainThread;

        private int clientId = 0;

        private List<ClientHandler> clients = new List<ClientHandler>();

        public static Queue<string> log = new Queue<string>();

        private void Awake()
        {
            connect.onClick.AddListener(ConnectButtonClick);
        }
        private void Update()
        {
            if (log.Count > 0)
            {
                TextMeshProUGUI logText = Instantiate(textPrefab, textArea);
                logText.text = log.Dequeue();
            }
        }

        private void ConnectButtonClick()
        {
            if (false == isConnected)
            {
                serverMainThread = new Thread(ServerThread);
                serverMainThread.IsBackground = true;
                serverMainThread.Start();
                isConnected = true;
            }
            else
            {
                serverMainThread.Abort();
                isConnected = false;
            }
        }

        private void ServerThread()
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Parse(ipAddress), port);
                listener.Start();
                log.Enqueue("서버 시작");
                while (true)
                {
                    TcpClient tcpClient = listener.AcceptTcpClient();

                    ClientHandler handler = new ClientHandler();

                    handler.Connect(clientId++, this, tcpClient);
                    handler.writer.WriteLine($"ID:{clientId}");
                    clients.Add(handler);
                    log.Enqueue($"{clientId}번 클라이언트가 접속됨.");
                }
            }
            catch
            {
                log.Enqueue("뭔가..뭔가 일어나고 있음");
            }
            finally
            {
                foreach (var client in clients)
                    client.Disconnect();
                serverMainThread.Abort();
                isConnected = false;
            }

        }

        public void Disconnect(ClientHandler client)
        {
            clients.Remove(client);
        }

        public void BroadcastToClients(string message)
        {
            foreach (ClientHandler client in clients)
            {
                client.MessageToClient(message);
            }
        }

    }

    public class ClientHandler
    {
        public int id;
        public Server server;
        public TcpClient tcpClient;
        public Thread clientHandlerThread;
        public StreamReader reader;
        public StreamWriter writer;

        public void Connect(int id, Server server, TcpClient tcpClient)
        {
            this.id = id;
            this.server = server;
            this.tcpClient = tcpClient;
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;
            clientHandlerThread = new Thread(Run);
            clientHandlerThread.IsBackground = true;
            clientHandlerThread.Start();
        }

        public void Disconnect()
        {
            writer.Close();
            reader.Close();
            tcpClient.Close();
            clientHandlerThread.Abort();
            server.Disconnect(this);
        }

        public void MessageToClient(string message)
        {
            writer.WriteLine(message);
        }

        public void Run()
        {
            try
            {
                while (tcpClient.Connected)
                {
                    string receiveMessage = reader.ReadLine();

                    if (string.IsNullOrEmpty(receiveMessage))
                        continue;

                    try
                    {
                        Vector2Packet positionData = JsonUtility.FromJson<Vector2Packet>(receiveMessage);
                        string logMessage = $"{positionData.id}번 클라이언트가 클릭한 좌표: ({positionData.x}, {positionData.y})";
                        Server.log.Enqueue(logMessage);
                        string broadcastMessage = JsonUtility.ToJson(positionData);
                        server.BroadcastToClients(broadcastMessage);
                    }
                    catch
                    {
                        server.BroadcastToClients($"{id}님의 말 : {receiveMessage}");
                    }
                }
            }
            finally
            {
                Server.log.Enqueue($"{id}번 클라이언트 연결 종료됨.");
                Disconnect();
            }
        }
    }
    public class Vector2Packet
    {
        public float x;
        public float y;
        public int id;

        public Vector2Packet(int id, float x, float y)
        {
            this.id = id;
            this.x = x;
            this.y = y;
        }
    }
}
