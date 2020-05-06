using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Managers
{
    public class PlayerHealthManager : MonoBehaviour
    {
        private const int InitialHealth = 50;

        private UIManager uiManager;
        private GameManager gameManager;

        private int currentHealth = 0;


        public void SetUp(UIManager uiManager, GameManager gameManager) {
            this.uiManager = uiManager;
            this.gameManager = gameManager;
        }

        private void Awake() {
            EventBus.Subscribe<PlayerHitEvent>(OnPlayerHit);
            EventBus.Subscribe<LevelStartedEvent>(OnLevelStarted);
        }

        private void OnLevelStarted(LevelStartedEvent customEvent) {
            currentHealth = InitialHealth;
            uiManager.UpdateHealth(currentHealth);
        }

        private void OnPlayerHit(PlayerHitEvent customEvent) {
            currentHealth -= customEvent.Damage;
            if (currentHealth <= 0) {
                uiManager.UpdateHealth(0);
                gameManager.LoseGame();
            }
            else {
                uiManager.UpdateHealth(currentHealth);
            }
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<PlayerHitEvent>(OnPlayerHit);
            EventBus.Unsubscribe<LevelStartedEvent>(OnLevelStarted);
        }
    }
}
