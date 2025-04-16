using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LibraryManager : MonoBehaviour
{
   public List<PColor> PColors;
   public Dictionary<PlayerC, PColor> PlayerDict = new Dictionary<PlayerC, PColor>();
   public List<ResourceArt> ResourceInfo;
   public Dictionary<GResources, ResourceArt> ResourceDict = new Dictionary<GResources, ResourceArt>();
   public TokenController TokenPrefab;
   public PlayerController PlayerPrefab;
   public IconController IconPrefab;
   

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
      foreach(ResourceArt p in ResourceInfo)
         ResourceDict.Add(p.Type,p);
      
      TextAsset j = Resources.Load<TextAsset>("GameSettings");
      JSONGame g = JsonUtility.FromJson<JSONGame>(j.text);
      GameSettings.Load(g);
   }


   public PColor GetPlayer(PlayerC p)
   {
      return PlayerDict.TryGetValue(p,out PColor r) ? r : null;
   }
   
   public ResourceArt GetResource(GResources res)
   {
      return ResourceDict.TryGetValue(res,out ResourceArt r) ? r : null;
   }

   public TokenController SpawnToken(PlayerState who)
   {
      TokenController r = Instantiate(TokenPrefab, who.PC.transform.position, Quaternion.identity);
      r.Setup(who);
      return r;
   }
   
   public IconController SpawnIcon(Vector3 where,GResources type,int amt=1)
   {
      IconController r = Instantiate(IconPrefab, where, Quaternion.identity);
      r.Setup(type,amt);
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

[System.Serializable]
public class ResourceArt
{
   public GResources Type;
   public Sprite S;
   public AudioClip Sfx;
   public IconController IconPrefab;
}