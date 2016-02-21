using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour {

    private Dictionary<string, CharacterSelectEntry> roster = new Dictionary<string, CharacterSelectEntry>();
    [SerializeField]
    private CharacterSelectEntry[] characterList = new CharacterSelectEntry[5];

    // Use this for initialization
    void Awake () {

        foreach (CharacterSelectEntry c in characterList)
        {
            try
            {
                roster.Add(c.CharacterPrefab.Label, c);
            }
            catch (Exception e)
            {
                Debug.Log("Invalid Character entry; cannot add to roster! Error:" + e.Message);
            }
        }
    }
	

	// Update is called once per frame
	void Update () {
	
	}

    /*
    public void SelectCharForPlayer(string characterName, CommandPanel playerCP)
    {
       
        if (roster.ContainsKey(characterName))
        {
            CharacterSelectEntry desiredCharacter = roster[characterName];

            // if the character is 
            if (desiredCharacter.isPicked == false)
            {
                // deselect any character they may already have selected
                if (playerCP.Character != null)
                {
                    roster[playerCP.].isPicked = false;
                }
                // select the newly desired character
                desiredCharacter.isPicked = true;
                playerCP.Character = desiredCharacter;

            // if the player clicks the character they already have, deselect it
            } else if (desiredCharacter.isPicked == true && playerCP.Character.Equals(desiredCharacter))
            {
                desiredCharacter.isPicked = false;
                playerCP.Character = null;
            }

        }
        
    }
    */

    public void SelectCharForPlayer(string characterName, CommandPanel playerCP)
    {
        if (roster.ContainsKey(characterName))
        {
            CharacterSelectEntry desiredCharacter = roster[characterName];
            if (desiredCharacter.Owner != null && desiredCharacter.Owner.Equals(playerCP))                            // If player selects the character they already had again, deselect it instead
            {
                DeselectPlayerCharacter(playerCP);
            }
            else if (desiredCharacter.Owner == null && playerCP.Character == null)  // If player has no character and selected character has no owner, 
            {
                desiredCharacter.Owner = playerCP;
                playerCP.Character = desiredCharacter.CharacterPrefab;
            }
            else if (desiredCharacter.Owner == null && playerCP.Character != null)  // If the player selects a character with no owner and already has a character, deselect the old character
            {
                DeselectPlayerCharacter(playerCP);

                desiredCharacter.Owner = playerCP;
                playerCP.Character = desiredCharacter.CharacterPrefab;
            }
        }
    }

    public void DeselectPlayerCharacter(CommandPanel playerCP)
    {
        foreach (CharacterSelectEntry entry in roster.Values)
        {
            if (entry.Owner != null && entry.Owner.Equals(playerCP))
            {
                entry.Owner = null;
                playerCP.Character = null;
                break;
            }
        }
    }
    public IEnumerator Countdown()
    {
        int count = 5;
        Intertitle.Instance.TextMode();
        Intertitle.Instance.Title.text = "Let's Begin!";
        Intertitle.Instance.Subtitle.text = "Ready?";
        yield return Intertitle.Instance.StartCoroutine(Intertitle.Instance.FromBottomToCenter(1.0f, true));
        yield return new WaitForSeconds(0.5f);

        for (count = 5; count > 0; count--)
        {
            Intertitle.Instance.Subtitle.text = count.ToString();
            yield return new WaitForSeconds(1f);
            
        }
        Intertitle.Instance.Subtitle.text = "Go!";
        yield return new WaitForSeconds(1f);
        GameStateManager.Instance.GameState = GameState.GameBegin;
        yield return Intertitle.Instance.StartCoroutine(Intertitle.Instance.FromCenterToTop(1.0f, true));
        
       
    }

    public IEnumerator StopCountdown()
    {
        StopCoroutine("Countdown");
        Intertitle.Instance.Subtitle.text = "Wait!";
        yield return new WaitForSeconds(0.5f);
        yield return Intertitle.Instance.StartCoroutine(Intertitle.Instance.FromCenterToTop(1.0f, true));
    }

}
