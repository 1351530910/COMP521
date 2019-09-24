using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeapotCount : MonoBehaviour
{
    void FixedUpdate()
    {
        string txt = $"Teapot count : {Game.teapotCount}/{Game.nTeapots}";
        txt += $"\nKeys : {Game.keyObtained}/1";
        GetComponent<Text>().text = txt;   
    }
}
