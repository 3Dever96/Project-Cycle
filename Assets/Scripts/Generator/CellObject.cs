using UnityEngine;
using Unity.Cinemachine;

namespace ProjectCycle.Generator
{
    // This class represents a cell in the dungeon and handles its initialization and entity spawning.
    public class CellObject : MonoBehaviour
    {
        // Array of GameObjects representing the doors in the cell.
        public GameObject[] doorObjects;

        // Prefab for the player character to be instantiated in the cell.
        public GameObject player;

        // Prefab for the enemy character to be instantiated in the cell.
        public GameObject enemy;

        // Data structure containing information about the cell (e.g., position, type, doors).
        public CellData cell;

        // Reference to the parent Transform for entities (player and enemies).
        private Transform entityParent;

        // Reference to the CinemachineTargetGroup for managing camera targets.
        private CinemachineTargetGroup targetGroup;

        // GameObject representing the end flag, which is activated for the final cell.
        public GameObject endFlag;

        // Method to create and initialize the cell.
        public void CreateCell()
        {
            // Find the first instance of CinemachineTargetGroup in the scene.
            targetGroup = FindFirstObjectByType<CinemachineTargetGroup>();

            // Find the GameObject named "EntityParent" and get its Transform component.
            entityParent = GameObject.Find("EntityParent").transform;

            // Deactivate the end flag initially.
            endFlag.SetActive(false);
        }

        // Method to initialize the cell based on its type and properties.
        public void Initialize()
        {
            // Loop through the door objects and set their active state based on the cell's door configuration.
            for (var i = 0; i < doorObjects.Length; i++)
            {
                doorObjects[i].gameObject.SetActive(!cell.Doors[i]);
            }

            // Handle initialization based on the cell type.
            if (cell.CellType == 0) // Starting cell
            {
                // Instantiate the player at the cell's position and set its parent to the entity parent.
                Transform newPlayer = Instantiate(player, transform.position, Quaternion.identity).transform;
                newPlayer.parent = entityParent;

                // Add the player to the CinemachineTargetGroup for camera tracking.
                targetGroup.AddMember(newPlayer, 1f, 5f);
            }
            else if (cell.CellType == 1) // Normal cell
            {
                // Instantiate an enemy at the cell's position and set its parent to the entity parent.
                Transform newEnemey = Instantiate(enemy, transform.position, Quaternion.identity).transform;
                newEnemey.parent = entityParent;
            }
            else if (cell.CellType == 2) // Final cell
            {
                // Activate the end flag to indicate the final cell.
                endFlag.SetActive(true);
            }
        }
    }
}
