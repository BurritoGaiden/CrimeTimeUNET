using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour {

	private GameObject helpMenu;

	void Start(){
		helpMenu = GameObject.Find ("HelpMenuPanel");
	}

	public void summonHelpMenu(){
		Debug.Log ("DEBUG");
		helpMenu.SetActive (true);
	}
}
