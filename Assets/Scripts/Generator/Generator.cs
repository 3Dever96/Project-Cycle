using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

namespace ProjectCycle.Generator
{
    // This class is responsible for generating a dungeon layout using a grid-based system.
    // It creates cells, connects them with doors, assigns cell types, and builds a navigation mesh for pathfinding.
    public class Generator : MonoBehaviour
    {
        // Maximum size of the dungeon grid.
        public int maxSize;
        [SerializeField] private int minRoom;

        // Prefab used to instantiate individual cells in the dungeon.
        public GameObject cellPrefab;

        // 2D array to store the grid of cells.
        private CellData[,] cellGrid;

        // Reference to the starting cell of the dungeon.
        private CellData startCell;

        // List to store all instantiated cell objects.
        private List<CellObject> cellObjects = new List<CellObject>();

        // Reference to the NavMeshSurface component for building the navigation mesh.
        private NavMeshSurface surface;

        // Called when the script is initialized.
        private void Start()
        {
            // Get the NavMeshSurface component attached to the GameObject.
            surface = GetComponent<NavMeshSurface>();

            // Generate the dungeon layout with the specified maximum size.
            GenerateDungeon(maxSize);
        }

        // Main method to generate the dungeon layout.
        public void GenerateDungeon(int size)
        {
            size = Mathf.Clamp(size, 1, maxSize);
            cellGrid = new CellData[size, size];

            float emptyCellProbability = 0.4f; // Adjust this value to control the likelihood of empty spots.

            bool isDungeonValid = false;

            while (!isDungeonValid)
            {
                // Clear the grid and start over
                cellGrid = new CellData[size, size];

                // Start with one guaranteed non-empty cell along the perimeter
                Vector2Int startPosition = GetStartCellPosition(size);
                startCell = new CellData(startPosition);
                startCell.DistanceFromStart = 0; // Starting cell has a distance of 0
                cellGrid[startPosition.x, startPosition.y] = startCell;

                // Use a queue for flood-fill to ensure connectivity
                Queue<CellData> queue = new Queue<CellData>();
                queue.Enqueue(startCell);

                while (queue.Count > 0)
                {
                    CellData currentCell = queue.Dequeue();
                    List<Vector2Int> neighbors = GetNeighborPositions(currentCell.Position, size);

                    foreach (Vector2Int neighborPos in neighbors)
                    {
                        // If the neighbor is empty and passes the probability check
                        if (cellGrid[neighborPos.x, neighborPos.y] == null && Random.value > emptyCellProbability)
                        {
                            // Create a new cell and connect it to the current cell
                            CellData newCell = new CellData(neighborPos);
                            newCell.DistanceFromStart = currentCell.DistanceFromStart + 1; // Set distance from start
                            cellGrid[neighborPos.x, neighborPos.y] = newCell;
                            ConnectCells(currentCell.Position, neighborPos);

                            // Add the new cell to the queue for further expansion
                            queue.Enqueue(newCell);
                        }
                        else if (cellGrid[neighborPos.x, neighborPos.y] != null)
                        {
                            // If the neighbor already exists, check if the distance can be updated
                            CellData neighborCell = cellGrid[neighborPos.x, neighborPos.y];
                            int newDistance = currentCell.DistanceFromStart + 1;

                            if (newDistance < neighborCell.DistanceFromStart)
                            {
                                neighborCell.DistanceFromStart = newDistance;
                                ConnectCells(currentCell.Position, neighborPos);

                                // Add the neighbor to the queue to propagate the updated distance
                                queue.Enqueue(neighborCell);
                            }
                        }
                    }
                }

                // Ensure the start cell has at least one door
                EnsureStartCellHasDoor(startCell, size);

                // Check if the dungeon meets the minimum room requirement
                int roomCount = CountRooms(size);
                if (roomCount >= minRoom)
                {
                    isDungeonValid = true;
                }
            }

            SetCellTypes(size);
            startCell.CellType = CellType.Start;
            SetFinalCell(size);
            InstantiateCells(size);
            surface.BuildNavMesh();
            InitCells();
        }

        private void EnsureStartCellHasDoor(CellData startCell, int size)
        {
            List<Vector2Int> neighbors = GetNeighborPositions(startCell.Position, size);

            foreach (Vector2Int neighborPos in neighbors)
            {
                if (cellGrid[neighborPos.x, neighborPos.y] != null)
                {
                    ConnectCells(startCell.Position, neighborPos);
                    break; // Ensure only one door is created
                }
            }
        }

        private int CountRooms(int size)
        {
            int roomCount = 0;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (cellGrid[x, y] != null)
                    {
                        roomCount++;
                    }
                }
            }

            return roomCount;
        }

        private List<Vector2Int> GetNeighborPositions(Vector2Int position, int size)
        {
            List<Vector2Int> neighbors = new List<Vector2Int>();

            if (position.x > 0) neighbors.Add(new Vector2Int(position.x - 1, position.y));
            if (position.x < size - 1) neighbors.Add(new Vector2Int(position.x + 1, position.y));
            if (position.y > 0) neighbors.Add(new Vector2Int(position.x, position.y - 1));
            if (position.y < size - 1) neighbors.Add(new Vector2Int(position.x, position.y + 1));

            return neighbors;
        }

        private void ConnectCells(Vector2Int currentPos, Vector2Int neighborPos)
        {
            CellData current = cellGrid[currentPos.x, currentPos.y];
            CellData neighbor = cellGrid[neighborPos.x, neighborPos.y];

            if (neighborPos.x > currentPos.x)
            {
                current.Doors[2] = true;
                neighbor.Doors[3] = true;
            }
            else if (neighborPos.x < currentPos.x)
            {
                current.Doors[3] = true;
                neighbor.Doors[2] = true;
            }
            else if (neighborPos.y > currentPos.y)
            {
                current.Doors[0] = true;
                neighbor.Doors[1] = true;
            }
            else if (neighborPos.y < currentPos.y)
            {
                current.Doors[1] = true;
                neighbor.Doors[0] = true;
            }
        }


        private Vector2Int GetStartCellPosition(int size)
        {
            List<Vector2Int> perimeterPositions = new List<Vector2Int>();

            // Add positions from the top and bottom rows to the perimeter positions list.
            for (int x = 0; x < size; x++)
            {
                perimeterPositions.Add(new Vector2Int(x, 0));
                perimeterPositions.Add(new Vector2Int(x, size - 1));
            }

            // Add positions from the left and right columns to the perimeter positions list.
            for (int y = 1; y < size - 1; y++)
            {
                perimeterPositions.Add(new Vector2Int(0, y));
                perimeterPositions.Add(new Vector2Int(size - 1, y));
            }

            // Randomly select a position from the perimeter positions as the starting position.
            return perimeterPositions[Random.Range(0, perimeterPositions.Count)];
        }

        // Sets the type of each cell in the grid based on its distance from the start.
        private void SetCellTypes(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (cellGrid[x, y] != null)
                    {
                        // Set the cell type to 1 (normal) if it is not the starting cell.
                        if (cellGrid[x, y].DistanceFromStart != 0)
                        {
                            cellGrid[x, y].CellType = (CellType)Random.Range(1, 6);
                        }
                    }
                }
            }
        }

        // Determines and sets the final cell in the dungeon based on its distance from the start.
        private void SetFinalCell(int size)
        {
            int distance = 0;
            CellData finalCell = null;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (cellGrid[x, y] != null)
                    {
                        // Find the cell with the maximum distance from the start and only one door.
                        if (cellGrid[x, y].DistanceFromStart > distance)
                        {
                            finalCell = cellGrid[x, y];
                            distance = cellGrid[x, y].DistanceFromStart;
                        }
                    }
                }
            }

            if (finalCell != null)
            {
                // Set the final cell type to 2.
                finalCell.CellType = CellType.Final;
            }
        }

        // Instantiates cell objects in the scene based on the generated grid.
        private void InstantiateCells(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (cellGrid[x, y] != null)
                    {
                        // Instantiate a cell prefab at the calculated position and set its properties.
                        CellData cell = cellGrid[x, y];
                        CellObject newCell = Instantiate(cellPrefab, new Vector3(x * 30f, 0f, y * 30f), Quaternion.identity).GetComponent<CellObject>();
                        newCell.cell = cell;
                        newCell.transform.parent = transform;
                        newCell.CreateCell();
                        cellObjects.Add(newCell);
                    }
                }
            }
        }

        // Initializes all instantiated cell objects.
        private void InitCells()
        {
            for (int i = 0; i < cellObjects.Count; i++)
            {
                cellObjects[i].Initialize();
            }
        }
    }
}
