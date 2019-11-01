using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class cannonBall : MonoBehaviour
{
    public Vector3 velocity { get; set; }
    public static readonly Vector3 gravity = new Vector3(0, -9.8f, 0);

    //cannon ball radius
    const float radius = 0.1f;
    const float sqrRadius = 0.01f;
    
    //added to the radius
    const float topR = StoneHenge.top + radius;
    const float rightR = StoneHenge.right + radius;

    //stonehenge corner
    readonly Vector3 TR = new Vector3(StoneHenge.right, StoneHenge.top);  //top right corner
    readonly Vector3 TL = new Vector3(StoneHenge.left, StoneHenge.top);   //top left corner
    readonly Vector3 TRn = new Vector3(-1, 1, 0).normalized; //top right normal vector

    //initial start position
    readonly Vector3 startPos = new Vector3(3.98f, -2, 0);
    const float repulse = -0.5f;

    System.DateTime immobile;
    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.position;
        transform.position = startPos;
    }

    void Update()
    {
        //compute instant velocity
        var dt = Time.deltaTime;
        var acceleration = gravity;

        //if above stonehenge and not moving backward, apply wind
        if (transform.position.y > StoneHenge.top && velocity.x < -0.01f) 
            acceleration += cloud.wind;

        //apply acceleration
        velocity += dt * acceleration;

        transform.position += velocity * dt;

        //check ground
        if (transform.position.y < -2.42f)
        {
            Destroy(gameObject);
        }

        //check screen
        if (transform.position.y>Game.scrTop|| transform.position.y < -Game.scrTop || transform.position.x>Game.scrRight|| transform.position.x < -Game.scrRight)
        {
            Destroy(gameObject);
            return;
        }

        //since cannonball cannot move backward(from handout), only top and right side should be detected
        if (checkStonehengeTopIntersection())
        {
            velocity = new Vector3(velocity.x, repulse * velocity.y, 0);
            transform.position += 2 * new Vector3(0,velocity.y,0) * dt;
            
        }
        if (checkStoneHengeRightIntersection())
        {
            velocity = new Vector3(repulse * velocity.x, velocity.y, 0);
            transform.position += 4 * new Vector3(velocity.x,0 , 0) * dt;
            
        }

        //outgoing vector = iv - 2(n dot iv)*n  
        //approximation using the rectangle normals instead of the real ones to make process faster
        if (checkStoneHengleTopRightCorner())
        {
            transform.position -= velocity * dt;
            float magnitude = velocity.magnitude * repulse;
            velocity = (velocity-2*(Vector3.Dot(velocity,TRn)*TRn)).normalized*magnitude;
            transform.position += 2 * velocity * dt;
            
        }

        if (velocity.magnitude<0.1f)
        {
            if ((System.DateTime.Now - immobile).Seconds > 2)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            immobile = System.DateTime.Now;
        }
        for (int i = 0; i < Game.ghosts.Length; i++)
        {
            if (sqrRadius + Ghost.sizeSqr > (Vector3.Distance(transform.position, Game.ghosts[i].gameObject.transform.position)))
            {
                var n = (transform.position - Game.ghosts[i].gameObject.transform.position).normalized;
                Game.ghosts[i].lastposition = Game.ghosts[i].gameObject.transform.position + (Vector3.Dot(velocity, n) * velocity) * 0.002f;
                Debug.Log("v " + (Game.ghosts[i].gameObject.transform.position - Game.ghosts[i].lastposition));
                Game.ghosts[i].angularSpeed = Game.ghosts[i].angularSpeed * rotate(Vector3.Cross(velocity, n).magnitude * 0.02f);
                Destroy(gameObject);
                return;
            }
        }
        
    }
    Matrix4x4 rotate(float angle)
    {
        Matrix4x4 m = Matrix4x4.identity;
        m.m00 = Mathf.Cos(angle);
        m.m11 = m.m00;
        m.m01 = Mathf.Sin(angle);
        m.m10 = -m.m01;
        return m;
    }
    
    bool checkStonehengeTopIntersection()
    {
        return transform.position.y < topR && transform.position.x < StoneHenge.right && transform.position.x > StoneHenge.left;
    }
    bool checkStoneHengeRightIntersection()
    {
        return transform.position.y < StoneHenge.top && transform.position.x < rightR&&transform.position.x> StoneHenge.left;
    }
    bool checkStoneHengleTopRightCorner()
    {
        return (TR-transform.position).sqrMagnitude<=sqrRadius;
    }
}
