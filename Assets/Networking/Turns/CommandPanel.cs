﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandPanel : MonoBehaviour {

	private bool moveSelectEnabled;
		public bool MoveSelectEnabled
	{
		get {return moveSelectEnabled; }
		set {moveSelectEnabled = value;}
	}

	[SerializeField]
	private GameObject unit;
	public GameObject SelectedUnit
	{
		get {return unit; }
		set {unit = value;}
	}

	[SerializeField]
	private float distanceBetweenTiles = 1f;

	private List<Vector3> queuedPath = new List<Vector3>();

	// Use this for initialization
	void Start () {
	
		MoveSelectEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void pathSelection(Vector3 tilePos){
		if (moveSelectEnabled) {
			/*
		if (currentPos > unit.GetComponent<CharacterBehavior> ().getMoveStat ()) {
			return;
		}
	*/
			// if the list is empty, set the start at where you're at, idiot
			if(queuedPath.Count == 0)
				queuedPath.Add(unit.transform.position);

			int currentPos = queuedPath.Count;

			// checks the distance between the selected tiles 
			float dx = Mathf.Abs(queuedPath [currentPos - 1].x - tilePos.x);
			float dz = Mathf.Abs(queuedPath [currentPos - 1].z - tilePos.z);


			//if you click on an already-existant tile, remove everything past there
			if (queuedPath.Contains (tilePos)) {
				int index = queuedPath.IndexOf (tilePos);
				for (int i = queuedPath.Count-1; i > index; i--) {
					Debug.Log ("Removing: " + queuedPath[i].x + "," + queuedPath[i].z);
					queuedPath.RemoveAt (i);
				}
			} else if (((dx == distanceBetweenTiles && dz == 0f) || (dx == 0f && dz == distanceBetweenTiles)) && (dx + dz <= 1f)){
				queuedPath.Add (tilePos);
			}

			foreach (Vector3 v in queuedPath) {
				Debug.Log ("Next Step: " + v.x + "," + v.z);
			}

			Debug.Log (queuedPath.Count);
		}
	}

	public void commitToMove(){
		Movement move = new Movement (unit, queuedPath, true);
		Execute (move);
		queuedPath.Clear ();
	}

	public void Execute(Action action){
		Debug.Log ("Executing...");
		FindObjectOfType<FieldReporter> ().addActionToTurn(action);
		StartCoroutine(action.Execute());
	}


}
