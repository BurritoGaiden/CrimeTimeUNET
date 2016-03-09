using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Heartbeat: IJSON {

    public GameState state;
    public PlayerJSON[] players;
    public CharacterJSON myCharacter;
    public CharacterJSON[] characters;

}
