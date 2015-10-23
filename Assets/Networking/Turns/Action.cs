using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface Action {

	// all actions need to be able to execute
	IEnumerator Execute();
	
}

public class Movement : Action{

	private List<Vector3> path;
	// what object is doing the action
	private GameObject actor;
	// what object is targeted by the action
	private GameObject target;
	// did the action work, or did it fail? 
	private bool success;

	public Movement(GameObject a, List<Vector3> p, bool s){
		actor = a;
		path = new List<Vector3>(p);
		success = s;
	}
	
	public IEnumerator Execute(){
		foreach (Vector3 v in path) {
			float t = 0.0f;
			while (t < 1.0f) {
				t += Time.deltaTime / 0.25f; // Sweeps from 0 to 1 in time seconds
				actor.transform.LookAt(Vector3.Lerp(actor.transform.position, new Vector3 (v.x, 0.125f, v.z), t));
				actor.transform.position = Vector3.Lerp(actor.transform.position, new Vector3 (v.x, 0.125f, v.z), t);
				yield return 0;
			}
			//actor.transform.position = new Vector3 (v.x, 0.125f, v.z);
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


