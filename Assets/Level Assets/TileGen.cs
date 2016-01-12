using UnityEngine;
using System.Collections;

public class TileGen : MonoBehaviour {
	

	[SerializeField]
	private GameObject tilePrefab;

	private GameObject[,] tileArray;
    public GameObject[,] TileArray
    {
        get { return tileArray; }
    }

    [SerializeField]
	private int fieldSize;

	// Use this for initialization
	void Start () {

		generateTiles ();


	}

	void generateTiles(){
		tileArray = new GameObject[fieldSize,fieldSize];
		int position;
		for (int i = 0; i< fieldSize; i++)
		for(int j = 0; j< fieldSize; j++){
			position = (i*fieldSize + j);
			tileArray[i,j] = (GameObject)Instantiate(tilePrefab, new Vector3(i, 0, j), Quaternion.identity);
                tileArray[i, j].transform.parent = this.transform;
			tileArray[i,j].transform.Rotate(new Vector3(90,0,0));
			if (position%2 == 0)
				tileArray[i,j].GetComponent<MeshRenderer>().material.color = Color.grey;
			tileArray[i,j].GetComponent<TileBehavior>().X = i;
			tileArray[i,j].GetComponent<TileBehavior>().Z = j;
		}
	}

}
