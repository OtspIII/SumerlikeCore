using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBoard : ThingController
{
    public GBoards Type;
    public TextMeshPro TimeDisplay;
    public Transform CamPosition;
    public List<ZoneController> Zones;
    public TextMeshPro MainTxt;
    public BoardState State;
    // public float Countdown;

    private void Awake()
    {
        State = new BoardState(this);
    }

    private void Start()
    {
        if(God.GM != null)
            God.GM.BoardDict.Add(Type,this);
    }

    public virtual void Setup()
    {
        State = new BoardState(this);
    }

    public void AddZone(ZoneController z)
    {
        Zones.Add(z);
        State.AddZone(z.State);
    }
    
    public virtual void PhaseSetup(GamePhase ph)
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

    public virtual IEnumerator DisplayText(string txt, float time = 1,bool waitForIcons=true)
    {
        MainTxt.text = txt;
        yield return new WaitForSeconds(time);
        float safety = 5;
        while (waitForIcons && safety > 0 && God.GM.AllIcons.Count > 0)
        {
            safety -= Time.deltaTime;
            yield return null;
        }
        MainTxt.text = "";
    }
}
