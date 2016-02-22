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
    private UsernameCanvas userCanvas;
    [SerializeField]
    private CommandPanel owner;
    public CommandPanel Owner
    {
        get { return owner; }
        set
        {
            owner = value;
            spotlight.enabled = (owner != null);
            if(owner != null && userCanvas != null)
            {
                userCanvas.Username.text = owner.PlayerName;
                userCanvas.Canvas.enabled = true;
            }
            else if(userCanvas != null)
            {
                userCanvas.Canvas.enabled = false;
            }

        }
    }
    //public bool isPicked = false;
  
}
