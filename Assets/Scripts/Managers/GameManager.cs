using TowerDefence.Enemies;
using TowerDefence.Towers;
using UnityEngine;

namespace TowerDefence.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager uiManager = null;
        [SerializeField] private EnemySpawner enemySpawner = null;
        [SerializeField] private EnemiesManager enemiesManager = null;
        [SerializeField] private TowersManager towersManager = null;
        [SerializeField] private GoldManager goldManager = null;
        [SerializeField] private PlayerHealthManager playerHealthManager = null;
        [Space(15)]
        [SerializeField] private TowerSettings towerSettings = null;
        [SerializeField] private EnemiesSpawnSettings enemiesSpawnSettings = null;

        private int killCount = 0;


        private void Start() {
            SetDependencies();

            EventBus.Subscribe<EnemyDiedEvent>(OnEnemyDied);
            RestartLevel();
        }

        private void SetDependencies() {
            enemySpawner.SetUp(enemiesSpawnSettings);
            towersManager.SetUp(enemiesManager, towerSettings, goldManager, uiManager);
            goldManager.SetUp(uiManager);
            playerHealthManager.SetUp(uiManager, this);
        }

        private void RestartLevel() {
            killCount = 0;
            EventBus.FireEvent<LevelStartedEvent>();
        }

        private void OnEnemyDied(EnemyDiedEvent customEvent) {
            killCount++;
        }

        public void LoseGame() {
            EventBus.FireEvent<LevelEndedEvent>();
            uiManager.OpenLossWindow(killCount, RestartLevel);
        }

        private void OnDestroy() {
            EventBus.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
        }
    }
}