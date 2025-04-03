using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameBoards Type;
    public TextMeshPro TimeDisplay;
    public Transform CamPosition;
    public List<ZoneController> Zones;
    // public TextMeshPro CountdownTxt;
    // public float Countdown;

    private void Start()
    {
        God.GM.BoardDict.Add(Type,this);
    }

    public virtual void Setup(GamePhase ph)
    {
        
    }

    public void DisplayTime(float timer, int rounds=-1)
    {
        TimeDisplay.text = "" + (rounds != -1 ? rounds + " : " : "" ) + Mathf.Ceil(timer);
    }
    
    void Update()
    {
        
        // TurnTimer -= Time.deltaTime;
        // TimeDisplay.text = "" + TurnsLeft + " : " + Mathf.Ceil(TurnTimer);
        // if(TurnTimer <= 0) TakeTurn();
        //
        // if (Countdown > 0)
        // {
        //     Countdown -= Time.unscaledDeltaTime;
        //     CountdownTxt.text = "GAME STARTS IN " + Mathf.Ceil(Countdown);
        //     if (Countdown < 0 || Input.GetKeyDown(KeyCode.Space))
        //     {
        //         Time.timeScale = 1;
        //         Countdown = 0;
        //         CountdownTxt.gameObject.SetActive(false);
        //     }
        // }
    }
    
    public void TakeTurn()
    {
        // TurnTimer = GameSettings.TurnTime;
        foreach (ZoneController z in Zones)
        {
            z.TurnEnd();
        }

        foreach (PlayerStats ps in God.Session.Players.Values)
        {
            ps.TurnEnd();
        }

        // TurnsLeft--;
        // if (TurnsLeft <= 0)
        // {
        //     God.GM.GameOver();
        // }
    }
}
