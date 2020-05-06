using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text goldDisplay = null;
        [SerializeField] private Text healhDisplay = null;
        [SerializeField] private Text upgradeCostDisplay = null;
        [SerializeField] private Text killCountDisplay = null;
        [SerializeField] private GameObject lossWindow = null;

        [Space(15)]

        [SerializeField] private string GoldDisplayText = "Золото: ";
        [SerializeField] private string HealthDisplayText = "Здоровье: ";
        [SerializeField] private string killCountDisplayText = "Врагов убито: ";
        [SerializeField] private string upgradeCostDisplayText = "Стоимость улучшения: ";

        private bool waitingForInput = false;
        private Action restartCallback = null;

        public void UpdateGold(int currentGold) {
            goldDisplay.text = GoldDisplayText + currentGold.ToString();
        }

        public void UpdateHealth(int currentHealth) {
            healhDisplay.text = HealthDisplayText + currentHealth.ToString();
        }

        public void OpenLossWindow(int killCount, Action restartCallback) {
            killCountDisplay.text = killCountDisplayText + killCount.ToString();
            lossWindow.SetActive(true);
            this.restartCallback = restartCallback;
            waitingForInput = true;
        }

        public void ShowUpradePrice(int upgradeCost) {
            upgradeCostDisplay.text = upgradeCostDisplayText + upgradeCost.ToString();
        }

        private void Update() {
            if (waitingForInput) {
                if (Input.GetKeyDown(KeyCode.R)) {
                    waitingForInput = false;
                    lossWindow.SetActive(false);
                    restartCallback();
                }
            }
        }
    }
}