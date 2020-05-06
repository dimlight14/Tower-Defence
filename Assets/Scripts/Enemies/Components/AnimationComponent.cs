using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{
    [RequireComponent(typeof(HealthComponent))]
    public class AnimationComponent : EnemyComponent
    {
        private const string IdleTrigger = "idle";
        private const string GotHitTrigger = "get hit";

        private Animator animatorComp;

        protected override void SetComponent() {
            base.SetComponent();
            animatorComp = Utility.GetComponentSafely<Animator>(gameObject);
            GetComponent<HealthComponent>().GetHitEvent += OnHit;
        }

        private void OnHit() {
            animatorComp.SetTrigger(GotHitTrigger);
        }

        protected override void OnStart() {
            animatorComp.SetTrigger(IdleTrigger);
        }
        protected override void OnEnemyDisabled() {
            animatorComp.ResetTrigger(IdleTrigger);
            animatorComp.ResetTrigger(GotHitTrigger);
        }

    }
}
