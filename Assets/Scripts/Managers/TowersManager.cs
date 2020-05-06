using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefence.Enemies;
using TowerDefence.Towers;
using UnityEngine;

namespace TowerDefence.Managers
{
    public class TowersManager : MonoBehaviour
    {
        private EnemiesManager enemiesManager;
        private TowerSettings settings;
        private GoldManager goldManager;
        private UIManager uiManager;

        private void Awake() {
            EventBus.Subscribe<LevelStartedEvent>(OnLevelStarted);
        }

        public void SetUp(EnemiesManager enemiesManager, TowerSettings settings, GoldManager goldManager, UIManager uiManager) {
            this.enemiesManager = enemiesManager;
            this.settings = settings;
            this.goldManager = goldManager;
            this.uiManager = uiManager;
        }

        private void OnLevelStarted(LevelStartedEvent customEvent) {
            EventBus.FireEvent<SetTowersEvent>(new SetTowersEvent() {
                Settings = settings,
                TowersManager = this
            });
            uiManager.ShowUpradePrice(settings.UpgradePrice);
        }

        public bool TowerCanBeUpgraded(int price) {
            if (goldManager.CurrentGold >= price) {
                return true;
            }
            else {
                return false;
            }
        }
        public void SpendGoldOnTowerUpgrade(int price) {
            goldManager.SpendGold(price);
        }

        public bool FindAndAttackEnemyInRange(Vector2 towerPosition, float attackRange, int damage) {
            foreach (EnemyMain enemy in enemiesManager.ActiveEnemies) {
                if (Vector2.Distance(enemy.transform.position, towerPosition) <= attackRange) {
                    HealthComponent enemyHealthComponent = enemy.GetComponent<HealthComponent>();
                    if (enemyHealthComponent != null) {
                        enemyHealthComponent.GetHit(damage);
                        return true;
                    }
                }
            }

            return false;
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<LevelStartedEvent>(OnLevelStarted);
        }
    }
}
