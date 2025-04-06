using System.Collections;
using UnityEngine;

public class PlacePhase : GamePhase
{
    public PlacePhase()
    {
        Type = GPhases.Place;
        TotalTurns = 3;
        TurnTime = 10;
    }

    public override IEnumerator EndTurn()
    {
        foreach (ZoneController z in God.Board.Zones)
        {
            z.TurnEnd();
        }
        yield return C(God.Board.DisplayText("Turn " + (TotalTurns-TurnsLeft) + " Complete"));
    }
}
