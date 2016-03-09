using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorRegistry : Singleton<ActorRegistry> {

    public List<MapActor> ActorRegister = new List<MapActor>();

	// Use this for initialization
	void Start () {
	
	}
	
}
