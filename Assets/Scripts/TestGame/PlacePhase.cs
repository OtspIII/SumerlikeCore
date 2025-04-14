using System.Collections;
using UnityEngine;

public class PlacePhase : GamePhase
{
    public PlacePhase()
    {
        Type = GPhases.Place;
        TotalTurns = 3;
        TurnTime = 10;
        CanAct = true;
    }

    public override IEnumerator EndTurn()
    {
        CanAct = false;
        foreach (ZoneController z in God.Board.Zones)
        {
            z.TurnEnd();
        }
        yield return C(God.Board.DisplayText("Turn " + (TotalTurns-TurnsLeft) + " Complete"));
        CanAct = true;
    }
}
