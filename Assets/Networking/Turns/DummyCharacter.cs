using UnityEngine;
using System.Collections;

public class DummyCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		FindObjectOfType<CommandPanel> ().targetAttack (this.gameObject);
	}
}
