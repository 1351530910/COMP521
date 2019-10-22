using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collider
{
    public abstract bool detectCollision(Collider other);
}
public class SCollider:Collider
{
    public Vector3 center { get; set; }
    public float radius { get; set; }
    public override bool detectCollision(Collider other)
    {
        throw new System.NotImplementedException();
    }
}
