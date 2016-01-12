using UnityEngine;
using System.Collections;

public class DoNotDestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
    }

}
