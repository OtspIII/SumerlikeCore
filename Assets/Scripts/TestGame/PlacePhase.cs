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

    public override void Run()
    {
        base.Run();
        if (Input.GetKeyDown(KeyCode.Space)) TimeLeft = 0.1f;
    }

    public override IEnumerator EndTurn()
    {
        SetAct(false);
        foreach (ZoneController z in God.Board.Zones)
        {
            yield return God.C(z.TurnEnd());
            // yield return God.Wait(0.5f, 1f, 0.1f);
        }
        foreach (ZoneController z in God.Board.Zones)
        {
            yield return God.C(z.TurnEndLate());
        }
        yield return C(God.Board.DisplayText("Turn " + (TotalTurns-TurnsLeft) + " Complete"));
        SetAct(true);
    }
}
