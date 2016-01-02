//using UnityEngine;
//using System.Collections;
//using UnityEngine.Networking;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System;
//using System.Net.Sockets;
//using System.Net;
//using System.Threading;

//public class WebSocketManager : MonoBehaviour {


//    private string ip_addr = "127.0.1.0";
//	private int reiliableChannelId = 0;
//	private int hostId = 0;

//	private Dictionary<int, CommandPanel> playerRegistry = new Dictionary<int, CommandPanel>();

//	[SerializeField]
//	private GameObject controllerPrefab;
//    TcpListener server = null;
//    Byte[] bytes = new Byte[256];
//    String data = null;
//    // Use this for initialization
//    private bool mRunning;
//    public static string msg = "";

//    public Thread mThread;
//    public TcpListener tcp_Listener = null;

//    void Start () {
//        //NetworkTransport.Init();

//        //ConnectionConfig config = new ConnectionConfig();
//        //reiliableChannelId = config.AddChannel(QosType.Reliable);
//        //HostTopology topology = new HostTopology(config, 5);
  
//        //hostId = NetworkTransport.AddWebsocketHost(topology, 8081, null);

//    }
	
//	// Update is called once per frame
//	void Update () {

//        //int recHostId; 
//        //int connectionId; 
//        //int channelId; 
//        //byte[] recBuffer = new byte[1024]; 
//        //int bufferSize = 1024;
//        //int dataSize;
//        //byte error;
//        //NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
//        //switch (recData)
//        //{
//        //case NetworkEventType.Nothing:         //1
//        //	break;
//        //case NetworkEventType.ConnectEvent:    //2
//        //	Debug.Log("Connection ID: " + connectionId);
//        //	break;
//        //case NetworkEventType.DataEvent:       //3
//        //              Debug.Log("Recieved data: " + "rechost " + recHostId + ", connection " + 
//        //                  connectionId + ", channel " + 
//        //                  channelId + ", rec " +
//        //                  System.Text.Encoding.UTF8.GetString(recBuffer) + ", buffsize " + 
//        //                  bufferSize + ", datasize " + 
//        //                  dataSize + ", error " + 
//        //                  error);
//        //              Debug.Log("Data Size: " + dataSize);
//        //              Debug.Log("Error: " + error);
//        //              foreach (byte b in recBuffer)
//        //              {
//        //                  if (!b.ToString().Equals("0"))
//        //                      Debug.Log(b.ToString());
//        //              }
//        //              Debug.Log("Recieved " + System.Text.Encoding.UTF8.GetString(recBuffer));
//        //              string s = "Hello";
//        //              byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
//        //              NetworkTransport.Send(hostId, connectionId, channelId, bytes, bytes.Length, out error);
//        //              Debug.Log("Sent " + System.Text.Encoding.UTF8.GetString(bytes));
//        //              string BinaryString = (string)entityInfo.UnmappedValuesMap["BinaryProp"];
//        //              byte[] BinaryData = Convert.FromBase64String(BinaryString);
//        //              //Debug.Log();
//        //              break;
//        //case NetworkEventType.DisconnectEvent: //4
//        //	break;
//        //}

//    }
    
//	//TODO: 
//	IEnumerator checkRegistryForPlayer(int connectionId, byte[] nameBytes){

//        string parsedName = "";
//        parsedName = parsedName.Trim().ToLower();
//        foreach(int existingId in playerRegistry.Keys)
//        {
//            CommandPanel tempCP = playerRegistry[existingId];
//            if (tempCP.PlayerName.Equals(parsedName))
//            {
//                playerRegistry.Remove(existingId);
//                playerRegistry.Add(connectionId, tempCP);
//                return null;
//            }
//        }
//        //else, if playercount is also less than max, create a new one
//        return null;
//	}

//	// TODO: write JSONObject class abstract
//	public void sendData(string temp, int conId)
//	{
//		byte[] compressedData = new byte[1];
//		byte error;
//		NetworkTransport.Send (hostId, conId, reiliableChannelId, compressedData, compressedData.Length, out error);
//	}

//    private void OnWebSocketOpen(WebSocket webSocket)
//    {
//        Debug.Log("WebSocket Open!");
//    }

//    private void OnMessageReceived(WebSocket webSocket, string message)
//    {
//        Debug.Log("Text Message received from server: " + message);
//    }

//    private void OnError(WebSocket ws, Exception ex)
//    {
//        string errorMsg = string.Empty;
//        if (ws.InternalRequest.Response != null)
//            errorMsg = string.Format("Status Code from Server: {0} and Message: {1}",
//            ws.InternalRequest.Response.StatusCode,
//            ws.InternalRequest.Response.Message);

//        Debug.Log("An error occured: " + (ex != null ? ex.Message : "Unknown: " + errorMsg));
//    }

//    private void OnWebSocketClosed(WebSocket webSocket, UInt16 code, string message)
//    {
//        Debug.Log("WebSocket Closed!");
//    }
//}