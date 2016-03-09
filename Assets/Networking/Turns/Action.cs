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
	private MapActor actor;
	// what object is targeted by the action
	private GameObject target;
	// did the action work, or did it fail? 
	private bool success;

	public Movement(MapActor a, List<TileBehavior> p, bool s){
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
				t += Time.deltaTime / 0.5f; // Sweeps from 0 to 1 in time seconds
				actor.transform.position = Vector3.Lerp(actor.transform.position, new Vector3 (xPos, 0.125f, zPos), t);
				yield return 0;
			}
                actor.Coords = tb.Coords;

            yield return null;
		}
	}
}

public class Attack : Action{
	

	private MapActor actor;
	// what object is targeted by the action
	private MapActor target;
	// did the action work, or did it fail? 
	private bool success;
	
	public Attack(MapActor a, MapActor t, bool s){
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


