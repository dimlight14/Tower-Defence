using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{
    public class HealthComponent : EnemyComponent
    {
        public event Action GetHitEvent = null;
        [SerializeField] private int currentHp = 3;


        protected override void OnStart() {
            currentHp = mainComponent.EnemySettings.Health;
        }
        protected override void OnEnemyDisabled() {

        }

        public void GetHit(int damage) {
            currentHp -= damage;
            if (currentHp > 0) {
                GetHitEvent?.Invoke();
            }
            else {
                mainComponent.Die();
            }
        }
    }
}
