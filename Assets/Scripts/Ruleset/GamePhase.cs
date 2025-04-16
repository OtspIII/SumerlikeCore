using System.Collections;
using UnityEngine;

public class GamePhase
{
    public GPhases Type;
    public GBoards Board;
    public GameSession Session;
    public float TurnTime;
    public float TimeLeft;
    public int TotalTurns;
    public int TurnsLeft;
    public bool Complete = false;
    public Coroutine Paused;
    public bool CanAct = false;

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
        //yield return C(God.Board.DisplayText(Type + " Intro"));
    }
    
    public virtual IEnumerator EndPhase()
    {
        foreach (ZoneController z in God.Board.Zones)
        {
            z.PhaseEnd(Type);
        }
        yield return null;
        //yield return C(God.Board.DisplayText(Type + " End"));
    }

    public virtual IEnumerator EndTurn()
    {
        foreach (ZoneController z in God.Board.Zones)
        {
            yield return God.C(z.TurnEnd());
        }
        foreach (ZoneController z in God.Board.Zones)
        {
            yield return God.C(z.TurnEndLate());
        }
        yield return C(God.Board.DisplayText("Turn " + (TotalTurns-TurnsLeft) + " Complete"));
    }

    public virtual void Run()
    {
        if (TurnTime > 0) Timer();
    }

    public virtual void Timer()
    {
        if (Paused != null) return;
        TimeLeft -= Time.deltaTime;
        God.Board.DisplayTime(TimeLeft, TurnsLeft);
        if (TimeLeft <= 0)
        {
            TimeLeft = TurnTime;
            if (TotalTurns != -1)
            {
                TurnsLeft--;
                if (TurnsLeft <= 0) Complete = true;
                C(EndTurn());
            }
            else
                Complete = true;
        }
        
    }

    public virtual void HandleEvent(PlayerState pc, GameEvent e,TokenState t=null)
    {
        //Can override normal rules if desired
        Session.HandleEvent(pc,e);
    }

    public bool CheckForComplete()
    {
        if (Paused != null) return false;
        return TurnTime <= 0 || Complete;
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
        God.Session.NextPhase();
    }

    public virtual IEnumerator Script()
    {
        yield return null;
    }

    public Coroutine C(IEnumerator i, bool pause=true)
    {
        if (pause)
        {
            return God.GM.StartCoroutine(CPause(i));
        }
        return God.GM.StartCoroutine(i);
    }

    public IEnumerator CPause(IEnumerator i)
    {
        God.Board.UndisplayTime();
        Coroutine p = God.GM.StartCoroutine(i);
        Paused = p;
        yield return Paused;
        if(Paused == p)
            Paused = null;
    }

    public void SetAct(bool canAct)
    {
        CanAct = canAct;
        foreach (PlayerController pc in God.GM.Players)
        {
            pc.Movement = Vector2.zero;
        }
    }
}
