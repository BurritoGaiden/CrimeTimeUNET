using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.IO;

public class MapRule : WebServerRule
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
		string s = reader.ReadToEnd();
		string ID = request.RemoteEndPoint.Address.ToString();

		JSONWrapper j = new JSONWrapper(s);

		if (WebServer.controllerRoster.ContainsKey (ID)) {
			CommandPanel cp = WebServer.controllerRoster [ID].GetComponent<CommandPanel> ();
			Debug.Log("Processing tile data");
			// TODO: rewrite this because holy shit it uses a public accessor
			cp.pathSelection (FindObjectOfType<TileGen> ().tileArray [int.Parse (j ["x"]), int.Parse (j ["z"])]);
			Debug.Log ("x:" + j ["x"] + "," + "z:" + j ["z"]);
		}

		byte[] data = Encoding.ASCII.GetBytes(dataString);
		
		yield return null;
		
		HttpListenerResponse response = context.Response;
		
		response.ContentType = "text/plain";
		
		Stream responseStream = response.OutputStream;
		
		int count = data.Length;
		int i = 0;
		while(i < count)
		{
			if (i != 0)
				yield return null;
			
			int writeLength = Math.Min((int)writeStaggerCount, count - i);
			responseStream.Write(data, i, writeLength);
			i += writeLength;
		}
	}
	
	#endif
	
	[SerializeField]
	private HtmlKeyValue[] substitutions;
	
	[SerializeField]
	[Tooltip("How many bytes to write before waiting a frame to continue.")]
	private uint writeStaggerCount = 4096;
}
