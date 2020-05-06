using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Towers
{
    public class TowerAttack : TowerComponent
    {
        [SerializeField] private AttackRangeRepresentation attackRangeRepresentation;

        private float towerRange = 0;
        private int damage = 0;
        private float fireRate = 0;
        private float currentAttackDelay = 0;
        private bool isAttacking = true;

        protected override void SetComponent() {
            base.SetComponent();
            if (attackRangeRepresentation == null) {
                attackRangeRepresentation = Utility.GetComponentSafely<AttackRangeRepresentation>(gameObject);
            }
            towerMainComponent.TowerUpgraded += UpgradeTower;
        }

        protected override void OnLevelEnded() {
            isAttacking = false;
        }

        protected override void OnLevelStarted() {
            isAttacking = true;
            towerRange = towerMainComponent.Settings.TowerRange;
            attackRangeRepresentation.AttackRange = towerRange;
            damage = towerMainComponent.Settings.Damage;
            fireRate = towerMainComponent.Settings.FireRate;
            currentAttackDelay = 0;
        }

        private void UpgradeTower() {
            damage = towerMainComponent.Settings.UpgradedDamage;
            fireRate = towerMainComponent.Settings.UpgradedFireRate;
        }

        private void Update() {
            if (!isAttacking) return;

            if (currentAttackDelay <= 0) {
                TryToAttackEnemy();
            }
            else {
                currentAttackDelay -= Time.deltaTime;
            }
        }

        private void TryToAttackEnemy() {
            if (towerMainComponent.towersManager.FindAndAttackEnemyInRange(transform.position, towerRange, damage))
                currentAttackDelay = fireRate;
        }
    }
}
