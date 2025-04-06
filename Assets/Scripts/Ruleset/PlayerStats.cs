using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public PlayerC Who;
    public string Name;
    public PlayerController PC;
    public PlayerStatsheet Sheet
    {
        get { return God.GM.SheetDict[Who]; }
    }
    public Dictionary<GResources, int> Resources = new Dictionary<GResources, int>();

    public PlayerStats(PlayerC w)
    {
        Who = w;
        Name = Who.ToString();
        Setup();
    }

    public void Setup()
    {
        
    }

    public int ChangeResource(GResources res, int amt)
    {
        if (!Resources.ContainsKey(res))
        {
            Resources.Add(res,amt);
            return amt;
        }
        Resources[res] += amt;
        Sheet.Imprint();
        return Resources[res];
    }

    // public void TurnEnd()
    // {
    //    
    // }
    //
    // public void TurnStart()
    // {
    //     
    // }
}


public enum PlayerC
{
    None=0,
    Blue=1,
    Red=2,
    Yellow=3,
    Green=4
}

public enum PActions
{
    None=0,
    Up=1,
    Right=2,
    Down=3,
    Left=4,
    A=5,
    B=6,
    Join=7
}

public enum ControllerType
{
    None=0,
    AI=1,
    Remote=2,
    Gamepad=3,
}
