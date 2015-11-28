using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderSnap : MonoBehaviour {

	public float threshold;

	// Use this for initialization
	void Start () {
	
	}

	public void snapToCenter(float t){
		Slider v = GetComponent<Slider> ();
		if (Mathf.Abs (t) < threshold) {
			v.value = 0f;
		}
	}
}
