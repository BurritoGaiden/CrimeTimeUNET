using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.IO;

public class ResourcesRule : WebServerRule
{
	[Serializable]
	public struct HtmlKeyValue
	{
		public string Key
		{
			get { return key; }
		}
		
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
		
		[SerializeField]
		private string key;
		[SerializeField]
		private string value;
	}
	
	#if UNITY_STANDALONE || UNITY_EDITOR
	
	protected override IEnumerator OnRequest(HttpListenerContext context)
	{
	
		HttpListenerRequest request = context.Request;
		TextAsset textItem = new TextAsset ();

		string url = request.RawUrl;
		Debug.Log ("RAW: " + url);


		string subFolder = "";
		string[] directories = url.Split ('/');
		for (int s = 3; s< directories.Length - 1; s++) {
			subFolder += directories [s]+"/";
		}

		string item = url.Substring(url.LastIndexOf('/') + 1);
		Debug.Log ("ITEM: " + item);

		string itemName = item.Substring(0, item.IndexOf('.'));
		Debug.Log ("NAME: " + itemName);



		// "HesitNight/sprites -> "Resources/www/sprites/asset"
		string path = string.Format ("www/Website/{0}{1}", subFolder, itemName);
		Debug.Log ("PATH: " + path);

		textItem = Resources.Load(path) as TextAsset;

		yield return null;

		///
		
		HttpListenerResponse response = context.Response;

		byte[] data = textItem.bytes;
		
		response.ContentType = "text/html";
		
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

