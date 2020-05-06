using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefence.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyFactory factory = null;
        [SerializeField] private Transform enemiesContainer = null;

        private EnemiesSpawnSettings spawnSettings;

        private int waveNumber = 0;
        private int currentWaveLength = 0;
        private float timeBetweenWaves = 0;
        private float timeBetweenSpawns = 0;
        private int damageIncremental = 0;
        private int healthIncremental = 0;
        private int rewardIncremental = 0;
        private bool isSpawning = false;

        private void Awake() {
            EventBus.Subscribe<LevelStartedEvent>(OnLevelStarted);
            EventBus.Subscribe<LevelEndedEvent>(OnLevelEnded);
        }

        public void SetUp(EnemiesSpawnSettings spawnSettings) {
            this.spawnSettings = spawnSettings;
        }


        private void OnLevelStarted(CustomEvent LevelStartedEvent) {
            waveNumber = 0;
            currentWaveLength = 0;
            timeBetweenWaves = 0;
            timeBetweenSpawns = 0;
            damageIncremental = 0;
            healthIncremental = 0;
            rewardIncremental = 0;
            isSpawning = true;
        }
        private void OnLevelEnded(LevelEndedEvent customEvent) {
            isSpawning = false;
        }

        private void Update() {
            if (!isSpawning) return;

            timeBetweenWaves -= Time.deltaTime;
            if (timeBetweenWaves <= 0) {
                StartNewWave();
            }

            if (currentWaveLength > 0) {
                timeBetweenSpawns -= Time.deltaTime;
                if (timeBetweenSpawns <= 0) {
                    SpawnEnemy();
                }
            }
        }

        private void StartNewWave() {
            waveNumber++;
            currentWaveLength = Random.Range(waveNumber, waveNumber + spawnSettings.WaveLengthVariation + 1);
            timeBetweenWaves = spawnSettings.BaseTimeBetweenWaves + spawnSettings.AdditionalWaveTimePerEnemy * currentWaveLength;
            timeBetweenSpawns = 0;
            IncreaseStats();
        }

        private void IncreaseStats() {
            float chance = Random.Range(0f, 1f);
            if (chance < spawnSettings.DamageChanceToIncrease) damageIncremental++;
            chance = Random.Range(0f, 1f);
            if (chance < spawnSettings.HealthChanceToIncrease) healthIncremental++;
            chance = Random.Range(0f, 1f);
            if (chance < spawnSettings.RewardChanceToIncrease) rewardIncremental++;
        }

        private void SpawnEnemy() {
            currentWaveLength--;
            timeBetweenSpawns = spawnSettings.AdditionalWaveTimePerEnemy;

            EnemyMain newEnemy = factory.Create(
                 new EnemyMain.Settings {
                     Health = spawnSettings.InitialHealth + healthIncremental,
                     Damage = spawnSettings.InitialDamage + damageIncremental,
                     Reward = spawnSettings.InitialReward + rewardIncremental,
                     Speed = spawnSettings.Speed,
                     Path = spawnSettings.Path
                 },
                 spawnSettings.StartingPosition
             );
            EventBus.FireEvent<EnemySpawnedEvent>(new EnemySpawnedEvent() { Enemy = newEnemy });
            newEnemy.transform.SetParent(enemiesContainer);
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<LevelStartedEvent>(OnLevelStarted);
            EventBus.Unsubscribe<LevelEndedEvent>(OnLevelEnded);
        }
    }
}
