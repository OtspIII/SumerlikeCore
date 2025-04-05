using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PregameManager : MonoBehaviour
{
    public PlayerInputManager PIM;
    public List<PlayerStats> Players;

    void Awake()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
        //PlayerInputManager.instance.onPlayerLeft += OnPlayerLeft;
    }
    
    void Start()
    {
        God.Session = new GameSession();
        foreach (InputDevice id in InputSystem.devices)
        {
            InputSystem.EnableDevice(id);
        }
    }
    
    public void OnPlayerJoined(PlayerInput pi)
    {
        PlayerController p = pi.gameObject.GetComponent<PlayerController>();
        p.SetupControls();
        PlayerC color = God.Session.NextPlayer();
        PlayerStats s = new PlayerStats(color);
        p.Setup(s);
        God.Session.Players.Add(color,s);
        DontDestroyOnLoad(p.gameObject);
    }
}
