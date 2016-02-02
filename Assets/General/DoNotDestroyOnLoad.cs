using UnityEngine;
using System.Collections;

public class DoNotDestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(transform.gameObject);
    }

}
