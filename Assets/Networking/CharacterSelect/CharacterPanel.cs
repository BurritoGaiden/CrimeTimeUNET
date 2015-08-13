using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CharacterPanel : NetworkBehaviour {

	[SyncVar(hook="updateColor")]
	private Color currentColor = Color.white;

	private Toggle childToggle;

	[SyncVar(hook="updateToggle")]
	private bool toggleState = false;

	[SyncVar(hook="enableToggle")]
	private GameObject associatedPlayer = null;

	[SerializeField]
	private GameObject associatedCharacter;

	// Use this for initialization
	void Start () 
	{
		childToggle = transform.parent.GetComponentInChildren<Toggle> ();
		updateColor (currentColor);
		updateToggle (toggleState);
		//this.GetComponent<CanvasGroup> ().interactable = false;
	}

	// if the button is not pressed, allow the clicking player to press it
	// otherwise, only allow the owning player to press and de-select
	public void clickButton()
	{
		if (!this.isServer) {
			GameObject localPlayer = MainMenuManager.singleton.client.connection.playerControllers [0].gameObject;
			localPlayer.GetComponent<LobbyToken> ().CmdCheckOwnership (this.gameObject);
		}
	} 

	public void clickToggle()
	{
		GameObject localPlayer = MainMenuManager.singleton.client.connection.playerControllers [0].gameObject;
		localPlayer.GetComponent<LobbyToken> ().CmdSetToggleState (this.gameObject, childToggle.isOn);
	}

	// called on the server by the player's lobby object
	// the server then updates these values, triggering the SyncVar hooks 
	public void CheckOwnership (GameObject playerToCheck)
	{
		if (associatedPlayer == playerToCheck) {
			associatedPlayer.GetComponent<LobbyToken>().setCharacter(null);
			associatedPlayer.GetComponent<LobbyToken>().selectedButton = null;
			associatedPlayer = null;
			currentColor = Color.white;
			toggleState = false;
		}else if (associatedPlayer == null) {
			associatedPlayer = playerToCheck;
			associatedPlayer.GetComponent<LobbyToken> ().setCharacter (associatedCharacter);
			associatedPlayer.GetComponent<LobbyToken>().selectedButton = this.gameObject;
			currentColor = Color.grey;
		}
	}

	// called when the holding player 
	[Command]
	public void CmdNullOut(GameObject incoming)
	{
		CheckOwnership (incoming);
	}

	public void SetToggle(bool incomingState)
	{
		toggleState = incomingState;
	}

	// --- SyncVar Hooks ---

	public void updateToggle(bool newState)
	{
		childToggle.isOn = newState;
	}

	public void enableToggle(GameObject checkAgainst)
	{
		GameObject localPlayer = MainMenuManager.singleton.client.connection.playerControllers [0].gameObject;
		if (localPlayer == checkAgainst)
			childToggle.interactable = true;
		else
			childToggle.interactable = false;
	}

	public void updateColor(Color c)
	{
		GetComponentInParent<CanvasRenderer> ().SetColor (c);
		//GameObject localPlayer = MainMenuManager.singleton.client.connection.playerControllers [0].gameObject;
	}
	
}
