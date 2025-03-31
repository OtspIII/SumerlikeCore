using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PregameManager : MonoBehaviour
{
    public List<PlayerStats> Players;
    
    void Start()
    {
        God.Session = new GameSession();
    }

    void Update()
    {
        
    }

    void OnPlayerJoined(PlayerInput pi)
    {
        PlayerController p = pi.gameObject.GetComponent<PlayerController>();
        p.SetupControls();
    }
}
