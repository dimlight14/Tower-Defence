using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Towers
{
    [RequireComponent(typeof(TowerMainComponent))]
    public abstract class TowerComponent : MonoBehaviour
    {
        protected TowerMainComponent towerMainComponent;

        private void Awake() {
            SetComponent();
        }

        protected virtual void SetComponent() {
            towerMainComponent = GetComponent<TowerMainComponent>();
            towerMainComponent.LevelStarted += OnLevelStarted;
            towerMainComponent.LevelEndedTowerEvent += OnLevelEnded;
        }

        protected abstract void OnLevelEnded();

        protected abstract void OnLevelStarted();
    }
}
