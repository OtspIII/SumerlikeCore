using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePhase : GamePhase
{
    public List<GResources> Resources = new List<GResources>()
        { GResources.Cups, GResources.Coins, GResources.Swords, GResources.Wands };
    
    public ScorePhase()
    {
        Type = GPhases.Score;
    }

    public override IEnumerator Script()
    {
        yield return C(God.GM.CurrentBoard.DisplayText("SCORING"));
        yield return God.Wait(0.2f,0.5f,0f);
        foreach (GResources res in Resources)
        {
            List<PlayerState> win = God.GetWinner(res);
            string txt = "";
            foreach (PlayerState ps in win)
            {
                if (txt != "") txt += " / ";
                txt += ps.Who;
                ps.ChangeResource(GResources.Points, 1, true);
            }
            txt += " WINS " + res;
            foreach (PlayerState ps in God.Session.Players.Values) ps.SetResource(res, 0,true);
            yield return C(God.GM.CurrentBoard.DisplayText(txt));
        }
    }
}
