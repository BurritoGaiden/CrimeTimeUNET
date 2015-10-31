using UnityEngine;
using System.Collections;

public class PAUSE : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pause(){
		Time.timeScale = 0.0f;
	}
}
