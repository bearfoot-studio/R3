#if R3_PHYSICS_SUPPORT
using UnityEngine;

namespace R3.Triggers
{
    public abstract class ObservableOnCollisionTrigger : ObservableTriggerBase
    {
        protected readonly Subject<Collision> CollisionSubject = new();

        /// <summary>OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.</summary>
        public Observable<Collision> AsObservable() => CollisionSubject;

        protected override void RaiseOnCompletedOnDestroy() => CollisionSubject.OnCompleted();
    }
}
#endif