using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    public static Vector3 wind = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("resetwind", 2, 1);
    }
    public void resetwind()
    {
        //wind = new Vector3(Random.Range(-4.0f, 4.0f), 0, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += wind * 0.01f;

        //check if cloud moves out of the screen space
        if (transform.position.x > 6)
        {
            Debug.Log(transform.position);
            transform.position = new Vector3(-6, 2, 0);
        }
        if (transform.position.x < -6)
        {
            Debug.Log(transform.position);
            transform.position = new Vector3(6, 2, 0);
        }
    }
}
