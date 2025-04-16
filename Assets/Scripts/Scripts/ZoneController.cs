using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZoneController : ThingController
{
    public string Name;
    public Collider2D Coll;
    public TextMeshPro Title;
    public TextMeshPro Desc;
    public List<PlayerState> Inside { get{return State.GetPlayers();}}
    public SpriteRenderer BG;
    public GameEvent UseEffect;
    public GameEvent TokenEffect;
    public GameBoard Board;
    public List<TokenState> Tokens { get{return State.GetTokens();}}
    public int MaxTokens {get{return State.MaxTokens;}set{State.MaxTokens = value;} }
    public ZoneState State;
    
    void Start()
    {
        Setup();        
    }

    public virtual void Setup()
    {
        if(Title != null)
            Title.text = Name;
        if (Board == null) Board = gameObject.GetComponentInParent<GameBoard>();
        State = new ZoneState(this);
        Board.AddZone(this);
    }

    public virtual void PlayerEnter(PlayerController pc)
    {
        if(pc.Zone != null) pc.Zone.PlayerExit(pc);
        pc.Zone = this;
        State.AddPlayer(pc.State);
        // Debug.Log("ENTER: " + pc + " / " + this);
        // Inside.Add(pc);
    }
    
    
    public virtual void PlayerExit(PlayerController pc)
    {
        if (pc.Zone != this) return;
        pc.Zone = null;
        // Inside.Remove(pc);
        // Debug.Log("EXIT: " + pc + " / " + this);
        State.RemovePlayer(pc.State);

    }

    public virtual void OnUse(PlayerState pc)
    {
        //Debug.Log("USED A: " + gameObject + " / " + pc);
    }
    
    public virtual void OnUseStart(PlayerState pc)
    {
        God.HandleEvent(pc,UseEffect);
        if (pc.PC.Followers.Count > 0) TakeToken(pc.PC.Followers[0].State);
    }
    
    public virtual void OnUseEnd(PlayerState pc)
    {
        
    }
    
    public virtual void OnAlt(PlayerState pc)
    {
        
    }
    public virtual void OnAltStart(PlayerState pc)
    {
        foreach (TokenState t in Tokens)
        {
            if (t.Owner == pc)
            {
                RemoveToken(t);
                break;
            }
        }
    }
    public virtual void OnAltEnd(PlayerState pc)
    {
        
    }
    
    public virtual IEnumerator TurnEnd()
    {
        foreach (PlayerState pc in Inside.ToArray())
        {
            yield return God.C(TurnEndPlayer(pc));
        }
        foreach (TokenState t in Tokens.ToArray())
        {
            yield return God.C(TurnEndToken(t));
        }
    }
    
    public virtual IEnumerator TurnEndLate()
    {
        yield return null;
    }
    
    public virtual IEnumerator TurnEndPlayer(PlayerState pc)
    {
        yield return null;
    }
    
    public virtual IEnumerator TurnEndToken(TokenState t)
    {
        God.HandleEvent(t.Owner,TokenEffect,t);
        RemoveToken(t);
        yield return null;
    }

    public virtual void PhaseEnd(GPhases p)
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            
            PlayerEnter(pc);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc != null)
        {
            PlayerExit(pc);
        }
    }

    public virtual bool ValidTarget(PlayerState who)
    {
        return true;
    }

    public virtual bool TakeToken(TokenState t)
    {
        if (Tokens.Count >= MaxTokens) return false;
        t.Token.SetZone(this,transform,new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)));
        State.AddToken(t);
        // Tokens.Add(t);
        return true;
    }

    public virtual void RemoveToken(TokenState t)
    {
        if (!Tokens.Contains(t)) return;
        // Tokens.Remove(t);
        State.RemoveToken(t);
        t.Token.SetTarget();
    }
}
