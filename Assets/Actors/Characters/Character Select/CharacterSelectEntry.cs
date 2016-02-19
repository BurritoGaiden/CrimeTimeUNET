using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterSelectEntry {

    // used as the identifier that the web client sends 
    [SerializeField]
    private CharacterBehavior characterPrefab;
    public CharacterBehavior CharacterPrefab
    {
        get { return characterPrefab; }
    }
    [SerializeField]
    private Light spotlight;
    [SerializeField]
    private CommandPanel owner;
    public CommandPanel Owner
    {
        get { return owner; }
        set
        {
            owner = value;
            spotlight.enabled = (owner != null);

        }
    }
    //public bool isPicked = false;
  
}
