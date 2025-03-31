using System.Collections.Generic;

public static class God
{
    public static LibraryManager Library;
    public static GameManager GM;
    public static Dictionary<Players, PlayerStats> Players = new Dictionary<Players, PlayerStats>();

    public static PlayerStats GetPlayer(Players p)
    {
        return Players.ContainsKey(p) ? Players[p] : null;
    }
}