using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Heartbeat {

    public GameState state;
    public PlayerJSON[] players;
    public CharacterJSON[] characters;

}
