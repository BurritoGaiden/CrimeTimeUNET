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
        cr.alpha = Random.Range(0.5f, 1.0f);
    }

    IEnumerator Flicker()
    {
        do
        {
            cr.alpha = Random.Range(0.5f, 1.0f);
            yield return new WaitForSeconds((1.0f / 30.0f));
        } while (true);
    }

}
