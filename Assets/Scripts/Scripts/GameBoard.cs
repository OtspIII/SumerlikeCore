using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GBoards Type;
    public TextMeshPro TimeDisplay;
    public Transform CamPosition;
    public List<ZoneController> Zones;
    public TextMeshPro MainTxt;
    // public float Countdown;

    private void Start()
    {
        if(God.GM != null)
            God.GM.BoardDict.Add(Type,this);
    }

    public virtual void Setup(GamePhase ph)
    {
        
    }

    public void DisplayTime(float timer, int rounds=-1)
    {
        TimeDisplay.text = "" + (rounds != -1 ? rounds + " : " : "" ) + Mathf.Ceil(timer);
    }
    public void UndisplayTime()
    {
        TimeDisplay.text = "";
    }

    public virtual IEnumerator DisplayText(string txt, float time = 1)
    {
        MainTxt.text = txt;
        yield return new WaitForSeconds(time);
        MainTxt.text = "";
    }
}
