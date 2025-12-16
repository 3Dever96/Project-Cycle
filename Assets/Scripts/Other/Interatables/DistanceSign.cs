using UnityEngine;

namespace ProjectCycle.Interactable
{
    public class DistanceSign : Interactable
    {
        public int distance;

        public override void OnInteract()
        {
            base.OnInteract();
            Debug.Log($"Room is {distance} many cells away from start cell.");
        }
    }
}
