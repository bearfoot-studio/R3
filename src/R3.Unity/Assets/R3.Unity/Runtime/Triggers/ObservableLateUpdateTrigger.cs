using UnityEngine;

namespace R3.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableLateUpdateTrigger : ObservableTriggerBase
    {
        Subject<Unit> lateUpdate;
        Observable<Unit> enablerStream;

        private void Awake()
        {
            this.enabled = false;
            lateUpdate = new Subject<Unit>();
            enablerStream = lateUpdate.Do(this,
                    onSubscribe: m => m.enabled = true,
                    onDispose: m => m.enabled = false)
                .Publish().RefCount();
        }

        /// <summary>LateUpdate is called every frame, if the Behaviour is enabled.</summary>
        void LateUpdate()
        {
            lateUpdate.OnNext(Unit.Default);
        }

        /// <summary>LateUpdate is called every frame, if the Behaviour is enabled.</summary>
        public Observable<Unit> LateUpdateAsObservable()
        {
            return enablerStream;
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            lateUpdate.OnCompleted();
        }
    }
}
