using UnityEngine;
using System.Collections;
using System;
using UnityEditor;

[System.Serializable]
public class Map : MonoBehaviour, IJSONable{

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

    public void InitializeMap()
    {
        tileset.InitializeDictionary();
    }

    public IJSON ToJSON()
    {
        MapJSON json = new MapJSON();
        json.width = layout.width;
        json.height = layout.height;
        json.imagePath = "./Resources/maps/"+layout.name+".png";
        return json;
    }
}
