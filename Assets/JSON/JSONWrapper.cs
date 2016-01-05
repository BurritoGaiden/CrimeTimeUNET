using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JSONWrapper : Dictionary<string, string>{

	//private Dictionary<string, string> table = new Dictionary<string, string>();


	//converts a JSON string into a usable datatable
	public JSONWrapper(string encoded){
		string[] andSplit = encoded.Split ('&');
		for (int i = 0; i < andSplit.Length; i++) {
			string[] temp = andSplit[i].Split('=');
			this.Add(temp[0], temp[1]);
		}
	}

	public string FindFromKey(string key){
		if (this.ContainsKey (key)) {
			return this[key];
		} else
			return "";
	}
}
