using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public readonly Vector3 jumpVector = new Vector3(0,8,0);
    public RaycastHit rayhit = new RaycastHit();
    public const float Hsensitivity = 10;
    public const float bulletForce = 40;
    public const float velocity = 0.1f;

    public float radius = 1f;
    public bool OnGround = true;

    GameObject bullet;

    public void Start()
    {
        GetComponent<Rigidbody>().maxAngularVelocity = 0;
        bullet = GameObject.Find("Bullet");
        bullet.GetComponent<Rigidbody>().maxAngularVelocity = 0;
        bullet.SetActive(false);
    }
    // Start is called before the first frame update
    public void FixedUpdate()
    {
        transform.Rotate(Game.UpVector, Input.GetAxis("Mouse X")*Hsensitivity);
        if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * velocity;
        if (Input.GetKey(KeyCode.S)) transform.position -= transform.forward * velocity;
        if (Input.GetKey(KeyCode.A)) transform.position -= Game.Agnle90 * transform.forward * velocity;
        if (Input.GetKey(KeyCode.D)) transform.position += Game.Agnle90 * transform.forward * velocity;
        if (Input.GetKey(KeyCode.Space)&&OnGround)
        {
            OnGround = false;
            GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.Impulse);
        }
        if (Input.GetMouseButton(0)&&!bullet.activeSelf)
        {
            bullet.SetActive(true);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Ground"))
        {
            OnGround = true;
        }
    }
}
