using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LibraryManager : MonoBehaviour
{
   public List<PColor> PColors;
   public Dictionary<Players, PColor> PlayerDict = new Dictionary<Players, PColor>();

   public void Awake()
   {
      God.Library = this;
      foreach(PColor p in PColors)
         PlayerDict.Add(p.Type,p);
      
      TextAsset j = Resources.Load<TextAsset>("GameSettings");
      JSONGame g = JsonUtility.FromJson<JSONGame>(j.text);
      GameSettings.Load(g);
   }


   public PColor GetPlayer(Players p)
   {
      return PlayerDict.ContainsKey(p) ? PlayerDict[p] : null;
   }

}

[System.Serializable]
public class PColor
{
   public Players Type;
   public string Name;
   public Color C;
}