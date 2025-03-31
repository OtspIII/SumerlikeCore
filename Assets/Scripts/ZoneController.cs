using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public string Name;
    public Collider2D Coll;
    public TextMeshPro Title;
    public TextMeshPro Desc;
    public List<PlayerController> Inside;
    public SpriteRenderer BG;

    
    void Start()
    {
        Setup();        
    }

    public virtual void Setup()
    {
        if(Title != null)
            Title.text = Name;
        God.GM.Zones.Add(this);
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

    public virtual void UseA(PlayerStats pc, bool down)
    {
        Debug.Log("USED A: " + gameObject + " / " + pc);
    }
    
    public virtual void UseB(PlayerStats pc, bool down)
    {
        Debug.Log("USED B: " + gameObject + " / " + pc);
        
    }
    
    public virtual void TurnEnd()
    {
        foreach (PlayerController pc in Inside)
        {
            TurnEndPlayer(pc.Stats);
        }
    }
    
    public virtual void TurnEndPlayer(PlayerStats pc)
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
}
