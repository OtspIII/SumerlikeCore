using UnityEngine;

[System.Serializable]
public class GameEvent
{
    public GEvents Type;

    public GameEvent(GEvents type) 
    {
        Type = type;
    }
}

public enum GEvents
{
    //Menuish
    None=0,
    StartGame=001,
    //ZoneActions
    GetCoins=101,
    GetCups=102,
    GetSwords=103,
    GetWands=104,
}