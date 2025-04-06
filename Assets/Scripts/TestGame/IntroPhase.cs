using System.Collections;
using UnityEngine;

public class IntroPhase : GamePhase
{
    public IntroPhase()
    {
        Type = GPhases.Intro;
        Board = GBoards.Main;
    }

    public override void Setup()
    {
        base.Setup();
        foreach (PlayerStats p in God.Session.Players.Values)
        {
            p.ChangeResource(GResources.Points, 0);
            p.ChangeResource(GResources.Cups, 0);
            p.ChangeResource(GResources.Coins, 0);
            p.ChangeResource(GResources.Swords, 0);
            p.ChangeResource(GResources.Wands, 0);
            
        }
    }

    public override IEnumerator Script()
    {
        yield return C(God.Board.DisplayText("Game Start"));
    }
}
