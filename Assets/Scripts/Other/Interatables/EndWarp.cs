using UnityEngine;
using ProjectCycle.GameSystems;

namespace ProjectCycle.Interactable
{
    public class EndWarp : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            print("hit");
            GameManager.instance.SetVictory();
        }
    }
}
