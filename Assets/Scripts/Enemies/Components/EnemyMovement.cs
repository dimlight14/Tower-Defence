using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Enemies
{
    public class EnemyMovement : EnemyComponent
    {
        public event Action GoalReached = null;
        private const float PathGoalXVariation = 1.5f;
        private const float PathGoalYVariation = 1.5f;

        private List<Vector2> currentPath;
        private Vector2 direction;
        private float speed = 5;
        private bool isMoving = false;

        protected override void OnStart() {
            SetPath(new List<Vector2>(mainComponent.EnemySettings.Path));
            speed = mainComponent.EnemySettings.Speed;
            isMoving = true;
        }
        private void SetPath(List<Vector2> path) {
            currentPath = path;

            Vector2 pathGoal = currentPath[currentPath.Count - 1];
            float xVariation = UnityEngine.Random.Range(-PathGoalXVariation / 2, PathGoalXVariation / 2);
            float yVariation = UnityEngine.Random.Range(-PathGoalYVariation / 2, PathGoalYVariation / 2);
            pathGoal = new Vector2(pathGoal.x + xVariation, pathGoal.y + yVariation);
            currentPath.Add(pathGoal);

            SetDirection();
        }

        private void Update() {
            if (isMoving && currentPath != null) {
                MoveToNextPoint();
            }
        }

        private void SetDirection() {
            direction = currentPath[0] - (Vector2)transform.position;
            direction.Normalize();
        }

        private void MoveToNextPoint() {
            float distanceToNext = Vector2.Distance(transform.position, currentPath[0]);
            Vector2 movement = direction * speed * Time.deltaTime;
            if (movement.magnitude >= distanceToNext) {
                SetNextPoint();
            }
            else {
                transform.Translate(movement);
            }
        }

        private void SetNextPoint() {
            if (currentPath.Count > 1) {
                transform.position = currentPath[0];
                currentPath.RemoveAt(0);
                SetDirection();
            }
            else {
                GoalReached?.Invoke();
                StopMovement();
            }
        }

        private void StopMovement() {
            transform.position = currentPath[0];
            currentPath = null;
        }

        protected override void OnEnemyDisabled() {
            isMoving = false;
        }
    }
}
