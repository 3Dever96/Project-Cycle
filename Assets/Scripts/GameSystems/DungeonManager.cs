using UnityEngine;

namespace ProjectCycle.GameSystems
{
    public class DungeonManager : MonoBehaviour
    {
        public DungeonType dungeonType;
        public int completedDungeons;

        public void BeginningDungeon(bool isPalace)
        {
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
