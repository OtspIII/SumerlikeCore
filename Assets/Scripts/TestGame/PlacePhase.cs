using UnityEngine;

public class PlacePhase : GamePhase
{
    public PlacePhase()
    {
        Type = GPhases.Place;
        TotalTurns = 3;
        TurnTime = 10;
    }
}
