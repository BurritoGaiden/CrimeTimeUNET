using UnityEngine;
using System.Collections;

public class TileBehavior : MonoBehaviour {

    private Coordinate coord;
    public Coordinate Coord
    {
        get { return coord; }
        set { coord = value; }
    }

    /*
	private int x, z;
	public int X
	{
		get {return x; }
		set {x  = value;}
	}
	public int Z
	{
		get {return z; }
		set {z  = value;}
	}
    */
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
		//Debug.Log ("This tile is: " + x + "," + z);
		FindObjectOfType<CommandPanel> ().PathSelection (this);
	}
}
