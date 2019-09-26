using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class debugtex : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        GetComponent<Text>().text = Game.text;
    }
}
