using System.Collections.Generic;

public static class God
{
    public static GameSession Session;
    public static LibraryManager Library;
    public static GameManager GM;
    
    public static GamePhase Phase { get { return Session.CurrentPhase; } }
    public static GameBoard Board { get { return GM.CurrentBoard; } }
    
    public static PlayerStats GetPlayer(PlayerC p)
    {
        return Session.Players.TryGetValue(p, out PlayerStats r) ? r : null;
    }

    public static GameEvent E(GEvents e)
    {
        return new GameEvent(e);
    }

    public static void HandleEvent(PlayerStats pc, GameEvent e,TokenController t=null)
    {
        if(Phase != null) Phase.HandleEvent(pc,e,t);
        else Session.HandleEvent(pc,e,t);
    }

}