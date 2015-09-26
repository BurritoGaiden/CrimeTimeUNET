using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class txtLookup : MonoBehaviour {

	[SerializeField]
	private TextAsset stringsFile;

	private static TextTable stringsList;
	private static txtParser parser;
	

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad (this.gameObject);
		stringsList = txtParser.Parse (stringsFile);
		parseStrings ();

	}

	void OnLevelWasLoaded(int level)
	{
		//parseStrings ();
	}

	void parseStrings()
	{
		Text[] textObjects = FindObjectsOfType<Text> ();
		foreach (Text t in textObjects) {
			t.text = updateString(t.text);
		}
	}

	// --- Static Functions ---

	public static TextTable getInstance()
	{
		return stringsList;
	}
	
	public bool checkLibrary(string toCheck)
	{
		try{
			string checker = getInstance()[toCheck];
		}
		catch(Exception e){
			Debug.Log(e.Message);
			return false;
		}
		return true;
	}

	public static string updateString(string s)
	{
		if (s.StartsWith ("#"))
			return (getInstance () [s]);
		else
			return s;
	}

	// ---
	
}
