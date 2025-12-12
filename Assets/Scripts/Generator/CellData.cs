using UnityEngine;

namespace ProjectCycle.Generator
{
    [System.Serializable]
    public class CellData
    {
        public Vector2Int Position { get; set; }
        public bool[] Doors { get; set; } = new bool[4];
        public int DistanceFromStart {  get; set; }
        public int CellType { get; set; }

        public CellData(Vector2Int position)
        {
            Position = position;
        }
    }
}
