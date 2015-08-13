using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnableOnServerOnly : NetworkBehaviour {

	[SerializeField]
	private GameObject[] toEnable;
	// Use this for initialization
	void Start () {
		if (this.isServer)
			foreach (GameObject g in toEnable)
				g.SetActive (true);
	}

}
