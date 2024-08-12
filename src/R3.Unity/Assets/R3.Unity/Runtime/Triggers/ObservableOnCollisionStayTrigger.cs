#if R3_PHYSICS_SUPPORT
using UnityEngine;

namespace R3.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableOnCollisionStayTrigger : ObservableOnCollisionTrigger
    {
        /// <summary>OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.</summary>
        void OnCollisionStay(Collision collisionInfo) => CollisionSubject.OnNext(collisionInfo);
    }
}
#endif