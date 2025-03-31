using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class GameManager : MonoBehaviour
{
    public GameObject Mockup;
    public float TurnTimer;
    public int TurnsLeft;
    public TextMeshPro TimeDisplay;
    public List<ZoneController> Zones;
    public PlayerController Keyboarder;
    public TextMeshPro CountdownTxt;
    public float Countdown;

    void Awake()
    {
        God.GM = this;
    }

    void Start()
    {
        Mockup.SetActive(false);
        TurnTimer = GameSettings.TurnTime;
        TurnsLeft = GameSettings.TotalTurns;
        foreach (InputDevice id in InputSystem.devices)
        {
            InputSystem.EnableDevice(id);
        }

        Countdown = GameSettings.StartTime;
        Time.timeScale = 0;
        CountdownTxt.gameObject.SetActive(true);
    }

    
    void OnPlayerJoined(PlayerInput pi)
    {
        pi.gameObject.GetComponent<PlayerController>().SetupControls();
        
        Debug.Log("OPJ: " + pi.gameObject + " / " + pi.devices.Count);
        // if(pi.devices.Count > 0)
        //     Debug.Log("OPJ2: " + pi.devices[0]);
    }
    
    void Update()
    {
        TurnTimer -= Time.deltaTime;
        TimeDisplay.text = "" + TurnsLeft + " : " + Mathf.Ceil(TurnTimer);
        if(TurnTimer <= 0) TakeTurn();

        if (Countdown > 0)
        {
            Countdown -= Time.unscaledDeltaTime;
            CountdownTxt.text = "GAME STARTS IN " + Mathf.Ceil(Countdown);
            if (Countdown < 0 || Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                Countdown = 0;
                CountdownTxt.gameObject.SetActive(false);
            }
        }


        if ((Input.GetKeyDown(KeyCode.Return) && Keyboarder == null) )
        {
            Debug.Log("KEYBOARDER JOIN");
            int best = 0;
            PlayerController b = null;
            foreach (PlayerStats ps in God.Players.Values)
            {
                PlayerController pc = ps.PC;
                if (pc.Controls != ControllerType.AI) continue;
                if (pc.PI.devices.Count > 0)
                {
                    if (best >= 1)
                        continue;
                    best = 1;
                    b = pc;
                }
                else
                {
                    if (best >= 2) continue;
                    best = 2;
                    b = pc;
                }
            }

            if (b != null)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    b.Controls = ControllerType.Keyboard;
                    Keyboarder = b;                    
                }
                Debug.Log("KB: " + b.gameObject.name + " / " + b.Controls);
            }
        }
    }

    public void TakeTurn()
    {
        TurnTimer = GameSettings.TurnTime;
        foreach (ZoneController z in Zones)
        {
            z.TurnEnd();
        }

        foreach (PlayerStats ps in God.Players.Values)
        {
            ps.TurnEnd();
        }

        TurnsLeft--;
        if (TurnsLeft <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        
    }
}
