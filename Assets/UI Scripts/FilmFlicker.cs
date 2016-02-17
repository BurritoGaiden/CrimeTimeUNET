using UnityEngine;
using System.Collections;

public class FilmFlicker : MonoBehaviour {

    private CanvasGroup cr;

	// Use this for initialization
	void Awake () {
        cr = GetComponent<CanvasGroup>();
	}
	
	// Update is called once per frame
	void Update () {
        cr.alpha = Random.Range(0.0f, 0.11f);
	}
}
