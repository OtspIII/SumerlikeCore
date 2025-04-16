using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgamePhase : GamePhase
{
    public EndgamePhase()
    {
        Type = GPhases.Endgame;
    }

    public override IEnumerator Script()
    {
        List<PlayerState> win = God.GetWinner(GResources.Points);
        string txt = "";
        foreach (PlayerState ps in win)
        {
            if (txt != "") txt += " / ";
            txt += ps.Who;
            ps.ChangeResource(GResources.Points, 1, true);
        }
        txt += " WINS";
        yield return C(God.GM.CurrentBoard.DisplayText(txt,60));
    }
}
