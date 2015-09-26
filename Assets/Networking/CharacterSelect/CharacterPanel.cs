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
	private GameObject associatedPlayer;

	[SerializeField]
	private GameObject associatedCharacter;
	private CharacterBehavior associatedCharacterBehavior;

	private GameObject nullPlayer;

	[SerializeField]
	private GameObject helpMenu;

	// --- Card Elements ---
	[SerializeField]
	private Text firstName, lastName;

	[SerializeField]
	private Image healthBar, moveBar, gunBar, CQCBar; 

	private Image[] healthPips, movePips, gunPips, CQCPips;

	// Use this for initialization
	void Start () 
	{
		initCardInfo ();
		nullPlayer = GameObject.FindGameObjectWithTag ("DummyPlayer");

		childToggle = transform.parent.GetComponentInChildren<Toggle> ();
		updateColor (currentColor);
		updateToggle (toggleState);
		//this.GetComponent<CanvasGroup> ().interactable = false;
	}

	void initCardInfo()
	{
	Color32 faded = new Color32 (167, 167, 167, 128);

	if (firstName != null && lastName != null) {
			associatedCharacterBehavior = associatedCharacter.GetComponent<CharacterBehavior> ();
			associatedCharacterBehavior.initCharacter ();

			firstName.text = associatedCharacterBehavior.getFirstName ().ToUpper();
			lastName.text = associatedCharacterBehavior.getLastName ().ToUpper();

			healthPips = healthBar.gameObject.GetComponentsInSiblings<Image>();
			healthPips.updatePips(associatedCharacterBehavior.getHP(), Color.white, faded);
		}
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

	public void clickHelpMenu(){
		helpMenu.SetActive (true);
		Text title = helpMenu.GetComponentInChildren<Text>();
		title.text =  associatedCharacterBehavior.getFirstName ().ToUpper () + " " 
					+ associatedCharacterBehavior.getLastName ().ToUpper ();
		title.gameObject.GetComponentsInSiblings<Text>()[0].text = associatedCharacterBehavior.getHelpText ();
		//Text body = helpMenu.transform.FindChild ("HelpItemBody");
		//body = associatedCharacterBehavior.getHelpText ();
	}

	// called on the server by the player's lobby object
	// the server then updates these values, triggering the SyncVar hooks 
	public void CheckOwnership (GameObject playerToCheck)
	{
		if (associatedPlayer == playerToCheck) {
			associatedPlayer.GetComponent<LobbyToken>().setCharacter(nullPlayer);
			associatedPlayer.GetComponent<LobbyToken>().selectedButton = nullPlayer;
			associatedPlayer = nullPlayer;
			currentColor = Color.white;
			toggleState = false;
		}else if (associatedPlayer == nullPlayer) {
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
