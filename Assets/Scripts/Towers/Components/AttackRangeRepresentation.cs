using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Towers
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class AttackRangeRepresentation : MonoBehaviour
    {
        public float AttackRange {
            set {
                if (circleCollider == null) {
                    circleCollider = GetComponent<CircleCollider2D>();
                }
                circleCollider.radius = value;
            }
        }
        private CircleCollider2D circleCollider;


        private void Awake() {
            circleCollider = GetComponent<CircleCollider2D>();
        }


    }
}
