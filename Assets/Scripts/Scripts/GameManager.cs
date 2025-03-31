using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class GameManager : MonoBehaviour
{
    public PlayerController Keyboarder;
    public GameBoard CurrentBoard;

    void Awake()
    {
        God.GM = this;
    }

    void Start()
    {
        foreach (InputDevice id in InputSystem.devices)
        {
            InputSystem.EnableDevice(id);
        }

        
    }

    
    void OnPlayerJoined(PlayerInput pi)
    {
        pi.gameObject.GetComponent<PlayerController>().SetupControls();
        
        Debug.Log("OPJ: " + pi.gameObject + " / " + pi.devices.Count);
        // if(pi.devices.Count > 0)
        //     Debug.Log("OPJ2: " + pi.devices[0]);
    }

    protected void CheckForNewPlayer()
    {
        // if ((Input.GetKeyDown(KeyCode.Return) && Keyboarder == null) )
        // {
        //     Debug.Log("KEYBOARDER JOIN");
        //     int best = 0;
        //     PlayerController b = null;
        //     foreach (PlayerStats ps in God.Players.Values)
        //     {
        //         PlayerController pc = ps.PC;
        //         if (pc.Controls != ControllerType.AI) continue;
        //         if (pc.PI.devices.Count > 0)
        //         {
        //             if (best >= 1)
        //                 continue;
        //             best = 1;
        //             b = pc;
        //         }
        //         else
        //         {
        //             if (best >= 2) continue;
        //             best = 2;
        //             b = pc;
        //         }
        //     }
        //
        //     if (b != null)
        //     {
        //         if (Input.GetKeyDown(KeyCode.Return))
        //         {
        //             b.Controls = ControllerType.Keyboard;
        //             Keyboarder = b;                    
        //         }
        //         Debug.Log("KB: " + b.gameObject.name + " / " + b.Controls);
        //     }
        // }
    }
    
    void Update()
    {
        CheckForNewPlayer();

        
    }

    

    public void GameOver()
    {
        
    }
}
