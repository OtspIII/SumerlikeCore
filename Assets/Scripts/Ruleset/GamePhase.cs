using UnityEngine;

public class GamePhase
{
    public GameSession Session;

    public void Start(GameSession s)
    {
        Session = s;
    }

    public void Run()
    {
        
    }

    public virtual void HandleEvent(PlayerStats pc, GameEvent e)
    {
        //Can override normal rules if desired
        Session.HandleEvent(pc,e);
    }
}
