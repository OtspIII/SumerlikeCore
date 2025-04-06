using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameSession
{
    public List<PlayerC> ColorOptions = new List<PlayerC>()
        { PlayerC.Blue, PlayerC.Green, PlayerC.Red, PlayerC.Yellow };
    public Dictionary<PlayerC, PlayerStats> Players = new Dictionary<PlayerC, PlayerStats>();

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

    public PlayerStats GetPlayer(PlayerC p)
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

    public virtual void HandleEvent(PlayerStats pc, GameEvent e,TokenController t=null)
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
}
