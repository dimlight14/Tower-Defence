using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{

    [CreateAssetMenu(fileName = "Enemies Spawn Settings")]
    public class EnemiesSpawnSettings : ScriptableObject
    {
        public int InitialHealth = 3;
        public int InitialDamage = 1;
        public int InitialReward = 4;
        public float Speed = 4;
        public List<Vector2> Path;
        public Vector2 StartingPosition = new Vector2(-7.42f, -3.63f);
        [Space(20)]
        public float BaseTimeBetweenWaves = 10;
        public float AdditionalWaveTimePerEnemy = 0.75f;
        public int WaveLengthVariation = 6;
        [Space(20)]
        public float DamageChanceToIncrease = 0.3f;
        public float HealthChanceToIncrease = 0.4f;
        public float RewardChanceToIncrease = 0.7f;
    }
}
