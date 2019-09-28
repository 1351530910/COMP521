using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveText : MonoBehaviour
{
    
    void FixedUpdate()
    {
        string txt = $"Teapot count : {Game.ObjectivesHit}/{Game.objectives.Count}\n";
        txt += $"Current time : {Game.time}\n";
        txt += $"Maze solved : {Game.solvedCount}\n";
        txt += $"Maze lost : {Game.lostCount}\n";
        txt += $"Maze reset : {Game.resetCount}\n";
        GetComponent<Text>().text = txt;   
    }
}
