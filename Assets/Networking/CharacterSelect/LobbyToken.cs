using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyToken : NetworkBehaviour {

	private GameObject characterSelected;
	public GameObject selectedButton;

	// Use this for initialization
	void Start () {
		if(FindObjectOfType<ConnectionDisplay> () != null)
			FindObjectOfType<ConnectionDisplay> ().CmdUpdateConnectedNumber ();
	}
	
	void OnDestroy() {
		if(FindObjectOfType<ConnectionDisplay> () != null)
			FindObjectOfType<ConnectionDisplay> ().CmdUpdateConnectedNumber ();
		if(selectedButton != null)
			selectedButton.GetComponent<CharacterPanel> ().CmdNullOut (this.gameObject);
	}

	public void setCharacter(GameObject character)
	{
		characterSelected = character;
		MainMenuManager.singleton.playerPrefab = characterSelected;
		//Debug.Log (characterSelected.name);
	}

	void OnLevelWasLoaded(int level)
	{

	}

	// --- Commands to the server ---

	[Command]
	public void CmdCheckOwnership (GameObject clickedButton)
	{
		clickedButton.GetComponent<CharacterPanel> ().CheckOwnership (this.gameObject);
	}
	
	[Command]
	public void CmdSetToggleState (GameObject clickedButton, bool incomingState)
	{
		clickedButton.GetComponent<CharacterPanel> ().SetToggle(incomingState);
	}
	
}
