using TMPro;
using UnityEngine;

public class PlayerStatsheet : MonoBehaviour
{
    public TextMeshPro Text;
    public PlayerC Color;
    public PlayerStats Who;
    public SpriteRenderer BG;

    public void Start()
    {
        God.GM.SheetDict.Add(Color,this);
    }
    
    public virtual void Setup(PlayerStats s)
    {
        Who = s;
        BG.color = God.Library.GetPlayer(Color).C;
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
