using System.Collections.Generic;

public static class God
{
    public static GameSession Session;
    public static LibraryManager Library;
    public static GameManager GM;
    
    public static GamePhase Phase { get { return Session.CurrentPhase; } }
    
    public static PlayerStats GetPlayer(PlayerC p)
    {
        return Session.Players.ContainsKey(p) ? Session.Players[p] : null;
    }

    public static GameEvent E(GEvents e)
    {
        return new GameEvent(e);
    }

    public static void HandleEvent(PlayerStats pc, GameEvent e)
    {
        if(Phase != null) Phase.HandleEvent(pc,e);
        else Session.HandleEvent(pc,e);
    }

}