using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Towers
{
    [CreateAssetMenu(fileName = "Tower Settings")]
    public class TowerSettings : ScriptableObject
    {
        public int Damage;
        public float FireRate;
        public float TowerRange;
        public int UpgradePrice;
        [Space(10)]
        public int UpgradedDamage;
        public float UpgradedFireRate;
    }
}
