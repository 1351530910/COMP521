using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using System;

public class GameLogic : MonoBehaviour
{
    public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    public static GameObject Player;
    public static GameObject Bullet;
    public static GameObject Camera;
    public static GameObject[,] Maze;


    const float jumpforce = 5.0f;   //how high the player jump
    const float Hsensitivity = 10.0f;   //horizontal sensitivity of the camera
    const float Vsensitivity = 5.0f;   //vertical sensitivity of the camera
    const float BulletSpeed = 50.0f;   //initial speed of the bullet
    const float velocity = 0.1f;  //movement speed of the player
    const float MaxBulletRange = 100f;  //maximum distance a bullet can travel
    public static readonly Vector3 ZeroVector = new Vector3(0, 0, 0);       //empty vector
    public static readonly Quaternion rotate90 = Quaternion.Euler(0,90,0);  //multiplication matrix that rotate a vector by 90degree on axis Y
    public static readonly Vector3 UpVector = new Vector3(0, 1, 0);         //up vector for the camera



    
    
    public bool jump = false;   //if player is in air
    float horizontal = 0f;      //horizontal angle of the camera
    float vertical = 0f;        //vertical angle of the camera
    

    [RuntimeInitializeOnLoadMethod]
    public static void initialize()
    {
        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            prefabs.Add(obj.name, obj);
            obj.SetActive(false);
        }
        Init();
    }

    public static void Init()
    {
        if (Player!=null)
        {
            Destroy(Camera);
            Destroy(Player);
            Destroy(Bullet);
        }
        //initialize player and bullet
        Player = Instantiate(prefabs["Player"]);
        
        Camera = Player.transform.GetChild(1).gameObject;
        Bullet = Player.transform.GetChild(0).gameObject;
        Player.SetActive(true);
        Camera.SetActive(true);
        Bullet.SetActive(false);



        var ground  = Instantiate(prefabs["Ground"]);
        ground.SetActive(true);

    }
    void FixedUpdate()
    {
        //compute new camera direction
        horizontal -= Input.GetAxis("Mouse Y") * Hsensitivity;
        vertical += Input.GetAxis("Mouse X") * Vsensitivity;
        
        //rotate player and the camera
        Player.transform.eulerAngles = new Vector3(horizontal,vertical,0);

        //remove y axis from forward direction
        var forward = Player.transform.forward;
        forward.y = 0;
        forward.Normalize();

        //get input and move the player
        if (Input.GetKey(KeyCode.W))    Player.transform.position += forward*velocity;
        if (Input.GetKey(KeyCode.S))    Player.transform.position -= forward*velocity;
        if (Input.GetKey(KeyCode.A))    Player.transform.position -= rotate90*forward * velocity;
        if (Input.GetKey(KeyCode.D))    Player.transform.position += rotate90*forward * velocity;

        //shoot a bullet if player left click
        if (Input.GetMouseButton(0)&&!Bullet.activeSelf) shoot();

        //if not jump
        if (!jump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("jump");
                Player.transform.position += UpVector/10;
                Player.GetComponent<Rigidbody>().AddForce(UpVector * jumpforce, ForceMode.Impulse);

                jump = true;
            }
        }
        else
        {
            if (Player.transform.position.y<=1)
            {
                jump = false;
            }
        }


        //check if bullet is out of range
        if (Bullet.transform.localPosition.magnitude>MaxBulletRange) Bullet.SetActive(false);

        //check if player fall in a hole
        if (Player.transform.position.y < -10) Init();
    }

    void shoot()
    {
        Bullet.SetActive(true);
        Bullet.GetComponent<Rigidbody>().velocity = ZeroVector;
        Bullet.GetComponent<Rigidbody>().AddForce(Player.transform.forward * BulletSpeed, ForceMode.Impulse);
        Bullet.transform.localPosition = ZeroVector;
    }




}
