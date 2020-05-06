using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{
    [SelectionBase]
    public class EnemyMain : MonoBehaviour
    {
        public struct Settings
        {
            public int Health;
            public int Damage;
            public int Reward;
            public float Speed;
            public List<Vector2> Path;
        }

        public Settings EnemySettings { get => enemySettings; private set => enemySettings = value; }
        private Settings enemySettings;

        public event Action StartEvent = null;
        public event Action DisableEvent = null;

        private void Awake() {
            EventBus.Subscribe<LevelEndedEvent>(OnLevelEnded);
        }

        private void OnLevelEnded(LevelEndedEvent customEvent) {
            RemoveSelf();
        }

        public void SetUpAndStart(Settings settings) {
            EnemySettings = settings;
            StartEvent?.Invoke();
        }

        public void Die() {
            EventBus.FireEvent<EnemyDiedEvent>(new EnemyDiedEvent() { Reward = enemySettings.Reward });
            RemoveSelf();
        }
        public void RemoveSelf() {
            DisableEvent?.Invoke();
            EventBus.FireEvent<EnemyDespawnedEvent>(new EnemyDespawnedEvent() { Enemy = this });
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<LevelEndedEvent>(OnLevelEnded);
        }
    }
}
