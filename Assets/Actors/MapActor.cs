using UnityEngine;
using System.Collections;

public class MapActor : MonoBehaviour {

    private Coordinate coord;
    public Coordinate Coord
    {
        get { return coord; }
        set { coord = value; }
    }
}
