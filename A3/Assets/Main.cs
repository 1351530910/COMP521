using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class Main 
{
    public static List<Trader> traders = new List<Trader>();

    [RuntimeInitializeOnLoadMethod]
    public static void main()
    {
        
        for (int i = 1; i < 9; i++)
            traders.Add(new Trader(GameObject.Find($"{i}")));
        traders = traders.OrderBy(x => Guid.NewGuid()).ToList();
        for (int i = 0; i < traders.Count; i++)
            traders[i].setn(i);
        traders = traders.OrderBy(x => x.n).ToList();
    }
}
