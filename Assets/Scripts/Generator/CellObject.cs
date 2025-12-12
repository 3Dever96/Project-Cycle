using UnityEngine;
using Unity.Cinemachine;

namespace ProjectCycle.Generator
{
    public class CellObject : MonoBehaviour
    {
        public GameObject[] doorObjects;
        public GameObject player;
        public CinemachineTargetGroup targetGroup;

        public GameObject basicFlag;
        public GameObject endFlag;

        public void Initialize(CellData cell)
        {
            targetGroup = FindFirstObjectByType<CinemachineTargetGroup>();

            for (var i = 0; i < doorObjects.Length; i++)
            {
                doorObjects[i].gameObject.SetActive(!cell.Doors[i]);
            }

            basicFlag.SetActive(false);
            endFlag.SetActive(false);

            if (cell.CellType == 0)
            {
                targetGroup.AddMember(Instantiate(player, transform.position, Quaternion.identity).transform, 1f, 5f);
            }
            else if (cell.CellType == 1)
            {
                basicFlag.SetActive(true);
            }
            else if (cell.CellType == 2)
            {
                endFlag.SetActive(true);
            }
        }
    }
}
