using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UsernameCanvas : MonoBehaviour {

    private Canvas c;
    public Canvas Canvas
    {
        get { return c; }
    }
    [SerializeField]
    private Text username;
    public Text Username
    {
        get { return username; }
    }


	// Use this for initialization
	void Awake () {
        c = GetComponent<Canvas>();
        c.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
