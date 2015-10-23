using UnityEngine;
using System.Collections;

public class Vision : MonoBehaviour {

	public Camera perspectiveCam;
	public GameObject unit;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame


	void OnPreCull(){

		perspectiveCam = GetComponent<Camera>();
		Collider[] hitColliders = Physics.OverlapSphere (unit.transform.position, 20f);
		//Debug.Log (hitColliders.Length);
		foreach (Collider c in hitColliders) {
			//Debug.DrawRay(transform.position, (c.gameObject.transform.position - unit.transform.position).normalized, Color.red);
			if(c.gameObject.layer == LayerMask.NameToLayer("P1"))
			{
				//Debug.Log ("Player seen!");
				RaycastHit hit = new RaycastHit();

				if(Physics.Raycast(unit.transform.position, (c.gameObject.transform.position - unit.transform.position).normalized, out hit, 20f))
				{
					//Debug.Log("We passed!");
					if(hit.collider.gameObject.layer != c.gameObject.layer){
						perspectiveCam.cullingMask &= ~(1 << c.gameObject.layer);
						foreach (Light l in FindObjectsOfType<Light>()) {
							if(l.gameObject.layer == LayerMask.NameToLayer("P1"))
							{
								l.enabled = false;
							}
						}
					}
					else if (hit.collider.gameObject.layer == c.gameObject.layer){
						perspectiveCam.cullingMask |= 1 << c.gameObject.layer;
					}
				}
			}
		}
	}

	void OnPostRender(){
		foreach (Light l in FindObjectsOfType<Light>()) {
				l.enabled = true;
		}
	}
}
