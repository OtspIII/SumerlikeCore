using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class God
{
    public static GameSession Session;
    public static LibraryManager Library;
    public static GameManager GM;
    
    public static GamePhase Phase { get { return Session.CurrentPhase; } }
    public static GameBoard Board { get { return GM.CurrentBoard; } }
    
    public static PlayerState GetPlayer(PlayerC p)
    {
        return Session.Players.TryGetValue(p, out PlayerState r) ? r : null;
    }

    public static GameEvent E(GEvents e)
    {
        return new GameEvent(e);
    }

    public static Coroutine C(IEnumerator co)
    {
        return GM.StartCoroutine(co);
    }

    public static void HandleEvent(PlayerState pc, GameEvent e,TokenState t=null)
    {
        if(Phase != null) Phase.HandleEvent(pc,e,t);
        else Session.HandleEvent(pc,e,t);
    }

    public static List<PlayerState> GetRanked(GResources res)
    {
        List<PlayerState> r = new List<PlayerState>();
        List<PlayerState> temp = Session.Players.Values.ToList();
        int safety = 99;
        while (safety > 0 && temp.Count > 0)
        {
            safety--;
            int best = -99999;
            List<PlayerState> b = new List<PlayerState>();
            foreach (PlayerState ps in temp)
            {
                int amt = ps.GetResource(res);
                if (amt > best)
                {
                    b.Clear();
                    best = amt;
                }
                if(amt == best) b.Add(ps);
            }

            if (b.Count == 0) b = temp;
            while(b.Count > 0)
            {
                PlayerState ps = b.Random();
                b.Remove(ps);
                temp.Remove(ps);
                r.Add(ps);
            }
        }
        return r;
    }
    
    public static List<PlayerState> GetWinner(GResources res)
    {
        List<PlayerState> r = new List<PlayerState>();
        int best = 0;
        // Debug.Log("GET WINNER: " + best + " / " + res);
        foreach (PlayerState ps in Session.Players.Values)
        {
            int amt = ps.GetResource(res);
            // Debug.Log("AMT: " + ps.Who + " / " + amt);
            if (amt > best)
            {
                r.Clear();
                best = amt;
            }
            if(amt == best) r.Add(ps);
            // Debug.Log("TIE: " + r.Count);
        }
        return r;
    }

    public static Coroutine Wait(float t, float slow = -1, float fast = -1)
    {
        return C(WaitRaw(t, slow, fast));
    }
    
    public static IEnumerator WaitRaw(float t, float slow = -1, float fast = -1)
    {
        float amt = t;
        if (GameSettings.Speed == GameSpeeds.Slow && slow > 0) amt = slow;
        else if (GameSettings.Speed == GameSpeeds.Fast && fast > 0) amt = fast;
        yield return new WaitForSeconds(amt);
    }
}