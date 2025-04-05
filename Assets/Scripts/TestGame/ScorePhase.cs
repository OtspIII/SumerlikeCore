using System.Collections;
using UnityEngine;

public class ScorePhase : GamePhase
{
    public ScorePhase()
    {
        Type = GPhases.Score;
    }

    public override IEnumerator Script()
    {
        yield return C(God.GM.CurrentBoard.DisplayText("SCORING"));
    }
}
