using UnityEngine;

namespace ProjectCycle.Generator
{
    // This class represents the data structure for a single cell in the dungeon grid.
    // It stores information about the cell's position, doors, distance from the start, and type.
    [System.Serializable]
    public class CellData
    {
        // The position of the cell in the grid, represented as a 2D integer vector (x, y).
        public Vector2Int Position { get; set; }

        // Array representing the doors of the cell.
        // Each index corresponds to a direction: 0 = Up, 1 = Down, 2 = Right, 3 = Left.
        // True means the door is open, and false means the door is closed.
        public bool[] Doors { get; set; } = new bool[4];

        // The distance of the cell from the starting cell in the dungeon.
        public int DistanceFromStart { get; set; }

        // The type of the cell:
        // 0 = Starting cell
        // 1 = Normal cell
        // 2 = Final cell
        public int CellType { get; set; }

        // Constructor to initialize the cell with its position in the grid.
        public CellData(Vector2Int position)
        {
            Position = position;
        }
    }
}
