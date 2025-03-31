using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    // public float TurnTimer;
    // public int TurnsLeft;
    //
    // public TextMeshPro TimeDisplay;
    public List<ZoneController> Zones;
    // public TextMeshPro CountdownTxt;
    // public float Countdown;

    private void Start()
    {
        // TurnTimer = GameSettings.TurnTime;
        // TurnsLeft = GameSettings.TotalTurns;
        //
        // Countdown = GameSettings.StartTime;
        // Time.timeScale = 0;
        // CountdownTxt.gameObject.SetActive(true);
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
