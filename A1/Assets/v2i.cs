using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//vector 2d int
public class v2i
{
    public int x { get; set; }
    public int y { get; set; }
    public v2i(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
    public override bool Equals(object o)
    {
        v2i b = (v2i)o;
        return x == b.x && y == b.y;
    }
    public static bool operator ==(v2i a, v2i b)
    {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(v2i a, v2i b)
    {
        return a.x != b.x || a.y != b.y;
    }
    public static v2i operator +(v2i a, v2i b)
    {
        return new v2i(a.x + b.x, a.y + b.y);
    }
    public static v2i operator -(v2i a, v2i b)
    {
        return new v2i(a.x + b.x, a.y + b.y);
    }
    public static v2i operator -(v2i a)
    {
        return new v2i(-a.x, -a.y);
    }
    public override string ToString()
    {
        return x + " " + y;
    }
}
