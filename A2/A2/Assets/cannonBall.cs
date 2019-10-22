using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class cannonBall : MonoBehaviour
{
    public static System.Timers.Timer t;
    public static Vector3 velocity { get; set; }
    public static readonly Vector3 gravity = new Vector3(0, -9.8f, 0);

    //cannon ball radius
    const float radius = 0.1f;
    const float sqrRadius = 0.01f;
    //stonehenge properties
    const float top = -1.22f;
    const float right = 0.74f;
    const float left = -1.4f;
    //added to the radius
    const float topR = top + radius;
    const float rightR = right + radius;
    //stonehenge corner
    readonly Vector3 TR = new Vector3(right, top);  //top right corner
    readonly Vector3 TL = new Vector3(left, top);   //top left corner
    readonly Vector3 TRn = new Vector3(-1, 1, 0).normalized; //top right normal vector
    readonly Vector3 TLn = new Vector3(-1, -1, 0).normalized; //top right normal vector

    const float repulse = -0.5f;

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //compute instant velocity
        var dt = Time.deltaTime;
        var acceleration = gravity;

        //if above stonehenge and not moving backward, apply wind
        if (transform.position.y > top && velocity.x < -0.01f) 
            acceleration += cloud.wind;

        //apply acceleration
        velocity += dt * acceleration;

        transform.position += velocity * dt;

        //check ground
        if (transform.position.y < -2.42f)
        {
            gameObject.SetActive(false);
        }

        //since cannonball cannot move backward(from handout), only top and right side should be detected
        if (checkStonehengeTopIntersection())
        {
            
            velocity = new Vector3(velocity.x, repulse * velocity.y, 0);
            transform.position += 2 * new Vector3(0,velocity.y,0) * dt;
            return;
        }
        if (checkStoneHengeRightIntersection())
        {
            
            velocity = new Vector3(repulse * velocity.x, velocity.y, 0);
            transform.position += 4 * new Vector3(velocity.x,0 , 0) * dt;
            return;
        }

        //outgoing vector = iv - 2(n dot iv)*n  
        //approximation using the rectangle normals instead of the real ones to make process faster
        if (checkStoneHengleTopRightCorner())
        {
            Debug.Log(velocity);
            transform.position -= velocity * dt;
            float magnitude = velocity.magnitude * repulse;
            velocity = (velocity-2*(Vector3.Dot(velocity,TRn)*TRn)).normalized*magnitude;
            transform.position += 2 * velocity * dt;
            Debug.Log(velocity);
            return;
        }
        if (checkStoneHengleTopLeftCorner())
        {
            Debug.Log(velocity);
            transform.position -= velocity * dt;
            float magnitude = velocity.magnitude * repulse;
            velocity = (velocity - 2*(Vector3.Dot(velocity, TLn) * TLn)).normalized*magnitude;
            transform.position += 4 * velocity * dt;
            Debug.Log(velocity);
            return;
        }
        
        
    }

    
    bool checkStonehengeTopIntersection()
    {
        return transform.position.y < topR && transform.position.x < right && transform.position.x > left;
    }
    bool checkStoneHengeRightIntersection()
    {
        return transform.position.y < top && transform.position.x < rightR&&transform.position.x>left;
    }
    bool checkStoneHengleTopRightCorner()
    {
        return (TR-transform.position).sqrMagnitude<=sqrRadius;
    }
    bool checkStoneHengleTopLeftCorner()
    {
        return (TL - transform.position).sqrMagnitude <= sqrRadius;
    }
}
