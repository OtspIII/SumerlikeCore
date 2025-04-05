using System.Collections;
using UnityEngine;

public class EndgamePhase : GamePhase
{
    public EndgamePhase()
    {
        Type = GPhases.Endgame;
    }

    public override IEnumerator Script()
    {
        yield return C(God.GM.CurrentBoard.DisplayText("GOOD JOB"));
    }
}
