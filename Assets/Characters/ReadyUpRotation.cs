using UnityEngine;
using System.Collections;

public class ReadyUpRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		this.GetComponent<CanvasRenderer> ().transform.Rotate (new Vector3 (0.0f, 0.0f, Mathf.Abs(360.0f * Mathf.Sin (0.5f * Time.fixedDeltaTime))));
		this.GetComponent<CanvasRenderer> ().transform.localScale = new Vector3 (Mathf.Abs(0.5f*Mathf.Sin (1.2f * Time.time))+1f,
		                                                                         Mathf.Abs(0.5f*Mathf.Sin (1.2f * Time.time))+1f
		                                                                         , 1.0f);
	}
}
