using UnityEngine;

public class TokenController : ThingController
{
    public PlayerController Owner;
    public float Speed;
    public Transform Target;
    public ZoneController AssignedZone{get{return State.Zone?.Zone;}set{SetZone(value);}}
    public Vector2 Offset;
    public float StopAt = 1;
    public SpriteRenderer Body;
    public TokenState State;

    void Start()
    {
        Speed = GameSettings.WalkSpeed * 1.5f;
    }

    public void Setup(PlayerState owner)
    {
        Owner = owner.PC;
        Owner.AddToken(this);
        State = new TokenState(this,Owner);
        Body.color = God.Library.GetPlayer(State.Owner.Who).C;
    }
    
    void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > StopAt)
        {
            Vector2 tar = (Vector2)Target.transform.position + Offset;
            Vector3 dest = Vector2.MoveTowards(transform.position, tar, Speed * Time.deltaTime);
            dest.z = transform.position.z;
            transform.position = dest;
        }
    }

    public void SetZone(ZoneController z)
    {
        SetZone(z,z.transform,Vector2.zero);
    }
    
    public void SetZone(ZoneController z,Transform t,Vector2 offset, float stop=0)
    {
        State.Zone = z.State;
        SetTarget(t,offset,stop);
    }

    public void SetTarget(float place=-1)
    {
        Target = Owner.transform;
        Offset = Vector2.zero;
        if(!Owner.Followers.Contains(this)) Owner.Followers.Add(this);
        if (place < 0) place = Owner.Followers.IndexOf(this);
        StopAt = (place+1);
        State.Zone = null;
    }

    public void SetTarget(Transform t)
    {
        SetTarget(t,new Vector2(0,0));
    }
    
    public void SetTarget(Transform t,Vector2 offset, float stop=0)
    {
        Target = t;
        Offset = offset;
        StopAt = stop;
        if(Owner.Followers.Contains(this)) Owner.Followers.Remove(this);
    }
    
    public void SpawnIcon(GResources res, int amt = 1)
    {
        IconController i = God.Library.SpawnIcon(transform.position, res, amt);
        i.Send(State.Owner);
        Owner.State.ChangeResource(res, amt);
    }
}
