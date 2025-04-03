using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    public static float AISpeedMult = 0.5f;

    //SET THROUGH JSON, DO NOT CHANGE HERE
    public static float WalkSpeed;

    
    public static void Load(JSONGame g)
    {
        WalkSpeed = g.WalkSpeed;

    }

}
