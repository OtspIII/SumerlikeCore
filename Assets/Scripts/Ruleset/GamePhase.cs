using System.Collections;
using UnityEngine;

public class GamePhase
{
    public GamePhases Type;
    public GameBoards Board;
    public GameSession Session;
    public float TurnTime;
    public float TimeLeft;
    public int TotalTurns;
    public int TurnsLeft;
    public bool Complete = false;

    public void Start()
    {
        Session = God.Session;
        Setup();
    }

    public virtual void Setup()
    {
        TimeLeft = TurnTime;
        TurnsLeft = TotalTurns;
    }

    public virtual IEnumerator Intro()
    {
        yield return null;
    }
    
    public virtual IEnumerator EndPhase()
    {
        yield return null;
    }

    public virtual IEnumerator EndTurn()
    {
        yield return null;
    }


    public virtual void Run()
    {
        if (TurnTime > 0) Timer();
    }

    public virtual void Timer()
    {
        TimeLeft -= Time.deltaTime;
        God.Board.DisplayTime(TimeLeft, TurnsLeft);
        if (TimeLeft <= 0)
        {
            if(TotalTurns != -1)
                EndTurn();
            else
                Complete = true;
        }
        
    }

    public virtual void HandleEvent(PlayerStats pc, GameEvent e)
    {
        //Can override normal rules if desired
        Session.HandleEvent(pc,e);
    }

    public bool CheckForComplete()
    {
        return Complete;
    }
    
    public IEnumerator Perform()
    {
        Start();
        yield return God.GM.StartCoroutine(Intro());
        yield return God.GM.StartCoroutine(Script());
        while (!CheckForComplete())
        {
            yield return null;
        }
        yield return God.GM.StartCoroutine(EndPhase());
    }

    public virtual IEnumerator Script()
    {
        yield return null;
    }
}
