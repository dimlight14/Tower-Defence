using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Towers
{
    public class TowerUpgradeComponent : TowerComponent
    {
        private InputReciever inputReciever = null;
        private int upgradePrice = 0;

        protected override void OnLevelEnded() {
            inputReciever.DisableSelf();
        }

        protected override void OnLevelStarted() {
            upgradePrice = towerMainComponent.Settings.UpgradePrice;
            inputReciever.SetCallback(TryToUpgrade);
        }

        protected override void SetComponent() {
            base.SetComponent();
            inputReciever = Utility.GetComponentSafely<InputReciever>(gameObject);
        }

        private void TryToUpgrade() {
            if (!towerMainComponent.IsUpgraded && towerMainComponent.towersManager.TowerCanBeUpgraded(upgradePrice)) {
                towerMainComponent.UpgradeTower();
            }
        }
    }
}
