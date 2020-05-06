using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private EnemyMain enemyPrefab = null;

        private Queue<EnemyMain> enemyPool = new Queue<EnemyMain>();
        private const int MaxPoolSize = 50;

        private void Awake() {
            EventBus.Subscribe<EnemyDespawnedEvent>(OnEnemyDespawned);
        }

        private void OnEnemyDespawned(EnemyDespawnedEvent customEvent) {
            if (enemyPool.Count < MaxPoolSize) {
                enemyPool.Enqueue(customEvent.Enemy);
                customEvent.Enemy.gameObject.SetActive(false);
            }
            else {
                Destroy(customEvent.Enemy.gameObject);
            }
        }

        public EnemyMain Create(EnemyMain.Settings settings, Vector2 startingPosition) {
            EnemyMain newEnemy;
            if (enemyPool.Count > 0) {
                newEnemy = enemyPool.Dequeue();
                newEnemy.gameObject.SetActive(true);
            }
            else {
                newEnemy = Instantiate(enemyPrefab, new Vector3(), Quaternion.identity);
            }

            newEnemy.transform.position = startingPosition;
            newEnemy.SetUpAndStart(settings);
            return newEnemy;
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<EnemyDespawnedEvent>(OnEnemyDespawned);
        }
    }
}
