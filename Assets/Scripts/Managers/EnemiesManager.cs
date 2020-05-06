using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefence.Enemies;
using UnityEngine;

namespace TowerDefence.Managers
{
    public class EnemiesManager : MonoBehaviour
    {
        private List<EnemyMain> activeEnemies = new List<EnemyMain>();
        public List<EnemyMain> ActiveEnemies { get => activeEnemies; private set => activeEnemies = value; }

        private void Awake() {
            EventBus.Subscribe<EnemySpawnedEvent>(OnEnemySpawned);
            EventBus.Subscribe<EnemyDespawnedEvent>(OnEnemyDespawned);
        }

        private void OnEnemyDespawned(EnemyDespawnedEvent customEvent) {
            activeEnemies.Remove(customEvent.Enemy);
        }

        private void OnEnemySpawned(EnemySpawnedEvent customEvent) {
            activeEnemies.Add(customEvent.Enemy);
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<EnemySpawnedEvent>(OnEnemySpawned);
            EventBus.Unsubscribe<EnemyDespawnedEvent>(OnEnemyDespawned);
        }
    }
}
