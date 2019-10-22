using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBody : MonoBehaviour
{
    public static float updateRate;
    public Vector3 velocity { get; set; }
    public Vector3 acceleration { get; set; }
    public Vector3 lastPosition { get; set; }
    // Start is called before the first frame update
    public void Update()
    {
        lastPosition = transform.position;
        velocity += acceleration;
        transform.position = lastPosition + velocity;
    }
}
