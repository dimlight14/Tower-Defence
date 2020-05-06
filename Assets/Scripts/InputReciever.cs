using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    [RequireComponent(typeof(Collider2D))]
    public class InputReciever : MonoBehaviour, IPointerClickHandler
    {
        public bool IsRecievingInput = false;
        private Action clickCallback = null;

        public void SetCallback(Action callback) {
            IsRecievingInput = true;
            clickCallback = callback;
            gameObject.layer = GameLayers.InputRecieverLayer;
        }

        public void DisableSelf() {
            gameObject.layer = GameLayers.IgnoreRaycast;
            IsRecievingInput = false;
            clickCallback = null;
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (IsRecievingInput) {
                clickCallback?.Invoke();
            }
        }
    }

}
