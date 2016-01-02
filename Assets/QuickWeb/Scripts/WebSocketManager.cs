using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Text;

public class WebSocketManager : MonoBehaviour {

	private int reiliableChannelId = 0;
	private int hostId = 0;

	private Dictionary<int, CommandPanel> playerRegistry = new Dictionary<int, CommandPanel>();

	[SerializeField]
	private GameObject controllerPrefab;

	// Use this for initialization
	void Start () {

		NetworkTransport.Init ();

		ConnectionConfig config = new ConnectionConfig();
		reiliableChannelId  = config.AddChannel(QosType.Reliable);
		HostTopology topology = new HostTopology(config, 5);

		hostId = NetworkTransport.AddWebsocketHost(topology, 8081, null);
	
	}
	
	// Update is called once per frame
	void Update () {

		int recHostId; 
		int connectionId; 
		int channelId; 
		byte[] recBuffer = new byte[1024]; 
		int bufferSize = 1024;
		int dataSize;
		byte error;
		NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
		switch (recData)
		{
		case NetworkEventType.Nothing:         //1
			break;
		case NetworkEventType.ConnectEvent:    //2
			//Debug.Log("Connection ID: " + connectionId);
                Byte[] toSend = Encoding.ASCII.GetBytes("Ping from server!");
                NetworkTransport.Send(hostId, connectionId, channelId, toSend, bufferSize, out error);
                break;
		case NetworkEventType.DataEvent:       //3

            Debug.Log(Encoding.ASCII.GetString(recBuffer));
                /*
            foreach(byte b in recBuffer)
                {
                    if (!b.ToString().Equals("0"))
                    Debug.Log(b.ToString());
                }
                */
                break;
		case NetworkEventType.DisconnectEvent: //4
			break;
		}
	
	}

	//TODO: 
	IEnumerator checkRegistryForPlayer(int connectionId, byte[] nameBytes){

        string parsedName = "";
        parsedName = parsedName.Trim().ToLower();
        foreach(int existingId in playerRegistry.Keys)
        {
            CommandPanel tempCP = playerRegistry[existingId];
            if (tempCP.PlayerName.Equals(parsedName))
            {
                playerRegistry.Remove(existingId);
                playerRegistry.Add(connectionId, tempCP);
                return null;
            }
        }
        //else, if playercount is also less than max, create a new one
        return null;
	}

	// TODO: write JSONObject class abstract
	public void sendData(string temp, int conId)
	{
		byte[] compressedData = new byte[1];
		byte error;
		NetworkTransport.Send (hostId, conId, reiliableChannelId, compressedData, compressedData.Length, out error);
	}
}