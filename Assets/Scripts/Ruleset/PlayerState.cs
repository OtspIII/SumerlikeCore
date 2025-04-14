using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Thing
{
    public int ID;
    public LocInfo Loc;
    // public ThingTypes Type;
    //
    // public GameLink GetLink()
    // {
    //     return new GameLink(Type, ID);
    // }
}

[System.Serializable]
public class PlayerState : Thing
{
    public PlayerC Who;
    public string Name;
    public PlayerController PC;
    public PlayerStatsheet Sheet
    {
        get { return God.GM.SheetDict[Who]; }
    }
    public Dictionary<GResources, int> Resources = new Dictionary<GResources, int>();

    public PlayerState(PlayerC w)
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


[System.Serializable]
public class LocInfo
{
    public Vector2 Position;
    public LocStates State;

    public LocInfo(Vector3 pos, LocStates st=LocStates.None)
    {
        Position = pos;
        State = st;
    }
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

public enum LocStates
{
    None=0,
    Active=1,
    LockedIn=2,
    Asleep=3,
    Cutscene=4,
}
