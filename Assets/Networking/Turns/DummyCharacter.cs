﻿using UnityEngine;
using System.Collections;

public class DummyCharacter : MapActor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		FindObjectOfType<CommandPanel> ().TargetAttack (this);
	}
}
