using UnityEngine;
using Unity.Cinemachine;

namespace ProjectCycle.Generator
{
    public class CellObject : MonoBehaviour
    {
        public GameObject[] doorObjects;
        public GameObject player;
        public GameObject enemy;
        public CellData cell;
        private Transform entityParent;

        private CinemachineTargetGroup targetGroup;

        public GameObject endFlag;

        public void CreateCell()
        {
            targetGroup = FindFirstObjectByType<CinemachineTargetGroup>();
            entityParent = GameObject.Find("EntityParent").transform;

            endFlag.SetActive(false);
        }

        public void Initialize()
        {
            for (var i = 0; i < doorObjects.Length; i++)
            {
                doorObjects[i].gameObject.SetActive(!cell.Doors[i]);
            }

            if (cell.CellType == 0)
            {
                Transform newPlayer = Instantiate(player, transform.position, Quaternion.identity).transform;
                newPlayer.parent = entityParent;

                targetGroup.AddMember(newPlayer, 1f, 5f);
            }
            else if (cell.CellType == 1)
            {
                Transform newEnemey = Instantiate(enemy, transform.position, Quaternion.identity).transform;
                newEnemey.parent = entityParent;
            }
            else if (cell.CellType == 2)
            {
                endFlag.SetActive(true);
            }
        }
    }
}
