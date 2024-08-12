#if R3_PHYSICS_SUPPORT
using UnityEngine;

namespace R3.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnCollisionExitTrigger : ObservableOnCollisionTrigger
    {
        /// <summary>OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.</summary>
        void OnCollisionExit(Collision collisionInfo) => CollisionSubject.OnNext(collisionInfo);
    }
}
#endif