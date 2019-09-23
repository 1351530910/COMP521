using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public readonly Vector3 jumpVector = new Vector3(0,2,0);
    
    public RaycastHit rayhit = new RaycastHit();
    public const float Hsensitivity = 10;
    public const float Vsensitivity = 7;
    public const float velocity = 0.1f;

    public float radius = 1f;
    public bool OnGround = false;

    // Start is called before the first frame update
    public void FixedUpdate()
    {
        Vector3 lookat = Quaternion.Euler(-Input.GetAxis("Mouse Y") * Hsensitivity, Input.GetAxis("Mouse X") * Vsensitivity, 0) * transform.forward;

        transform.LookAt(transform.position + lookat, Game.UpVector);
        lookat.y = 0;
        lookat.Normalize();
        GetComponent<Rigidbody>().maxAngularVelocity = 0;
        


        if (Input.GetKey(KeyCode.W)) transform.position += lookat * velocity;
        if (Input.GetKey(KeyCode.S)) transform.position -= lookat * velocity;
        if (Input.GetKey(KeyCode.A)) transform.position -= Game.Agnle90 * lookat * velocity;
        if (Input.GetKey(KeyCode.D)) transform.position += Game.Agnle90 * lookat * velocity;

        rayhit = new RaycastHit();
        if (Input.GetKey(KeyCode.Space)&& GetComponent<Collider>().Raycast(new Ray(transform.position, Game.DownVector), out rayhit, GetComponent<SphereCollider>().radius + radius)) 
            GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.Impulse);
        
        Debug.Log(GetComponent<Collider>().Raycast(new Ray(transform.position-new Vector3(0, GetComponent<SphereCollider>().radius, 0), transform.forward), out rayhit, 1000));
        Debug.Log(rayhit.distance);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnGround = collision.collider.gameObject.CompareTag("Ground");
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Ground")) OnGround = false;
    }
}
