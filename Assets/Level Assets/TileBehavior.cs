using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour {

    private Coordinate coord;
    public Coordinate Coords
    {
        get { return coord; }
        set { coord = value; }
    }

    private TileType tt;
    public TileType TileType
    {
        get { return tt; }
        set
        {
            tt = value;
            DeterminePathing(tt);
        }
    }

    private bool isPathable = true;
    public bool IsPathable
    {
        get { return isPathable; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
    void DeterminePathing(TileType incomingType)
    {
        switch (incomingType)
        {
            case TileType.Wall:
                isPathable = false;
                break;

            default:
                isPathable = true;
                break;
        }
    }


	void OnMouseDown(){
		//Debug.Log ("This tile is: " + x + "," + z)
	}
}
