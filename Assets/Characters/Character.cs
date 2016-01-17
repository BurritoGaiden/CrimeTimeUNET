using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    // the character prefab for use during gameplay
    [SerializeField]
    private GameObject associatedCharacter;
    public GameObject AssociatedCharacterPrefab
    {
        get { return associatedCharacter; }
    }

    // the name of the associated character, pulled from the CharacterBehavior of the AssociatedCharacter
    [SerializeField]
    private string characterName = "";
    public string CharacterName
    {
        get { return characterName; }
    }

    // the boolean determining if this character has been selected by a player
    private bool isChosen = false;
    public bool IsChosen
    {
        get { return isChosen; }
        set
        {
            isChosen = value;
            selectionSpotlight.SetActive(value);
        }
    }

    [Header("Gameplay Objects to Interface")]
    [SerializeField]
    private GameObject selectionSpotlight;

    // Use this for initialization
    void Start () {
        characterName = associatedCharacter.GetComponent<CharacterBehavior>().Label;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
