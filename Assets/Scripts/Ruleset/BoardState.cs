using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardState : Thing
{
    public GameBoard Board;
    public List<ZoneState> Zones { get { return GetZones(); } }
    public List<int> ZoneIDs = new List<int>();
    public GBoards Type;

    public BoardState(GameBoard gb)
    {
        Board = gb;
        Type = gb.Type;
        if(God.Session != null)
            God.Session.AddBoard(this);
        // foreach (ZoneController z in gb.Zones)
        // {
        //     ZoneState zs = new ZoneState(z, this);
        //     z.State = zs;
        //     Zones.Add(zs);
        // }
    }

    public void AddZone(ZoneState z)
    {
        ZoneIDs.Add(z.ID);
        z.Setup(this);
    }
    
    public List<ZoneState> GetZones()
    {
        List<ZoneState> r = new List<ZoneState>();
        foreach(int id in ZoneIDs)
            r.Add(God.Session.GetZone(id));
        return r;
    }
}

[System.Serializable]
public class ZoneState : Thing
{
    public ZoneController Zone;
    public BoardState Board { get { return God.Session.GetBoard(BoardID); } set { BoardID = value.ID; } }
    public int BoardID;
    public int MaxTokens = 3;
    public List<TokenState> Tokens { get { return GetTokens(); } }
    public List<int> TokenIDs = new List<int>();
    public List<PlayerState> Players { get { return GetPlayers(); } }
    public List<PlayerC> PlayerIDs = new List<PlayerC>();
    
    public ZoneState(ZoneController zc)
    {
        Zone = zc;
        if(God.Session != null)
            God.Session.AddZone(this);
    }

    public void Setup(BoardState b)
    {
        Board = b;
    }
    
    public void AddToken(TokenState z)
    {
        TokenIDs.Add(z.ID);
    }
    
    public void RemoveToken(TokenState z)
    {
        TokenIDs.Remove(z.ID);
    }
    
    public List<TokenState> GetTokens()
    {
        List<TokenState> r = new List<TokenState>();
        foreach(int id in TokenIDs)
            r.Add(God.Session.GetToken(id));
        return r;
    }
    
    public void AddPlayer(PlayerState z)
    {
        PlayerIDs.Add(z.Who);
    }
    
    public void RemovePlayer(PlayerState z)
    {
        PlayerIDs.Remove(z.Who);
    }
    
    public List<PlayerState> GetPlayers()
    {
        List<PlayerState> r = new List<PlayerState>();
        foreach(PlayerC id in PlayerIDs)
            r.Add(God.Session.GetPlayer(id));
        return r;
    }
}


[System.Serializable]
public class TokenState : Thing
{
    public TokenController Token;
    public PlayerState Owner { get { return God.Session.GetPlayer(OwnerC); } set { OwnerC = value.Who; } }
    public PlayerC OwnerC;
    public ZoneState Zone{ get { return God.Session.GetZone(ZoneID); } set { ZoneID = value != null ? value.ID : -1; } }
    public int ZoneID;
    
    public TokenState(TokenController tc, PlayerController pc)
    {
        Token = tc;
        Owner = pc.State;
        God.Session.AddToken(this);
    }
}
