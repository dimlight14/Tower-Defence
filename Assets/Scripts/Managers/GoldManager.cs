using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefence.Enemies;
using UnityEngine;

namespace TowerDefence.Managers
{
    public class GoldManager : MonoBehaviour
    {
        public int CurrentGold { get => currentGold; private set => currentGold = value; }
        private int currentGold = 0;
        private UIManager uiManager;

        private void Awake() {
            EventBus.Subscribe<LevelStartedEvent>(OnLevelStarted);
            EventBus.Subscribe<EnemyDiedEvent>(OnEnemyDied);
        }

        public void SetUp(UIManager uiManager) {
            this.uiManager = uiManager;
        }

        private void OnEnemyDied(EnemyDiedEvent customEvent) {
            CurrentGold += customEvent.Reward;
            uiManager.UpdateGold(CurrentGold);
        }

        private void OnLevelStarted(LevelStartedEvent customEvent) {
            CurrentGold = 0;
            uiManager.UpdateGold(CurrentGold);
        }

        public void SetComponent(UIManager uiManager) {
            this.uiManager = uiManager;
        }

        public void SpendGold(int amount) {
            CurrentGold -= amount;
            uiManager.UpdateGold(CurrentGold);
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<LevelStartedEvent>(OnLevelStarted);
            EventBus.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
        }
    }
}
