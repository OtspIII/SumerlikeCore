using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerController : ThingController
{
    [Header("Stats")] public ControllerType Controls = ControllerType.AI;

    [FormerlySerializedAs("Stats")] [Header("Stuff")] 
    public PlayerState State;
    public Rigidbody2D RB;
    public SpriteRenderer SR;
    public ZoneController Zone;
    public float Speed;
    public PlayerInput PI;
    public Vector2 Movement;
    public List<PActions> Pressed;
    public List<PActions> JustPressed;
    public List<PActions> JustReleased;
    public List<TokenController> Tokens = new List<TokenController>();
    public List<TokenController> Followers = new List<TokenController>();
    public LocStates LState = LocStates.Active;

    public ZoneController AITarget;
    
    void Start()
    {
        // AIPickTarget();
        // Stats.TurnStart();
    }

    void Update()
    {
        if (LState != LocStates.Active) return;
        if (God.Phase != null && !God.Phase.CanAct) return;
        if(Controls != ControllerType.Gamepad)
            Movement = Vector2.zero;
        if(Controls == ControllerType.AI) AIControls();
        RB.linearVelocity = Movement;
        if (Pressed.Contains(PActions.A))
        {
            if(JustPressed.Contains(PActions.A))
                UseAStart();
            UseA();
        }
        else if (JustPressed.Contains(PActions.A))
        {
            UseAEnd();
        }
        if (Pressed.Contains(PActions.B))
        {
            if(JustPressed.Contains(PActions.B))
                UseBStart();
            UseB();
        }
        else if (JustPressed.Contains(PActions.B))
        {
            UseBEnd();
        }
        JustPressed.Clear();
    }

    public virtual void StartPregame()
    {
        
    }

    public virtual void StartGameplay()
    {
        for (int n = 0; n < GameSettings.StartingTokens; n++)
        {
            God.Library.SpawnToken(State);
        }
    }
    
    void UseA()
    {
        if (Zone != null)
        {
            Zone.OnUse(State);
        }
    }

    void UseAStart()
    {
        if (Zone != null)
        {
            Zone.OnUseStart(State);
        }
    }
    
    void UseAEnd()
    {
        if (Zone != null)
        {
            Zone.OnUseEnd(State);
        }
    }
    
    void UseB(bool held=true)
    {
        if (Zone != null)
        {
            Zone.OnAlt(State);
        }
    }
    
    void UseBStart()
    {
        if (Zone != null)
        {
            Zone.OnAltStart(State);
        }
    }
    
    void UseBEnd()
    {
        if (Zone != null)
        {
            Zone.OnAltEnd(State);
        }
    }

    public void AIControls()
    {
        if (AITarget == null)
        {
           AIPickTarget();
           if (AITarget == null) return;
        }

        if (Zone != AITarget)
        {
            Movement = ((Vector2)AITarget.transform.position - (Vector2)transform.position).normalized * Speed;
        }
        else
        {
            if (Followers.Count > 0)
            {
                AITarget.OnUseStart(State);
                AITarget.OnUse(State);
                AITarget.OnUseEnd(State);
                AITarget = null;
            }
        }
    }


    public void Setup(PlayerState s)
    {
        State = s;
        Debug.Log(s + " / " + State.Who);
        PColor p = God.Library.GetPlayer(State.Who);
        SR.color = p.C;
        State.PC = this;
        gameObject.name = State.Name;
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

    public void AddToken(TokenController t)
    {
        if(!Tokens.Contains(t)) Tokens.Add(t);
        if (!Followers.Contains(t))
        {
            Followers.Add(t);
            t.SetTarget(Followers.IndexOf(t));
        }
    }

    public override LocInfo MakeLocInfo()
    {
        return new LocInfo(transform.position,LState);
    }

    public virtual void AIPickTarget()
    {
        List<ZoneController> opts = God.Board.Zones;
        AITarget = opts.Random();
    }

    public void SpawnIcon(GResources res, int amt = 1)
    {
        IconController i = God.Library.SpawnIcon(transform.position, res, amt);
        i.Send(State);
        State.ChangeResource(res, amt);
    }

    public void Audit()
    {
        Movement = Vector2.zero;
        RB.linearVelocity = Movement;
    }

}

