using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


public static class ExtensionMethods {

	public static List<int> AllIndexesOf(this string str, string value) {
		if (String.IsNullOrEmpty(value))
			throw new ArgumentException("the string to find may not be empty", "value");
		List<int> indexes = new List<int>();
		for (int index = 0;; index += value.Length) {
			index = str.IndexOf(value, index);
			if (index == -1)
				return indexes;
			indexes.Add(index);
		}
	}
	
	// updates a pip bar with a new number of filled-in pips
	public static void updatePips(this Image[] pipArray, int newValue, Color on, Color off){

		if (newValue <= pipArray.Length) {
			for (int i = 0; i < newValue; i++) {
				pipArray [i].color = on;
			}
			for (int i = newValue; i < pipArray.Length; i++) {
				pipArray [i].color = off;
			}
		}
	}
	// returns an array of all the GameObjects adjacent in heirachy to the one that this method is called upon
	// excludes self by default
	public static GameObject[] GetSiblings(this GameObject obj){
		return GetSiblings (obj, true);
	}

	public static GameObject[] GetSiblings(this GameObject obj, bool excludeSelf){
		List<GameObject> list = new List<GameObject>();
		foreach(Transform child in obj.transform.parent) {
			if((!child.gameObject.Equals(obj) && excludeSelf) || !excludeSelf){
				list.Add(child.gameObject);
			}
		}
		GameObject[] siblingList = list.ToArray ();
		return siblingList;
	}

	// returns an array of all the components of type T in the the GameObjects adjacent in heirachy 
	// to the one that this method is called upon

	public static T[] GetComponentsInSiblings<T>(this GameObject obj){
		return obj.GetComponentsInSiblings<T> (true);
	}

	public static T[] GetComponentsInSiblings<T>(this GameObject obj, bool excludeSelf){

		GameObject[] siblingsList = obj.GetSiblings(excludeSelf);

		List<T> componentList = new List<T>();
		for (int i = 0; i < siblingsList.Length; i++) {
			if(siblingsList[i].GetComponent<T>() != null)
				componentList.Add(siblingsList[i].GetComponent<T>());
		}
		return componentList.ToArray();
	}

}
