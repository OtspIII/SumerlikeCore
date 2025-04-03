using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PregameManager : MonoBehaviour
{
    public List<PlayerStats> Players;
    
    void Start()
    {
        God.Session = new GameSession();
        foreach (InputDevice id in InputSystem.devices)
        {
            InputSystem.EnableDevice(id);
        }
    }
    
    void OnPlayerJoined(PlayerInput pi)
    {
        PlayerController p = pi.gameObject.GetComponent<PlayerController>();
        p.SetupControls();
        PlayerC color = God.Session.NextPlayer();
        PlayerStats s = new PlayerStats(color);
        p.Setup(s);
        DontDestroyOnLoad(p.gameObject);
    }
}
