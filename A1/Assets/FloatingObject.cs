using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    const float period = 100f;
    int frames = 0;
    void Start()
    {
        frames = Random.Range(0, 360 * (int)period);
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(++frames / period) / 2 + 2, transform.position.z);
        if (frames > 360 * period)
        {
            frames -= 360 * (int)period;
        }
    }
}
