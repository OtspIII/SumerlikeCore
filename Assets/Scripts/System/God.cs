using System.Collections.Generic;

public static class God
{
    public static GameSession Session;
    public static LibraryManager Library;
    public static GameManager GM;
    
    public static PlayerStats GetPlayer(Players p)
    {
        return Session.Players.ContainsKey(p) ? Session.Players[p] : null;
    }

    public static GameEvent E(GEvents e)
    {
        return new GameEvent(e);
    }
}