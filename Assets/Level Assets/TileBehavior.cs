using UnityEngine;
using System.Collections;
using System;


public class TileBehavior : MonoBehaviour {

    private Coordinate coords;
    public Coordinate Coords
    {
        get { return coords; }
        set { coords = value; }
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
