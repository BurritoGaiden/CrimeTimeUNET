using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

public class txtParser
{
	public static TextTable Parse(TextAsset asset)
	{
		return Parse (new StringReader (asset.text));
	}
	
	public static TextTable Parse(TextReader reader)
	{
		TextTable table = new TextTable();
		
		if (regularExpressionParser == null)
			loadRegEx ();
		
		string tableString = reader.ReadToEnd ();
		
		MatchCollection matches = regularExpressionParser.Matches (tableString);
		
		foreach (Match m in matches) 
		{
			if(!m.Success)
			{
				string logError = "Error: Could not parse string in file matching \"" + m.Value + "\"";
				Debug.LogError(logError);
				return null;
			}
			
			string key = m.Groups["tag"].Value;
			Group group = m.Groups["text"];
			
			string s = "";
			foreach(Capture c in group.Captures)
			{
				s += c.Value + "\n";
			}
			
			if(s.Length > 0)
				s = s.Substring(0, s.Length - 1);
			
			table.AddEntry(key, s);
		}
		
		return table;
	}
	
	private static void loadRegEx()
	{
		
		regularExpressionParser = new Regex ("(?<tag>#[\\w]+)[ \\t]+({(?<text>[d+\\w \\t\\n\\r\\+\\-\\[\\]\\!\\?\\@\\#\\$\\%\\^\\&\\*\\'\\:\\(\\)\\=\\\\\\/\\<\\>\\,\\.\\\"]*)}[\\n]*[ \\t]*)+");
	}
	
	private static Regex regularExpressionParser;
}

public class TextTable
{
	public string this[string key]
	{
		get{ return table [key];}
		set{ table.Add (key, value);}
	}
	
	public void AddEntry(string key, string value)
	{
		table.Add (key, value);
	}
	
	public bool RemoveEntry(string key)
	{
		bool worked = table.Remove (key);
		return worked;
	}
	
	public bool HasEntry(string key)
	{
		return table.ContainsKey (key);
	}
	
	private Dictionary<string, string> table = new Dictionary<string, string>();
}