using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LibraryManager : MonoBehaviour
{
   public List<PColor> PColors;
   public Dictionary<PlayerC, PColor> PlayerDict = new Dictionary<PlayerC, PColor>();
   public TokenController TokenPrefab;
   public PlayerController PlayerPrefab;

   public void Awake()
   {
      if (God.Library != null)
      {
         Destroy(gameObject);
         return;
      }
      DontDestroyOnLoad(gameObject);
      God.Library = this;
      foreach(PColor p in PColors)
         PlayerDict.Add(p.Type,p);
      
      TextAsset j = Resources.Load<TextAsset>("GameSettings");
      JSONGame g = JsonUtility.FromJson<JSONGame>(j.text);
      GameSettings.Load(g);
   }


   public PColor GetPlayer(PlayerC p)
   {
      return PlayerDict.ContainsKey(p) ? PlayerDict[p] : null;
   }

   public TokenController SpawnToken(PlayerState who)
   {
      TokenController r = Instantiate(TokenPrefab, who.PC.transform.position, Quaternion.identity);
      r.Setup(who);
      return r;
   }
   
   public PlayerController SpawnAIPlayer(PlayerC who,PlayerState s, Vector3 where)
   {
      PlayerController r = Instantiate(PlayerPrefab, where, Quaternion.identity);
      r.Controls = ControllerType.AI;
      God.Session.Players.Add(who,s);
      r.Setup(s);
      return r;
   }

}

[System.Serializable]
public class PColor
{
   public PlayerC Type;
   public string Name;
   public Color C;
}