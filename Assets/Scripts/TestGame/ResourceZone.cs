using System.Collections;
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

    public override IEnumerator TurnEnd()
    {
        int split = Inside.Count + Tokens.Count;
        if (split > 0)
        {
            int amt = Amount / split;
            foreach (PlayerState pc in Inside.ToArray())
            {
                for(int n=0;n<amt;n++)
                {
                    // pc.ChangeResource(Resource, amt);
                    pc.PC.SpawnIcon(Resource);
                    Amount--;
                    yield return God.Wait(0.2f, 0.5f, 0.1f);
                }
                
            }

            foreach (TokenState t in Tokens.ToArray())
            {
                for(int n=0;n<amt;n++)
                {
                    t.Token.SpawnIcon(Resource);
                    // t.Owner.ChangeResource(Resource, amt);
                    Amount--;
                    yield return God.Wait(0.2f, 0.5f, 0.1f);
                }
                RemoveToken(t);
                
            }
        }
        
    }

    public override IEnumerator TurnEndLate()
    {
        Debug.Log("TEL");
        Amount += TurnIncrease();
        UpdateDesc();
        Desc.color = Color.red;
        yield return God.Wait(0.2f, 0.5f, 0.1f);
        Desc.color = Color.black;
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
