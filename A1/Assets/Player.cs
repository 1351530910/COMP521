using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool jump = false;   //if player is in air
    float horizontal = 0f;      //horizontal angle of the camera
    float vertical = 0f;        //vertical angle of the camera

    const float Hsensitivity = 10.0f;   //horizontal sensitivity of the camera
    const float Vsensitivity = 5.0f;   //vertical sensitivity of the camera
    const float velocity = 0.1f;  //movement speed of the player
    const float jumpforce = 5.0f;   //how high the player jump
    public static readonly Quaternion rotate90 = Quaternion.Euler(0, 90, 0);  //multiplication matrix that rotate a vector by 90degree on axis Y


    // Start is called before the first frame update
    void FixedUpdate()
    {
        //compute new camera direction
        horizontal -= Input.GetAxis("Mouse Y") * Hsensitivity;
        vertical += Input.GetAxis("Mouse X") * Vsensitivity;

        //rotate player and the camera
        transform.eulerAngles = new Vector3(horizontal, vertical, 0);

        //remove y axis from forward direction
        var forward = transform.forward;
        forward.y = 0;
        forward.Normalize();

        //get input and move the player
        if (Input.GetKey(KeyCode.W)) transform.position += forward * velocity;
        if (Input.GetKey(KeyCode.S)) transform.position -= forward * velocity;
        if (Input.GetKey(KeyCode.A)) transform.position -= rotate90 * forward * velocity;
        if (Input.GetKey(KeyCode.D)) transform.position += rotate90 * forward * velocity;

        //shoot a bullet if player left click
        if (Input.GetMouseButton(0) && !GameLogic.Bullet.activeSelf) GameLogic.shoot();

        //if not jump
        if (!jump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += GameLogic.UpVector / 10;
                GetComponent<Rigidbody>().AddForce(GameLogic.UpVector * jumpforce, ForceMode.Impulse);
                jump = true;
            }
            else
            {
                //reset player inertia
                GetComponent<Rigidbody>().velocity = GameLogic.ZeroVector;
                
            }
        }
        else
        {
            if (transform.position.y <= 1)
            {
                jump = false;
            }
        }
        if (transform.position.y>1)
        {

            GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = false;
        }

        //check if player fall in a hole
        if (transform.position.y < -10) GameLogic.Init();
    }
}
