using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileGen : MonoBehaviour {
	

	[SerializeField]
	private GameObject tilePrefab;

    [System.Serializable]
    public class ColorToTileEntry
    {
        public Color color;
        public TileType tile;
    }

    [SerializeField]
    private ColorToTileEntry[] colorToTileMap;

    private Dictionary<Color, TileType> colorToTile = new Dictionary<Color, TileType>();

	private GameObject[,] tileArray;
    public GameObject[,] TileArray
    {
        get { return tileArray; }
    }

    [SerializeField]
	private int fieldSize;

    private int fieldSizeX;
    private int fieldSizeZ;

    // Use this for initialization
    void Start () {

		//generateTiles ();
        foreach (ColorToTileEntry c in colorToTileMap)
            colorToTile.Add(c.color, c.tile);

	}

	void generateTiles(){
		tileArray = new GameObject[fieldSize,fieldSize];
        float tileSpacing = tilePrefab.GetComponent<MeshRenderer>().bounds.size.x;
        int position;
		for (int i = 0; i< fieldSize; i++)
		for(int j = 0; j< fieldSize; j++){
			position = (i*fieldSize + j);
			tileArray[i,j] = (GameObject)Instantiate(tilePrefab, new Vector3(i*tileSpacing, 0, j*tileSpacing), Quaternion.identity);
                tileArray[i, j].transform.parent = this.transform;
			tileArray[i,j].transform.Rotate(new Vector3(90,0,0));
			if (position%2 == 0)
				tileArray[i,j].GetComponent<MeshRenderer>().material.color = Color.grey;
			tileArray[i,j].GetComponent<TileBehavior>().X = i;
			tileArray[i,j].GetComponent<TileBehavior>().Z = j;
		}
	}

    public void generateFromMap(Map map)
    {
        fieldSizeX = map.Layout.width;
        fieldSizeZ = map.Layout.height;
        tileArray = new GameObject[fieldSizeX, fieldSizeZ];
        float tileSpacing = map.Tileset.Size;

        for (int i = 0; i < fieldSize; i++)
            for (int j = 0; j < fieldSize; j++)
            {
                TileType t = colorToTile[map.Layout.GetPixel(i, j)];
                tilePrefab = map.Tileset.TypeToTile[t];

                tileArray[i, j] = (GameObject)Instantiate(tilePrefab, new Vector3(i * tileSpacing, 0, j * tileSpacing), Quaternion.identity);
                tileArray[i, j].transform.parent = this.transform;
                tileArray[i, j].transform.Rotate(new Vector3(90, 0, 0));

                tileArray[i, j].GetComponent<TileBehavior>().X = i;
                tileArray[i, j].GetComponent<TileBehavior>().Z = j;
            }
    }

}
