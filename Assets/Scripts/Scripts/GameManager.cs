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

    public Dictionary<GameBoards, GameBoard> BoardDict = new Dictionary<GameBoards, GameBoard>();

    void Awake()
    {
        God.GM = this;
    }

    void Start()
    {
        God.Session.NextPhase();
    }

    void Update()
    {
        if(God.Phase != null) God.Phase.Run();
    }

    public GameBoard GetBoard(GameBoards b)
    {
        if (BoardDict.TryGetValue(b, out GameBoard r)) return r;
        Debug.Log("UNFOUND BOARD: " + b);
        return null;
    }

    public void StartPhase(GamePhase p)
    {
        if(PhaseC != null) StopCoroutine(PhaseC);
        PhaseC = StartCoroutine(p.Perform());
    }

    public void SetBoard(GameBoard b)
    {
        CurrentBoard = b;
        StartCoroutine(CameraTransition(CurrentBoard.CamPosition));
    }

    public IEnumerator CameraTransition(Transform tr, float duration=0.2f)
    {
        float t = 0;
        while (t < 1)
        {
            float tt = Imp.Ease (t, Eases.InOut);
            Cam.transform.position = Vector3.Lerp(Cam.transform.position,tr.position,tt);
            yield return null;
            t += Time.deltaTime / duration;
        }
        Cam.transform.position = tr.position;
    }

}
