using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{
    [RequireComponent(typeof(EnemyMovement))]
    public class AttackComponent : EnemyComponent
    {
        private const float AttackSpeed = 4f;
        private const float InitialAttackDelay = 0.5f;

        private int damage;
        private float currentAttackDelay = 0;
        private bool isAttacking = false;

        protected override void SetComponent() {
            base.SetComponent();
            GetComponent<EnemyMovement>().GoalReached += StartAttacking;
        }
        protected override void OnStart() {
            damage = mainComponent.EnemySettings.Damage;
            isAttacking = false;
            currentAttackDelay = 0;
        }

        private void Update() {
            if (!isAttacking) return;

            currentAttackDelay -= Time.deltaTime;
            if (currentAttackDelay <= 0) {
                currentAttackDelay = AttackSpeed;
                HitPlayer();
            }
        }

        private void StartAttacking() {
            isAttacking = true;
            currentAttackDelay = InitialAttackDelay;
        }
        private void HitPlayer() {
            EventBus.FireEvent<PlayerHitEvent>(new PlayerHitEvent() { Damage = damage });
        }

        protected override void OnEnemyDisabled() {
            isAttacking = false;
        }
    }
}
