using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using Newtonsoft.Json;

public class WebSocketManager : MonoBehaviour {

	private int reiliableChannelId = 0;
	private int hostId = 0;

	private Dictionary<string, GameObject> playerRegistry = new Dictionary<string, GameObject>();
	[SerializeField]
	private GameObject controllerPrefab;

	// Use this for initialization
	void Start () {

		NetworkTransport.Init ();

		ConnectionConfig config = new ConnectionConfig();
		reiliableChannelId  = config.AddChannel(QosType.Reliable);
		HostTopology topology = new HostTopology(config, 5);

		hostId = NetworkTransport.AddWebsocketHost(topology, 8887, null);
	
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
			connectionCheck(connectionId);
			break;
		case NetworkEventType.DataEvent:       //3
			break;
		case NetworkEventType.DisconnectEvent: //4
			break;
		}
	
	}

	//TODO: 
	void connectionCheck(int connectionID){

	}

	// TODO: write JSONObject class abstract
	public void sendData(string temp, int conId)
	{
		byte[] compressedData = new byte[1];
		byte error;
		NetworkTransport.Send (hostId, conId, reiliableChannelId, compressedData, compressedData.Length, out error);
	}
}