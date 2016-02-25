using UnityEngine;
using System.Collections;

public abstract class MapActor : MonoBehaviour, IJSONable {


    private Coordinate coord;
    public Coordinate Coords
    {
        get { return coord; }
        set { coord = value; }
    }

    private bool isVisible = true;
    public bool IsVisible
    {
        get { return isVisible; }
        set { isVisible = value; }
    }

    public virtual IJSON ToJSON()
    {
        ActorJSON json = new ActorJSON();
        json.coords = Coords;
        return json;
    }
  
}
