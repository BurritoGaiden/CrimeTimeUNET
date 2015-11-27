using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net;
using System.IO;

public class ControllerInitRule : WebServerRule
{
	
	[Serializable]
	public struct HtmlKeyValue
	{
		[SerializeField]
		private string key;
		public string Key
		{
			get { return key; }
		}
		
		[SerializeField]
		private string value;
		public string Value
		{
			get { return value; }
			set { this.value = value; }
		}
		
		public HtmlKeyValue(string key, string value)
		{
			this.key = key;
			this.value = value;
		}	
	}
	
	#if UNITY_STANDALONE || UNITY_EDITOR
	
	protected virtual string ModifyHtml(string html)
	{
		return html;
	}
	
	protected override IEnumerator OnRequest(HttpListenerContext context)
	{
		string dataString = "";

		HttpListenerRequest request = context.Request;
		StreamReader reader = new StreamReader(request.InputStream);
		string command = reader.ReadToEnd();
		string ID = request.RemoteEndPoint.Address.ToString();

		if ((!WebServer.controllerRoster.ContainsKey (ID)) && (WebServer.controllerRoster.Count < 5)) {
			GameObject newController = (GameObject)Instantiate (controllerPrefab, Vector3.zero, Quaternion.identity);
			newController.name = ID;
			WebServer.controllerRoster.Add (ID, newController);
		} else if (WebServer.controllerRoster.ContainsKey (ID)) {
			CommandPanel cp = WebServer.controllerRoster[ID].GetComponent<CommandPanel>();

			if(command.Equals("commitMove")){
				cp.commitToMove();
			}
		}

		yield return null;
	}
	
	#endif

	[SerializeField]
	private GameObject controllerPrefab;

	[SerializeField]
	private int charSelectIndex;

	[SerializeField]
	private HtmlKeyValue[] substitutions;
	
	[SerializeField]
	[Tooltip("How many bytes to write before waiting a frame to continue.")]
	private int writeStaggerCount = 4096;
}
