using UnityEngine;
using System.Collections;

public class CopVision : MonoBehaviour {

    [SerializeField]
    private float visionRange = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPreCull()
    {
        RaycastHit hit = new RaycastHit();
        foreach (CharacterBehavior spotter in FindObjectsOfType<CharacterBehavior>())
        {
            if(spotter.Team == Alliance.Cops)
            {
                foreach (MapActor spotted in FindObjectsOfType<MapActor>())
                {
                    if(Physics.Raycast(spotter.transform.position, (spotted.gameObject.transform.position - spotter.transform.position).normalized, out hit, visionRange))
                    {
                        if (hit.collider.gameObject == spotted.gameObject)
                        {
                            spotted.GetComponent<Renderer>().enabled = true;
                            spotted.IsVisible = true;
                            // Not the best way to do this!!!
                            foreach (Transform child in spotted.transform)
                            {
                                child.gameObject.SetActive(true);
                            }
                        }
                        else
                        {
                            spotted.IsVisible = false;
                            spotted.GetComponent<Renderer>().enabled = false;
                            foreach (Transform child in spotted.transform)
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }

    }
}
