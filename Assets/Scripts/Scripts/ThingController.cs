using UnityEngine;

public class ThingController : MonoBehaviour
{
    public virtual LocInfo MakeLocInfo()
    {
        return new LocInfo(transform.position);
    }
}
