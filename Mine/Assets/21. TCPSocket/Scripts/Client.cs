using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Threading;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System;

namespace Myproject
{
    public class Client : MonoBehaviour
    {
        [Header("IP Input")]
        public TMP_InputField ip;
        public TMP_InputField port;
        public Button connect;

        [Header("Message Input")]
        public TMP_InputField message;
        public Button send;

        [Header("Text Area")]
        public RectTransform textArea;
        public TextMeshProUGUI textPrefab;

        private Thread clientThread;
        private StreamReader reader;
        private StreamWriter writer;

        private bool isConnected;

        public static Queue<string> log = new Queue<string>();

        Vector2 point = Vector2.zero;

        private int myId = -1;

        private void Awake()
        {
            connect.onClick.AddListener(ConnectButtonClick);
            send.onClick.AddListener(() => { SendSubmit(message.text); });
            message.onEndEdit.AddListener(SendSubmit);
        }

        private void Update()
        {
            if (log.Count > 0)
            {
                TextMeshProUGUI logText = Instantiate(textPrefab, textArea);
                logText.text = log.Dequeue();
            }

            // 마우스 클릭 입력 감지
            if (Input.GetMouseButtonDown(0)) // 0은 왼쪽 마우스 버튼
            {
                point = Input.mousePosition;
                SendMousePosition(point);
            }
        }

        private void ClientThread()
        {
            try
            {
                log.Enqueue("스레드를 시작합니다");
                TcpClient tcpClient = new TcpClient();
                IPAddress serverAddress = IPAddress.Parse(ip.text);
                int portNum = int.Parse(port.text);

                IPEndPoint endPoint = new IPEndPoint(serverAddress, portNum);
                tcpClient.Connect(endPoint);

                log.Enqueue($"서버 접속 성공!~");

                reader = new StreamReader(tcpClient.GetStream());
                writer = new StreamWriter(tcpClient.GetStream());
                writer.AutoFlush = true;

                while (tcpClient.Connected)
                {
                    string receiveMessage = reader.ReadLine();
                    if (receiveMessage.StartsWith("ID:"))
                    {
                        //log.Enqueue(receiveMessage);
                        SetId(int.Parse(receiveMessage.Substring(3)));
                    }
                    else
                        try
                        {
                            Vector2Packet positionData = JsonUtility.FromJson<Vector2Packet>(receiveMessage);
                            string logMessage = $"{positionData.id}번 클라이언트가 클릭한 좌표: ({positionData.x}, {positionData.y})";
                            log.Enqueue(logMessage);
                        }
                        catch
                        {
                            log.Enqueue(receiveMessage);
                        }
                }

            }
            catch (ApplicationException e)
            {
                log.Enqueue("어플리케이션 예외 발생");
                log.Enqueue(e.Message);
            }
            catch (Exception e)
            {
                log.Enqueue("뭔가 문제가 발생함");
                log.Enqueue(e.Message);
            }
            finally
            {
                reader?.Close();
                writer?.Close();
            }
        }

        private void ConnectButtonClick()
        {
            log.Enqueue("연결 버튼 눌림");
            if (!isConnected)
            {
                clientThread = new Thread(ClientThread);
                clientThread.IsBackground = true;
                clientThread.Start();
                isConnected = true;
            }
            else
            {
                clientThread.Abort();
                isConnected = false;
            }
        }

        private void SendSubmit(string message)
        {
            if (writer != null && !string.IsNullOrEmpty(message))
            {
                writer.WriteLine(message);
                this.message.text = "";
            }
        }

        private void SendMousePosition(Vector2 position)
        {
            if (writer != null)
            {
                // 좌표 정보를 JSON으로 직렬화
                Vector2Packet positionData = new Vector2Packet(myId, position.x, position.y);
                string jsonData = JsonUtility.ToJson(positionData);

                // 서버로 전송
                SendSubmit(jsonData);
            }
        }

        public void SetId(int id)
        {
            myId = id;
        }
    }
}
