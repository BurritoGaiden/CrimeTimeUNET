using UnityEngine;
using System.Collections;

public class HelpMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void closeHelpWindow()
	{
		Debug.Log ("DEBUG");
		this.gameObject.SetActive (false);
	}

}
