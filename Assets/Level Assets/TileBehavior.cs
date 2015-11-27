using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour {

	private int x, z;
	public int X
	{
		get {return x; }
		set {x  = value;}
	}
	public int Z
	{
		get {return z; }
		set {z  = value;}
	}

	// Use this for initialization
	void Start () {
	
	}
	

	void OnMouseDown(){
		Debug.Log ("This tile is: " + x + "," + z);
		FindObjectOfType<CommandPanel> ().pathSelection (this.gameObject);
	}
}
