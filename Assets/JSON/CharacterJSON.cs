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
    public string owner;
    public Alliance team;
    public Stats stats;

}

[Serializable]
public class ItemJSON : ActorJSON
{

}


