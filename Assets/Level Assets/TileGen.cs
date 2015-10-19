using UnityEngine;
using System.Collections;

public class TileGen : MonoBehaviour {
	

	[SerializeField]
	private GameObject tilePrefab;

	private GameObject[] tileArray;

	[SerializeField]
	private int fieldSize;

	// Use this for initialization
	void Start () {

		generateTiles ();


	}

	void generateTiles(){
		tileArray = new GameObject[fieldSize*fieldSize];
		int position;
		for (int i = 0; i< fieldSize; i++)
		for(int j = 0; j< fieldSize; j++){
			position = (i*fieldSize + j);
			tileArray[position] = (GameObject)Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity);
			tileArray[position].transform.Rotate(new Vector3(90,0,0));
			if (position%2 == 0)
				tileArray[position].GetComponent<MeshRenderer>().material.color = Color.grey;
			tileArray[position].GetComponent<TileBehavior>().X = i;
			tileArray[position].GetComponent<TileBehavior>().Z = j;
		}

	}
	void spawnPlayer(){

	}

}
