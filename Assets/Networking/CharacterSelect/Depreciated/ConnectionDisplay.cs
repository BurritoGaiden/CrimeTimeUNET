using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ConnectionDisplay : NetworkBehaviour {


	private Image[] playerPips;

	[SerializeField]
	private Color notJoinedColor;
	
	[SerializeField]
	private Color joinedColor;


	[SyncVar(hook="updateDisplay")]
	private int numberConnected = 0;

	// Use this for initialization
	void Start () {
		/*
		foreach (Image i in playerPips) {
			i.color = Color.grey;
		}
		*/
		playerPips = gameObject.GetComponentsInSiblings<Image>();
		updateDisplay (numberConnected);
	
	}

	private void updateDisplay(int incoming)
	{
		if (numberConnected >= 0)
			playerPips.updatePips (incoming, joinedColor, notJoinedColor);
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