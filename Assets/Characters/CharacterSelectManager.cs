using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectManager : MonoBehaviour {

    private Dictionary<string, Character> roster = new Dictionary<string, Character>();
    [SerializeField]
    private Character[] characterPrefabList = new Character[5];

    // Use this for initialization
    void Start () {
        foreach (Character c in characterPrefabList)
        {
            try
            {
                roster.Add(c.AssociatedCharacterPrefab.GetComponent<CharacterBehavior>().Label, c);
            }
            catch (Exception e)
            {
                Debug.Log("Invalid Character prefab; cannot add to roster! Error:" + e.Message);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectCharForPlayer(string character, CommandPanel playerCP)
    {
        if (roster.ContainsKey(character))
        {
            Character desiredCharacter = roster[character];

            // if the character is 
            if (desiredCharacter.IsChosen == false)
            {
                // deselect any character they may already have selected
                if (playerCP.Character != null)
                {
                    playerCP.Character.IsChosen = false;
                }
                // select the newly desired character
                playerCP.Character = desiredCharacter;
                desiredCharacter.IsChosen = true;

            // if the player clicks the character they already have, deselect it
            } else if (desiredCharacter.IsChosen == true && playerCP.Character.Equals(desiredCharacter))
            {
                desiredCharacter.IsChosen = false;
                playerCP.Character = null;
            }

        }
    }
}
