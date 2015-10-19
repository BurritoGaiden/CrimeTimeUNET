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
	
	// did the action work, or did it faail? 
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
