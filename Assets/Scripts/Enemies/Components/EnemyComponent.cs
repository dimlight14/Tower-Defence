using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{
    [RequireComponent(typeof(EnemyMain))]
    public abstract class EnemyComponent : MonoBehaviour
    {
        protected EnemyMain mainComponent;

        private void Awake() {
            SetComponent();
        }

        protected virtual void SetComponent() {
            mainComponent = GetComponent<EnemyMain>();
            mainComponent.StartEvent += OnStart;
            mainComponent.DisableEvent += OnEnemyDisabled;
        }

        protected abstract void OnStart();
        protected abstract void OnEnemyDisabled();
    }
}
