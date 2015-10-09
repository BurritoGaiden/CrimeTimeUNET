using UnityEngine;
using System.Collections;

public class NoManagerBounceBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (MainMenuManager.singleton == null) {
			Application.LoadLevel (0);
			Debug.Log ("No network manager detected, returning to main menu");
		}
	}

}
