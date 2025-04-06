using System.Collections;
using UnityEngine;

public class IntroPhase : GamePhase
{
    public IntroPhase()
    {
        Type = GPhases.Intro;
        Board = GBoards.Main;
    }

    public override IEnumerator Script()
    {
        yield return C(God.Board.DisplayText("Game Start"));
    }
}
