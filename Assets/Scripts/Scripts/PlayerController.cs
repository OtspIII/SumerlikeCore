using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")] public ControllerType Controls = ControllerType.AI;

    [Header("Stuff")] 
    public Players Color;
    public PlayerStats Stats;
    public Rigidbody2D RB;
    public SpriteRenderer SR;
    public ZoneController Zone;
    public float Speed;
    public PlayerInput PI;
    public Vector2 Movement;
    public List<PActions> Pressed;
    public List<PActions> JustPressed;
    public List<PActions> JustReleased;
    
    void Start()
    {
        Setup(Color);
        // AIPickTarget();
        Stats.TurnStart();
    }

    void Update()
    {
        if(Controls != ControllerType.Gamepad)
            Movement = Vector2.zero;
        if(Controls == ControllerType.AI) AIControls();
        RB.linearVelocity = Movement;
        if(Pressed.Contains(PActions.A)) UseA(JustPressed.Contains(PActions.A));
        if(Pressed.Contains(PActions.B)) UseB(JustPressed.Contains(PActions.B));
        JustPressed.Clear();
    }


    void UseA(bool held=true)
    {
        if (Zone != null)
        {
            Zone.UseA(Stats,held);
        }
    }
    
    void UseB(bool held=true)
    {
        if (Zone != null)
        {
            Zone.UseB(Stats,held);
        }
    }

    public void AIControls()
    {
        
    }


    public void Setup(Players c)
    {
        Color = c;
        PColor p = God.Library.GetPlayer(c);
        SR.color = p.C;
        Stats = new PlayerStats(c);
        Stats.PC = this;
        gameObject.name = Stats.Name;
        God.Session.Players.Add(c,Stats);
        Speed = GameSettings.WalkSpeed;
    }

    public bool ButtonDown(PActions a)
    {
        return JustPressed.Contains(a);
    }
    public bool ButtonUp(PActions a)
    {
        return JustReleased.Contains(a);
    }
    
    public bool Button(PActions a)
    {
        return Pressed.Contains(a);
    }


    public void SetupControls()
    {
        if (PI.devices.Count == 0)
        {
            return;
        }
        
        Debug.Log("CONTROLS SETUP: " + gameObject.name);
    }
    
    void OnMove(InputValue movementValue)
    {
        Controls = ControllerType.Gamepad;
        Vector2 m = movementValue.Get<Vector2>();
        Movement = m * Speed;
    }

    void OnUseA(InputValue iv)
    {
        Press(PActions.A,iv);
    }
    
    void OnUseB(InputValue iv)
    {
        Press(PActions.B,iv);
    }
    
    void OnJoin()
    {
        if (Controls != ControllerType.AI && Controls != ControllerType.Gamepad)
        {
            Debug.Log(gameObject.name + " TRIED TO JOIN, BUT ALREADY CLAIMED BY" + Controls);
            return;
        }
        Join();
        
        Controls = ControllerType.Gamepad;
    }

    public void Join()
    {
        // if (Controls != ControllerType.AI && God.GM.CurrentBoard.Countdown > 0)
        // {
        //     God.GM.CurrentBoard.Countdown = 0.1f;
        //     return;
        // }
    }

    public void Press(PActions a, InputValue iv)
    {
        if(iv.isPressed)
            Press(a);
        else
            Release(a);
    }
    
    public void Press(PActions a)
    {
        Pressed.Add(a);
        JustPressed.Add(a);
    }

    public void Release(PActions a)
    {
        Pressed.Remove(a);
    }

}

[System.Serializable]
public class PlayerStats
{
    public Players Who;
    public string Name;
    public PlayerController PC;

    public PlayerStats(Players w)
    {
        Who = w;
        Name = Who.ToString();
        Setup();
    }

    public void Setup()
    {
        
    }

    public void TurnEnd()
    {
       
    }

    public void TurnStart()
    {
        
    }
}


public enum Players
{
    None=0,
    Blue=1,
    Red=2,
    Yellow=3,
    Green=4
}

public enum PActions
{
    None=0,
    Up=1,
    Right=2,
    Down=3,
    Left=4,
    A=5,
    B=6,
    Join=7
}

public enum GResources
{
    None=0,
    Coins=1,
    Materials=2,
}

public enum ControllerType
{
    None=0,
    AI=1,
    Remote=2,
    Gamepad=3,
}