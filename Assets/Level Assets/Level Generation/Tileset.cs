using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tileset : MonoBehaviour
{

    [System.Serializable]
    public class TypeToTileEntry
    {
        public TileType type;
        public GameObject tile;
    }

    [SerializeField]
    private TypeToTileEntry[] typeToTileMap;

    private Dictionary<TileType, GameObject> typeToTile = new Dictionary<TileType, GameObject>();
    public Dictionary<TileType, GameObject> TypeToTile
    {
        get { return typeToTile; }
    }

    private float size;
    public float Size
    {
        get { return size; }
    }

    public void InitializeDictionary()
    {
        size = typeToTileMap[0].tile.GetComponent<MeshRenderer>().bounds.size.x;
        foreach (TypeToTileEntry t in typeToTileMap)
            typeToTile.Add(t.type, t.tile);
    }

}
