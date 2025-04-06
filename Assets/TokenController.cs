using UnityEngine;

public class TokenController : MonoBehaviour
{
    public PlayerController Owner;
    public PlayerStats OwnStats;
    public float Speed;
    public Transform Target;
    public Vector2 Offset;
    public float StopAt = 1;
    public SpriteRenderer Body;

    void Start()
    {
        Speed = GameSettings.WalkSpeed * 1.5f;
    }

    public void Setup(PlayerStats owner)
    {
        Owner = owner.PC;
        OwnStats = owner;
        Owner.AddToken(this);
        Body.color = God.Library.GetPlayer(OwnStats.Who).C;
    }
    
    void Update()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > StopAt)
        {
            Vector3 dest = Vector2.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
            dest.z = transform.position.z;
            transform.position = dest;
        }
    }

    public void SetTarget(float place=0)
    {
        Target = Owner.transform;
        Offset = Vector2.zero;
        StopAt = (place+1);
        if(!Owner.Followers.Contains(this)) Owner.Followers.Add(this);
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
}
