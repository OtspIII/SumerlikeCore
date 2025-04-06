using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class GameManager : MonoBehaviour
{
    public Camera Cam;
    public Coroutine PhaseC;
    public GameBoard CurrentBoard;

    public Dictionary<GBoards, GameBoard> BoardDict = new Dictionary<GBoards, GameBoard>();
    public Dictionary<PlayerC, PlayerStatsheet> SheetDict = new Dictionary<PlayerC, PlayerStatsheet>();

    void Awake()
    {
        God.GM = this;
    }

    void Start()
    {
        foreach (PlayerC c in God.Session.Players.Keys)
        {
            PlayerStats ps = God.Session.Players[c];
            SheetDict[c].Setup(ps);
            ps.PC.Setup();
        }
        God.Session.NextPhase();
    }

    void Update()
    {
        if(God.Phase != null) God.Phase.Run();
        // foreach (PlayerC c in God.Session.Players.Keys)
        // {
        //     SheetDict[c].Imprint();
        // }
    }

    public GameBoard GetBoard(GBoards b)
    {
        if (BoardDict.TryGetValue(b, out GameBoard r)) return r;
        Debug.Log("UNFOUND BOARD: " + b);
        return null;
    }

    public void StartPhase(GamePhase p)
    {
        Debug.Log("START PHASE: " + p);
        if (p.Board != GBoards.None)
        {
            GameBoard b = GetBoard(p.Board);
            SetBoard(b);
        }

        if(PhaseC != null) StopCoroutine(PhaseC);
        PhaseC = StartCoroutine(p.Perform());
    }

    public void SetBoard(GameBoard b)
    {
        CurrentBoard = b;
        if(CurrentBoard.CamPosition.position != Cam.transform.position)
            StartCoroutine(CameraTransition(CurrentBoard.CamPosition));
    }

    public IEnumerator CameraTransition(Transform tr, float duration=0.2f)
    {
        float t = 0;
        Vector3 tpos = tr.position;
        tpos.z = -10;
        while (t < 1)
        {
            float tt = Imp.Ease (t, Eases.InOut);
            Cam.transform.position = Vector3.Lerp(Cam.transform.position,tpos,tt);
            yield return null;
            t += Time.deltaTime / duration;
        }
        Cam.transform.position = tpos;
    }

}
