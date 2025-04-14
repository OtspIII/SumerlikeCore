using System;
using TMPro;
using UnityEngine;

public class PlayerStatsheet : MonoBehaviour
{
    public TextMeshPro Text;
    public PlayerC Color;
    public PlayerState Who;
    public SpriteRenderer BG;

    public void Start()
    {
        God.GM.SheetDict.Add(Color,this);
    }

    private void Update()
    {
        if(Who != null && Who.Who != PlayerC.None)
            Imprint();
    }

    public virtual void Setup(PlayerState s)
    {
        Who = s;
        BG.color = God.Library.GetPlayer(Color).C;
        Imprint();
    }
    
    public virtual void Imprint()
    {
        string txt = "";
        foreach (GResources res in Who.Resources.Keys)
        {
            int amt = Who.Resources[res];
            if (txt != "") txt += "\n";
            txt += res + ": " + amt;
        }
        Text.text = txt;
    }
    
}
