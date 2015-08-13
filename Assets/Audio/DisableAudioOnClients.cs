using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DisableAudioOnClients : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		if (!this.isServer) {
			AudioListener.volume = 0.0f;
			Debug.Log("Audio disabled on client");
		}
	}

}
