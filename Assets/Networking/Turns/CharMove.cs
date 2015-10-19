using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CharMove : MonoBehaviour {

	public bool pathTime = false;

	// Use this for initialization
	void Start () {
	
	}

	public List<Vector3> selectPath(){
		List<Vector3> path = new List<Vector3> ();
		path.Add (gameObject.transform.position);
		return path;
	}
	

}
