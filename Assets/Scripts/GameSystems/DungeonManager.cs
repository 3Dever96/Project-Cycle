using UnityEngine;

namespace ProjectCycle.GameSystems
{
    public class DungeonManager : MonoBehaviour
    {
        public DungeonType dungeonType;
        public int completedDungeons;
        public GameObject boss;

        private void Update()
        {
            if (dungeonType == DungeonType.MiniBoss || dungeonType == DungeonType.Palace)
            {
                if (boss != null)
                {
                    if (!boss.activeInHierarchy)
                    {
                        GameManager.instance.SetVictory();
                    }
                }
            }
        }

        public void BeginningDungeon(bool isPalace)
        {
            boss = null;
            GameManager.instance.gameState = GameState.Play;

            if (isPalace)
            {
                dungeonType = DungeonType.Palace;
            }
            else
            {
                dungeonType = (DungeonType)Random.Range(0, 5);
            }
        }
    }
}
