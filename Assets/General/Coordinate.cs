using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Coordinate {

    public int x, z;
    public int X
    {
        get { return x; }
    }
    public int Z
    {
        get { return z; }
    }

    public Coordinate(int xPos, int zPos)
    {
        x = xPos;
        z = zPos;
    }

    public override string ToString()
    {
        return x + "," + z;
    }

    public static Coordinate operator +(Coordinate c1, Coordinate c2)
    {
        return new Coordinate((c1.X + c2.X), (c1.Z + c2.Z));
    }

    public static Coordinate operator -(Coordinate c1, Coordinate c2)
    {
        return new Coordinate((c1.X - c2.X), (c1.Z - c2.Z));
    }


}
