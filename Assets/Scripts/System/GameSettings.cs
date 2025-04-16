using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    // public static float AISpeedMult = 0.5f;
    public static float IconFlyTime = 1f;
    public static GameSpeeds Speed = GameSpeeds.Default;

    //SET THROUGH JSON, DO NOT CHANGE HERE
    public static float WalkSpeed;
    public static int StartingTokens = 2;

    
    public static void Load(JSONGame g)
    {
        WalkSpeed = g.WalkSpeed;

    }

}

public enum GameSpeeds
{
    None=0,
    Default=1,
    Slow=2,
    Fast=3,
}