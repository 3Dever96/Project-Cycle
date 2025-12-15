using UnityEngine;

namespace ProjectCycle.GameSystems
{
    public class PlayerManager : MonoBehaviour
    {
        public Color skinColor;
        public ArmorData armor;

        private void Start()
        {
            armor.GenerateNewOutfit();
        }
    }
}
