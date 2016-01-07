using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyReadyUp : NetworkBehaviour {

	[SyncVar(hook="checkReadyUps")]
	private int readyUps = 0;
	
	private int countdown = 5;

	[SerializeField]
	private Text countdownText;
	[SerializeField]
	private GameObject countdownPanel;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	[Command]
	public void CmdUpdateReadies(bool incoming)
	{
		if (incoming)
			readyUps++;
		else
			readyUps--;

	}

	public void checkReadyUps(int newReadys)
	{
		if (readyUps == FindObjectsOfType<CharacterPanel> ().Length) {
			Debug.Log ("All characters chosen and ready!");
			countdown = 5;
			countdownPanel.SetActive(true);
			StartCoroutine ("countdownCoroutine");
		}
		else {
			countdownPanel.SetActive(false);
			StopCoroutine("countdownCoroutine");
		}
	}
	
	public IEnumerator countdownCoroutine()
	{
		while (countdown >= 0) {
			if(countdown > 0)
				countdownText.text = countdown.ToString ();
			else
				countdownText.text = "GO!";
			GetComponent<AudioSource> ().Play ();
			yield return new WaitForSeconds (1.0f);
			countdown--;
		}
		finishLobby ();
		yield break;
	}

	public void finishLobby()
	{
		MainMenuManager.singleton.ServerChangeScene ("PostLobbyGame");
	}
}
