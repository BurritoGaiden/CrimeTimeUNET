using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Action {

	// all actions need to be able to execute
	IEnumerator Execute();
	
}

public class Movement : Action{

	private List<TileBehavior> path;
	// what object is doing the action
	private GameObject actor;
	// what object is targeted by the action
	private GameObject target;
	// did the action work, or did it fail? 
	private bool success;

	public Movement(GameObject a, List<TileBehavior> p, bool s){
		actor = a;
		path = new List<TileBehavior>(p);
		success = s;
	}
	
	public IEnumerator Execute(){
		foreach (TileBehavior tb in path) {
			float t = 0.0f;
            float xPos = tb.transform.position.x;
            float zPos = tb.transform.position.z;
            actor.transform.LookAt(Vector3.Lerp(actor.transform.position, new Vector3(xPos, 0.125f, zPos), t));
            while (t < 1.0f) {
				t += Time.deltaTime / 0.25f; // Sweeps from 0 to 1 in time seconds
				actor.transform.position = Vector3.Lerp(actor.transform.position, new Vector3 (xPos, 0.125f, zPos), t);
				yield return 0;
			}
            // If the moving actor is a character, set its current coordinates to the index of the tile it ends on
            if (actor.GetComponent<CharacterBehavior>() != null)
            {
                actor.GetComponent<CharacterBehavior>().Coord = tb.Coord;
            }
            yield return null;
		}
	}
}

public class Attack : Action{
	

	private GameObject actor;
	// what object is targeted by the action
	private GameObject target;
	// did the action work, or did it fail? 
	private bool success;
	
	public Attack(GameObject a, GameObject t, bool s){
		actor = a;
		target = t;
		success = s;
	}
	
	public IEnumerator Execute(){
		actor.GetComponent<LineRenderer>().SetPosition(0,actor.transform.position);
		actor.GetComponent<LineRenderer>().SetPosition(1,target.transform.position);
		yield return new WaitForSeconds(0.25f);
		actor.GetComponent<LineRenderer>().SetPosition(0,actor.transform.position);
		actor.GetComponent<LineRenderer>().SetPosition(1,actor.transform.position);
	}
}


