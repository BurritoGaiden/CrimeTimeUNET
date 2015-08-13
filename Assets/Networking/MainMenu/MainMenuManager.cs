using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.Networking;


public class MainMenuManager : NetworkManager {

	private bool foundConnection = false;
	private float timeoutWindow = 0.5f;
	private Regex ipParser;

	void Start(){

		Screen.fullScreen = true;

		#if UNITY_STANDALONE || UNITY_EDITOR
			networkAddress = Network.player.ipAddress;
			Debug.Log (networkAddress);
		#endif

		#if !(UNITY_STANDALONE || UNITY_EDITOR)
			networkAddress = regexParseURL();
			StartClientFromMenu ();
		#endif

	}

	string regexParseURL()
	{
		ipParser = new Regex ("^(http|https)\\:\\/\\/(?<ip>[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}).*$");
			Match ipMatch = ipParser.Match ("http://192.168.1.9/QuickWeb/MyPage.html");
			Group group = ipMatch.Groups["ip"];
		return group.Captures [0].Value;
	}

	public void StartHostFromMenu()
	{
		Debug.Log (networkAddress);
		base.StartHost ();
	}

	public void StartClientFromMenu()
	{
		base.StartClient ();
		StartCoroutine ("timeoutCountdown");
	}

	public override void OnClientError (NetworkConnection conn, int errorCode)
	{
		base.OnClientError (conn, errorCode);
	}
	

	public override void OnClientConnect (NetworkConnection conn)
	{
		foundConnection = true;
		Debug.Log ("Server found, client joined!");
	}

	public IEnumerator timeoutCountdown() {
		yield return new WaitForSeconds (timeoutWindow);
		if (foundConnection == false) {
			StopClient();
			Debug.Log ("No server found, client timed out!");
			yield return null;
		}
		yield return null;
	}


}
