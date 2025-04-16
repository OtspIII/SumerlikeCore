using System;
using TMPro;
using UnityEngine;

public class IconController : MonoBehaviour
{
    public SpriteRenderer SR;
    public TextMeshPro Text;
    public GResources Type;
    public int Amount;
    public IconAction Action = IconAction.Idle;
    public PlayerC Who;
    public Vector3 Target;
    public Vector3 StartPoint;
    public float Progress;

    
    void Update()
    {
        if (Action != IconAction.Flying) return;
        Progress += Time.deltaTime / GameSettings.IconFlyTime;
        float p = Imp.Ease(Progress, Eases.InOut);
        transform.position = Vector3.Lerp(StartPoint, Target, p);
        if (Progress >= 1)
        {
            God.GetPlayer(Who).Sheet.TakeIcon(this);
            Action = IconAction.Spent;
            Destroy(gameObject);
        }
    }
    
    public void Setup(int amt)
    {
        Amount = amt;
        Imprint();
    }
    
    public void Setup(GResources res,int amt)
    {
        Type = res;
        Amount = amt;
        Imprint();
    }

    private void OnDestroy()
    {
        
        God.GM.AllIcons.Remove(this);
    }

    public void Imprint()
    {
        ResourceArt res = God.Library.GetResource(Type);
        if (Text != null) Text.text = ""+Amount;
        if (SR.sprite == null)
        {
            SR.sprite = res.S;
        }
    }

    public void Send(PlayerState who)
    {
        God.GM.AllIcons.Add(this);
        Who = who.Who;
        Action = IconAction.Flying;
        Progress = 0;
        StartPoint = transform.position;
        Target = who.Sheet.transform.position;
    }
}

public enum IconAction
{
    None=0,
    Idle=1,
    Flying=2,
    Spent=3,
}