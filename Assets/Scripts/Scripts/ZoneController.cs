using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZoneController : MonoBehaviour
{
    public string Name;
    public Collider2D Coll;
    public TextMeshPro Title;
    public TextMeshPro Desc;
    public List<PlayerController> Inside;
    public SpriteRenderer BG;
    public GameEvent UseEffect;
    public GameEvent TokenEffect;
    public GameBoard Board;
    public List<TokenController> Tokens;
    public int MaxTokens = 1;
    
    void Start()
    {
        Setup();        
    }

    public virtual void Setup()
    {
        if(Title != null)
            Title.text = Name;
        if (Board == null) Board = gameObject.GetComponentInParent<GameBoard>();
        Board.Zones.Add(this);
    }

    public virtual void PlayerEnter(PlayerController pc)
    {
        if(pc.Zone != null) pc.Zone.PlayerExit(pc);
        pc.Zone = this;
        Inside.Add(pc);
    }
    
    
    public virtual void PlayerExit(PlayerController pc)
    {
        if (pc.Zone != this) return;
        pc.Zone = null;
        Inside.Remove(pc);

    }

    public virtual void OnUse(PlayerStats pc)
    {
        //Debug.Log("USED A: " + gameObject + " / " + pc);
    }
    
    public virtual void OnUseStart(PlayerStats pc)
    {
        God.HandleEvent(pc,UseEffect);
        if (pc.PC.Followers.Count > 0) TakeToken(pc.PC.Followers[0]);
    }
    
    public virtual void OnUseEnd(PlayerStats pc)
    {
        
    }
    
    public virtual void OnAlt(PlayerStats pc)
    {
        
    }
    public virtual void OnAltStart(PlayerStats pc)
    {
        
    }
    public virtual void OnAltEnd(PlayerStats pc)
    {
        
    }
    
    public virtual void TurnEnd()
    {
        foreach (PlayerController pc in Inside.ToArray())
        {
            TurnEndPlayer(pc.Stats);
        }
        foreach (TokenController t in Tokens.ToArray())
        {
            TurnEndToken(t);
        }
    }
    
    public virtual void TurnEndPlayer(PlayerStats pc)
    {
        
    }
    
    public virtual void TurnEndToken(TokenController t)
    {
        God.HandleEvent(t.OwnStats,TokenEffect,t);
        RemoveToken(t);
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

    public virtual bool ValidTarget(PlayerStats who)
    {
        return true;
    }

    public virtual bool TakeToken(TokenController t)
    {
        if (Tokens.Count >= MaxTokens) return false;
        t.SetTarget(transform,new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)));
        Tokens.Add(t);
        return true;
    }

    public virtual void RemoveToken(TokenController t)
    {
        if (!Tokens.Contains(t)) return;
        Tokens.Remove(t);
        t.SetTarget();
    }
}
