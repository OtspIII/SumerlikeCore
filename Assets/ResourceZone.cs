using System.Collections.Generic;
using UnityEngine;

public class ResourceZone : ZoneController
{
    public int Amount;
    public GResources Resource;

    public override void Setup()
    {
        Name = Resource.ToString();
        base.Setup();
        Amount = TurnIncrease();
        UpdateDesc();
    }

    public override void TurnEnd()
    {
        int split = Inside.Count + Tokens.Count;
        if (split > 0)
        {
            int amt = Amount / split;
            foreach (PlayerController pc in Inside.ToArray())
            {
                pc.Stats.ChangeResource(Resource, amt);
                Amount -= amt;
            }

            foreach (TokenController t in Tokens.ToArray())
            {
                t.OwnStats.ChangeResource(Resource, amt);
                RemoveToken(t);
                Amount -= amt;
            }
        }
        Amount += TurnIncrease();
        UpdateDesc();
    }

    public int TurnIncrease()
    {
        return 3;
    }

    public void UpdateDesc()
    {
        Desc.text = "x"+Amount;
    }
}
