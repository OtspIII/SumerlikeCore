using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameSession
{
    public List<PlayerC> ColorOptions = new List<PlayerC>()
        { PlayerC.Blue, PlayerC.Green, PlayerC.Red, PlayerC.Yellow };
    public Dictionary<PlayerC, PlayerState> Players = new Dictionary<PlayerC, PlayerState>();

    public int MaxBoardID = 1;
    public Dictionary<int, BoardState> BoardLinks = new Dictionary<int, BoardState>();
    public int MaxZoneID = 1;
    public Dictionary<int, ZoneState> ZoneLinks = new Dictionary<int, ZoneState>();
    public int MaxTokenID = 1;
    public Dictionary<int, TokenState> TokenLinks = new Dictionary<int, TokenState>();
    
    public List<GamePhase> Phases;
    public GamePhase CurrentPhase;

    public GameSession()
    {
        Setup();
    }

    public virtual void Setup()
    {
        Phases = new List<GamePhase>()
        {
            new IntroPhase(),
            new PlacePhase(),new ScorePhase(),
            new PlacePhase(),new ScorePhase(),
            new PlacePhase(),new ScorePhase(),
            new EndgamePhase()
        };
    }

    public PlayerState GetPlayer(PlayerC p)
    {
        return Players.ContainsKey(p) ? Players[p] : null;
    }
    
    public PlayerC NextPlayer()
    {
        PlayerC chosen = ColorOptions.RandomE(true);
        if(chosen == PlayerC.None) Debug.Log("ERROR: OUT OF COLORS");
        return chosen;
    }

    public virtual void NextPhase()
    {
        if (Phases.Count == 0) SceneManager.LoadScene(0);
        GamePhase chosen = Phases[0];
        Phases.Remove(chosen);
        CurrentPhase = chosen;
        God.GM.StartPhase(chosen);
    }

    public virtual void HandleEvent(PlayerState pc, GameEvent e,TokenState t=null)
    {
        switch (e.Type)
        {
            case GEvents.StartGame:
            {
                SceneManager.LoadScene("Gameplay");
                break;
            }
            case GEvents.GetCoins:
            {
                pc.ChangeResource(GResources.Coins, 1);
                break;
            }
            case GEvents.GetCups:
            {
                pc.ChangeResource(GResources.Cups, 1);
                break;
            }
            case GEvents.GetSwords:
            {
                pc.ChangeResource(GResources.Swords, 1);
                break;
            }
            case GEvents.GetWands:
            {
                pc.ChangeResource(GResources.Wands, 1);
                break;
            }
        }
    }

    public int AddBoard(BoardState p)
    {
        p.ID = MaxBoardID;
        MaxBoardID++;
        BoardLinks.Add(p.ID,p);
        return p.ID;
    }
    
    public int AddZone(ZoneState p)
    {
        p.ID = MaxZoneID;
        MaxZoneID++;
        ZoneLinks.Add(p.ID,p);
        return p.ID;
    }
    
    public int AddToken(TokenState p)
    {
        p.ID = MaxTokenID;
        MaxTokenID++;
        TokenLinks.Add(p.ID,p);
        return p.ID;
    }

    public BoardState GetBoard(int id)
    {
        return BoardLinks.TryGetValue(id,out BoardState r) ? r : null;
    }
    
    public ZoneState GetZone(int id)
    {
        return ZoneLinks.TryGetValue(id,out ZoneState r) ? r : null;
    }
    
    public TokenState GetToken(int id)
    {
        return TokenLinks.TryGetValue(id,out TokenState r) ? r : null;
    }
}
