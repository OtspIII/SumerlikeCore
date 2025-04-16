using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsheet : MonoBehaviour
{
    public TextMeshPro Text;
    public PlayerC Color;
    public PlayerState Who;
    public SpriteRenderer BG;
    public Dictionary<GResources, int> ShownResources = new Dictionary<GResources, int>();

    public void Start()
    {
        God.GM.SheetDict.Add(Color,this);
    }

    private void Update()
    {
        // if(Who != null && Who.Who != PlayerC.None)
        //     Imprint();
    }

    public virtual void Setup(PlayerState s)
    {
        Who = s;
        BG.color = God.Library.GetPlayer(Color).C;
        Sync();
    }
    
    public virtual void Imprint()
    {
        string txt = "";
        foreach (GResources res in Who.Resources.Keys)
        {
            ShownResources.TryGetValue(res, out int amt);
            if (txt != "") txt += "\n";
            txt += res + ": " + amt;
        }
        Text.text = txt;
    }

    public virtual void Sync()
    {
        foreach (GResources res in Who.Resources.Keys)
        {
            int amt = Who.Resources[res];
            ShownResources.TryAdd(res, amt);
            ShownResources[res] = amt;
        }
        Imprint();
    }

    public virtual void TakeIcon(IconController i)
    {
        ShownResources.TryGetValue(i.Type, out int amt);
        // Debug.Log("TAKE ICON: " + i.Amount + " / " + amt);
        amt += i.Amount;
        ShownResources.TryAdd(i.Type, amt);
        ShownResources[i.Type] = amt;
        Imprint();
    }
    
}
