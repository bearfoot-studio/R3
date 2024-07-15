
using UnityEngine;

namespace R3.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableFixedUpdateTrigger : ObservableTriggerBase
    {
        Subject<Unit> fixedUpdate;
        Observable<Unit> enablerStream;

        private void Awake()
        {
            this.enabled = false;
            fixedUpdate = new Subject<Unit>();
            enablerStream = fixedUpdate.Do(this,
                onSubscribe: m => m.enabled = true,
                onDispose: m => m.enabled = false)
                .Publish().RefCount();
        }

        /// <summary>This function is called every fixed framerate frame, if the MonoBehaviour is enabled.</summary>
        void FixedUpdate()
        {
            fixedUpdate.OnNext(Unit.Default);
        }

        /// <summary>This function is called every fixed framerate frame, if the MonoBehaviour is enabled.</summary>
        public Observable<Unit> FixedUpdateAsObservable()
        {
            return enablerStream;
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            fixedUpdate.OnCompleted();
        }
    }
}