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
            // Clamp the size to ensure it doesn't exceed the maximum size or go below 1.
            size = Mathf.Clamp(size, 1, maxSize);

            // Initialize the cell grid with the specified size.
            cellGrid = new CellData[size, size];

            // Create cells for the grid.
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    cellGrid[x, y] = new CellData(new Vector2Int(x, y));
                }
            }

            // Generate paths between cells to create the dungeon layout.
            GeneratePaths(size);

            // Set the types of cells (e.g., start, normal, final).
            SetCellTypes(size);
            startCell.CellType = 0; // Set the starting cell type.
            SetFinalCell(size); // Determine and set the final cell.

            // Instantiate the cells in the scene based on the generated grid.
            InstantiateCells(size);

            // Build the navigation mesh for pathfinding.
            surface.BuildNavMesh();

            // Initialize the cells (e.g., set up their properties or behaviors).
            InitCells();
        }

        // Generates paths between cells using a depth-first search algorithm.
        private void GeneratePaths(int size)
        {
            // Stack to keep track of cells during the path generation process.
            Stack<CellData> stack = new Stack<CellData>();

            // Determine the starting cell position and initialize its distance from the start.
            startCell = GetStartCellPosition(size);
            startCell.DistanceFromStart = 0;
            stack.Push(startCell);

            // Continue generating paths until all cells are visited.
            while (stack.Count > 0)
            {
                CellData current = stack.Pop();

                // Get unvisited neighboring cells of the current cell.
                List<CellData> neighbors = GetUnvisitedNeighbors(current, size);

                if (neighbors.Count > 0)
                {
                    // Push the current cell back onto the stack.
                    stack.Push(current);

                    // Randomly select a neighboring cell to connect to.
                    CellData chosenNeighbor = neighbors[Random.Range(0, neighbors.Count)];

                    // Create doors between the current cell and the chosen neighbor.
                    if (chosenNeighbor.Position.x > current.Position.x)
                    {
                        current.Doors[2] = true;
                        chosenNeighbor.Doors[3] = true;
                    }
                    else if (chosenNeighbor.Position.x < current.Position.x)
                    {
                        current.Doors[3] = true;
                        chosenNeighbor.Doors[2] = true;
                    }
                    else if (chosenNeighbor.Position.y > current.Position.y)
                    {
                        current.Doors[0] = true;
                        chosenNeighbor.Doors[1] = true;
                    }
                    else if (chosenNeighbor.Position.y < current.Position.y)
                    {
                        current.Doors[1] = true;
                        chosenNeighbor.Doors[0] = true;
                    }

                    // Update the distance from the start for the chosen neighbor.
                    chosenNeighbor.DistanceFromStart = current.DistanceFromStart + 1;

                    // Push the chosen neighbor onto the stack to continue path generation.
                    stack.Push(chosenNeighbor);
                }
            }
        }

        // Determines the starting cell position from the perimeter cells of the grid.
        private CellData GetStartCellPosition(int size)
        {
            List<CellData> perimeterCells = new List<CellData>();

            // Add cells from the top and bottom rows to the perimeter cells list.
            for (int x = 0; x < size; x++)
            {
                perimeterCells.Add(cellGrid[x, 0]);
                perimeterCells.Add(cellGrid[x, size - 1]);
            }

            // Add cells from the left and right columns to the perimeter cells list.
            for (int y = 1; y < size - 1; y++)
            {
                perimeterCells.Add(cellGrid[0, y]);
                perimeterCells.Add(cellGrid[size - 1, y]);
            }

            // Randomly select a cell from the perimeter cells as the starting cell.
            return perimeterCells[Random.Range(0, perimeterCells.Count)];
        }

        // Retrieves unvisited neighboring cells of the given cell.
        private List<CellData> GetUnvisitedNeighbors(CellData cell, int size)
        {
            List<CellData> neighbors = new List<CellData>();

            // Check each direction (left, right, up, down) for unvisited neighbors.
            if (cell.Position.x > 0 && cellGrid[cell.Position.x - 1, cell.Position.y].DistanceFromStart == 0)
            {
                neighbors.Add(cellGrid[cell.Position.x - 1, cell.Position.y]);
            }

            if (cell.Position.x < size - 1 && cellGrid[cell.Position.x + 1, cell.Position.y].DistanceFromStart == 0)
            {
                neighbors.Add(cellGrid[cell.Position.x + 1, cell.Position.y]);
            }

            if (cell.Position.y > 0 && cellGrid[cell.Position.x, cell.Position.y - 1].DistanceFromStart == 0)
            {
                neighbors.Add(cellGrid[cell.Position.x, cell.Position.y - 1]);
            }

            if (cell.Position.y < size - 1 && cellGrid[cell.Position.x, cell.Position.y + 1].DistanceFromStart == 0)
            {
                neighbors.Add(cellGrid[cell.Position.x, cell.Position.y + 1]);
            }

            return neighbors;
        }

        // Sets the type of each cell in the grid based on its distance from the start.
        private void SetCellTypes(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    // Set the cell type to 1 (normal) if it is not the starting cell.
                    if (cellGrid[x, y].DistanceFromStart != 0)
                    {
                        cellGrid[x, y].CellType = 1;
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
                    // Find the cell with the maximum distance from the start and only one door.
                    if (cellGrid[x, y].DistanceFromStart > distance)
                    {
                        int doors = 0;

                        for (var i = 0; i < 4; i++)
                        {
                            if (cellGrid[x, y].Doors[i])
                            {
                                doors++;
                            }
                        }

                        if (doors == 1)
                        {
                            finalCell = cellGrid[x, y];
                            distance = cellGrid[x, y].DistanceFromStart;
                        }
                    }
                }
            }

            // Set the final cell type to 2.
            finalCell.CellType = 2;
        }

        // Instantiates cell objects in the scene based on the generated grid.
        private void InstantiateCells(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
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
