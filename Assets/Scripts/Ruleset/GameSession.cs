using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSession
{
    public Dictionary<Players, PlayerStats> Players = new Dictionary<Players, PlayerStats>();

    public PlayerStats GetPlayer(Players p)
    {
        return Players.ContainsKey(p) ? Players[p] : null;
    }
}
