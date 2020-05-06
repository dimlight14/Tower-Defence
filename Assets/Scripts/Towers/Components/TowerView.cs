using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Towers
{
    public class TowerView : TowerComponent
    {
        private const string UpgradeTrigger = "upgrade";
        private const string DowngradeTrigger = "downgrade";

        private Animator animatorComp;

        protected override void SetComponent() {
            base.SetComponent();
            animatorComp = Utility.GetComponentSafely<Animator>(gameObject);

            towerMainComponent.TowerUpgraded += UpgradeTower;
        }

        protected override void OnLevelEnded() {
            animatorComp.ResetTrigger(DowngradeTrigger);
            animatorComp.ResetTrigger(UpgradeTrigger);
        }

        protected override void OnLevelStarted() {
            if (towerMainComponent.IsUpgraded)
                animatorComp.SetTrigger(DowngradeTrigger);
        }

        private void UpgradeTower() {
            animatorComp.SetTrigger(UpgradeTrigger);
        }
    }
}
