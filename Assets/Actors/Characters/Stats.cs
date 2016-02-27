using System;

[Serializable]
public class Stats
{
    public int hp;
    public int gun;
    public int melee;
    public int movement;
    public int defense;

    public Stats(int hpstat, int gunstat, int meleestat, int movestat, int defstat)
    {
        hp = hpstat;
        gun = gunstat;
        melee = meleestat;
        movement = movestat;
        defense = defstat;
    }
}
