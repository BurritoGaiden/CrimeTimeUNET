using UnityEngine;
using System.Collections;
using System;

public interface IJSONable
{
    IJSON ToJSON();
}

public interface IJSON
{

}

[Serializable]
public class ConnectionJSON : IJSON
{
    public bool pass;
    public string message;
}

[Serializable]
public class MapJSON : IJSON
{
    public int width;
    public int height;
    public string imagePath;
    
}

[Serializable]
public class PlayerJSON: IJSON
{
    public string username;
    public bool connected;
}

[Serializable]
public class ActorJSON : IJSON
{
    public Coordinate coords;

}


[Serializable]
public class CharacterJSON : ActorJSON
{
    public string name;
    public string username;
    public Alliance team;
    public Stats stats;

}

[Serializable]
public class ItemJSON : ActorJSON
{

}


