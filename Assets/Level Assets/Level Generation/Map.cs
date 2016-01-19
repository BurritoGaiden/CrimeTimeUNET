using UnityEngine;
using System.Collections;

[System.Serializable]
public class Map : MonoBehaviour{

    [SerializeField]
    private string mapName;
    public string MapName
    {
        get { return mapName; }
    }

    [SerializeField]
    private Texture2D layout;
    public Texture2D Layout
    {
        get { return layout; }
    }

    [SerializeField]
    private Tileset tileset;
    public Tileset Tileset
    {
        get { return tileset; }
    }

}
