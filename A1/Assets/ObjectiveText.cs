using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveText : MonoBehaviour
{
    
    void FixedUpdate()
    {
        string txt = $"Teapot count : {Game.ObjectivesHit}/{Game.TotalObjectives}";
        txt += $"\nKeys : {Game.keyObtained}/1";
        GetComponent<Text>().text = txt;   
    }
}
