using UnityEngine;
using ProjectCycle.GameSystems;

namespace ProjectCycle.UI
{
    public class VictoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject screen;
        [SerializeField] private TMPro.TMP_Text achievement;

        private void Start()
        {
            screen.SetActive(false);
        }

        private void OnEnable()
        {
            GameManager.instance.onVictory += OnVictory;
        }

        private void OnDisable()
        {
            GameManager.instance.onVictory -= OnVictory;
        }

        void OnVictory()
        {
            screen.SetActive(true);

            switch (GameManager.instance.DungeonManager.dungeonType)
            {
                case DungeonType.None:
                    achievement.text = "GOAL!!!";
                    break;
                case DungeonType.BossKey:
                    achievement.text = "You got the Boss Key!";
                    break;
                case DungeonType.Treasure:
                    achievement.text = "Item acquired!";
                    break;
                case DungeonType.MiniBoss:
                    achievement.text = "You beat the boss!";
                    break;
                case DungeonType.Palace:
                    achievement.text = "You beat the boss!";
                    break;
            }
        }

        public void OnContinue()
        {
            GameManager.instance.SceneHandler.MoveToScene(2);
            GameManager.instance.DungeonManager.completedDungeons += 1;
            GameManager.instance.DungeonManager.BeginningDungeon(false);
        }

        public void OnQuit()
        {
            GameManager.instance.SceneHandler.MoveToScene(1);
        }
    }
}
