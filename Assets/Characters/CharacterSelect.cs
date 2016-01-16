using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour {

    private Dictionary<string, GameObject> roster = new Dictionary<string, GameObject>();
    [SerializeField]
    private GameObject[] characterPrefabList = new GameObject[5];

    // Use this for initialization
    void Start () {
        foreach(GameObject go in characterPrefabList)
        {
            try
            {
                roster.Add(go.GetComponent<CharacterBehavior>().Label, go);
            }catch (Exception e)
            {
                Debug.Log("Invalid Character prefab; cannot add to roster! Error:" + e.Message);
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectCharForPlayer(string character, string user)
    {
        PlayerRegisterRule.PlayerRegister[user].Character = roster[character];
    }
}
