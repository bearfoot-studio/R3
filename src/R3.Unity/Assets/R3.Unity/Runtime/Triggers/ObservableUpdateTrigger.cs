using UnityEngine;

namespace R3.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableUpdateTrigger : ObservableTriggerBase
    {
        SingleAssignmentSubject<Unit> update;
        Observable<Unit> enablerStream;

        private void Awake()
        {
            this.enabled = false;
            update = new ();
            enablerStream = update.Do(this,
                    onSubscribe: m => m.enabled = true,
                    onDispose: m => m.enabled = false)
                .Share();
        }

        /// <summary>Update is called every frame, if the MonoBehaviour is enabled.</summary>
        void Update()
        {
            update.OnNext(Unit.Default);
        }

        /// <summary>Update is called every frame, if the MonoBehaviour is enabled.</summary>
        public Observable<Unit> UpdateAsObservable()
        {
            return enablerStream;
        }

        protected override void RaiseOnCompletedOnDestroy()
        {
            update.OnCompleted();
        }
    }
}
