using System.Collections.Generic;
using UnityEngine;

namespace ProjectCycle.Generator
{
    public class Generator : MonoBehaviour
    {
        public int maxSize;
        public GameObject cellPrefab;
        private CellData[,] cellGrid;
        private CellData startCell;

        private void Start()
        {
            GenerateDungeon(maxSize);
        }

        public void GenerateDungeon(int size)
        {
            size = Mathf.Clamp(size, 1, maxSize);
            cellGrid =  new CellData[size, size];

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    cellGrid[x, y] = new CellData(new Vector2Int(x, y));
                }
            }

            GeneratePaths(size);

            SetCellTypes(size);
            startCell.CellType = 0;
            SetFinalCell(size);

            InstantiateCells(size);
        }

        private void GeneratePaths(int size)
        {
            Stack<CellData> stack = new Stack<CellData>();
            startCell = GetStartCellPosition(size);
            startCell.DistanceFromStart = 0;
            stack.Push(startCell);

            while (stack.Count > 0)
            {
                CellData current = stack.Pop();

                List<CellData> neighbors = GetUnvisitedNeighbors(current, size);

                if (neighbors.Count > 0)
                {
                    stack.Push(current);

                    CellData chosenNeighbor = neighbors[Random.Range(0, neighbors.Count)];

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

                    chosenNeighbor.DistanceFromStart = current.DistanceFromStart + 1;
                    stack.Push(chosenNeighbor);
                }
            }
        }

        private CellData GetStartCellPosition(int size)
        {
            List<CellData> perimeterCells = new List<CellData>();

            for (int x = 0; x < size; x++)
            {
                perimeterCells.Add(cellGrid[x, 0]);
                perimeterCells.Add(cellGrid[x, size - 1]);
            }

            for (int y = 1; y < size - 1; y++)
            {
                perimeterCells.Add(cellGrid[0,y]);
                perimeterCells.Add(cellGrid[size - 1,y]);
            }

            return perimeterCells[Random.Range(0, perimeterCells.Count)];
        }

        private List<CellData> GetUnvisitedNeighbors(CellData cell, int size)
        {
            List<CellData> neighbors = new List<CellData>();

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

        private void SetCellTypes(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (cellGrid[x,y].DistanceFromStart != 0)
                    {
                        cellGrid[x, y].CellType = 1;
                    }
                }
            }
        }

        private void SetFinalCell(int size)
        {
            int distance = 0;
            CellData finalCell = null;

            for (int x = 0;  x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
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

            finalCell.CellType = 2;
        }

        private void InstantiateCells(int size)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    CellData cell = cellGrid[x, y];
                    CellObject newCell = Instantiate(cellPrefab, new Vector3(x * 30f, 0f, y * 30f), Quaternion.identity).GetComponent<CellObject>();
                    newCell.transform.parent = transform;
                    newCell.Initialize(cell);
                }
            }
        }
    }
}
