using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //flags
    public static bool onset = false;

    //constants
    public static readonly Vector3 jumpVector = new Vector3(0,10,0);    //the jump force
    public static readonly Vector3 startPosition = new Vector3(21, 1, 5);    //the jump force
    public const float Hsensitivity = 10;   //horizontal sensitivity of the camera
    public const float bulletForce = 20;    //initial speed of the bullet when it spawn
    public const float velocity = 0.1f;     //velocity of player's movement

    //variables
    public bool OnGround = true;    //if player on ground
    GameObject bullet;
    

    public void Start()
    {
        GetComponent<Rigidbody>().maxAngularVelocity = 0;   //ignore friction with other objects
        bullet = GameObject.Find("Bullet");     //find the bullet gameobject 
        bullet.GetComponent<Rigidbody>().maxAngularVelocity = 0;    //ignore friction with air
        bullet.SetActive(false);    //hide the bullet
    }
   
    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape) && !onset)
        {
            Game.resetCount++;
            Game.Reset();
            return;
        }




        //rotate camera based on mouse
        transform.Rotate(Game.UpVector, Input.GetAxis("Mouse X")*Hsensitivity);

        //detect wasd movements
        if (OnGround)
        {
            if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * velocity;
            if (Input.GetKey(KeyCode.S)) transform.position -= transform.forward * velocity;
            if (Input.GetKey(KeyCode.A)) transform.position -= Game.Agnle90 * transform.forward * velocity;
            if (Input.GetKey(KeyCode.D)) transform.position += Game.Agnle90 * transform.forward * velocity;
        }
        

        //detect jump
        if (Input.GetKey(KeyCode.Space)&&OnGround)
        {
            OnGround = false;
            GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.Impulse);
        }

        //detect shoot bullet
        if (Input.GetMouseButton(0)&&!bullet.activeSelf)
        {
            bullet.SetActive(true);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        }

        
    }
    public void OnCollisionEnter(Collision collision)
    {
        //detect if player on ground
        if (collision.gameObject.name.Equals("Ground"))
        {
            OnGround = true;
        }
    }
}
