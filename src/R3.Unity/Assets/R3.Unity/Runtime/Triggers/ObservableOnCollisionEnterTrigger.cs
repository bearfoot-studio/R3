#if R3_PHYSICS_SUPPORT
using UnityEngine;

namespace R3.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnCollisionEnterTrigger : ObservableOnCollisionTrigger
    {
        /// <summary>OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.</summary>
        void OnCollisionEnter(Collision collision) => CollisionSubject.OnNext(collision);
    }
}
#endif
