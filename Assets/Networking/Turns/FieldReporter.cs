using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldReporter : MonoBehaviour {

	//The innermost list is a list of all things that happen per turn, 
	//The outermost list is a list each turn
	private List<List<Action>> Record = new List<List<Action>>();

	private int turnNumber = 0;

	// Use this for initialization
	void Start () {
		Record.Add (new List<Action> ());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void addActionToTurn(Action a){
		Record [turnNumber].Add (a);
	}

	public void startReplay(){
		StartCoroutine (crunchTurns ());
	}

	public IEnumerator crunchTurns(){
		int i;
		for (i = 0; i < Record[turnNumber].Count; i++) {
			yield return StartCoroutine(Record[turnNumber][i].Execute());
		}
	}

}
