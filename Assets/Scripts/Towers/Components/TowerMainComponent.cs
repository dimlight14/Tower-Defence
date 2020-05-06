using System;
using TowerDefence.Managers;
using UnityEngine;

namespace TowerDefence.Towers
{
    public class TowerMainComponent : MonoBehaviour
    {
        private bool isUpgraded;
        public bool IsUpgraded { get => isUpgraded; private set => isUpgraded = value; }

        private TowerSettings settings;
        public TowerSettings Settings { get => settings; private set => settings = value; }

        public event Action LevelStarted = null;
        public event Action LevelEndedTowerEvent = null;
        public event Action TowerUpgraded = null;

        [HideInInspector] public TowersManager towersManager = null;

        private void Awake() {
            EventBus.Subscribe<SetTowersEvent>(OnTowerSet);
            EventBus.Subscribe<LevelEndedEvent>(OnLevelEnded);
        }

        private void OnLevelEnded(LevelEndedEvent customEvent) {
            LevelEndedTowerEvent?.Invoke();
        }

        private void OnTowerSet(SetTowersEvent customEvent) {
            this.settings = customEvent.Settings;
            this.towersManager = customEvent.TowersManager;
            LevelStarted?.Invoke();
            IsUpgraded = false;
        }

        public void UpgradeTower() {
            towersManager.SpendGoldOnTowerUpgrade(settings.UpgradePrice);
            isUpgraded = true;
            TowerUpgraded?.Invoke();
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<SetTowersEvent>(OnTowerSet);
            EventBus.Unsubscribe<LevelEndedEvent>(OnLevelEnded);
        }
    }
}
