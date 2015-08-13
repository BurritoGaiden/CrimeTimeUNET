using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ConnectionDisplay : NetworkBehaviour {

	[SerializeField]
	private Image[] playerPips = new Image[5];

	[SyncVar(hook="updateDisplay")]
	private int numberConnected = 0;

	// Use this for initialization
	void Start () {
		/*
		foreach (Image i in playerPips) {
			i.color = Color.grey;
		}
		*/
		updateDisplay (numberConnected);
	
	}

	private void updateDisplay(int incoming)
	{
		numberConnected = incoming;
		for (int i = 0; i < numberConnected; i++) {
			playerPips[i].color = Color.white;
		}
		for (int i = numberConnected; i < 5; i++) {
			playerPips[i].color = Color.grey;
		}
	}

	[Command]
	public void CmdUpdateConnectedNumber(){
		//NetworkServer.connections.Count
		
		numberConnected = FindObjectsOfType<LobbyToken> ().Length - 1;
	}

	// Update is called once per frame
	void Update () {

	
	}
}