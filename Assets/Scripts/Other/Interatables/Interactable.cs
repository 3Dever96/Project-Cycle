using UnityEngine;

namespace ProjectCycle.Interactable
{
    public class Interactable : MonoBehaviour
    {
        public Transform interactPoint;
        [SerializeField] private float discoverDistance;
        public float interactDistance;
        public bool canInteract = true;

        private void Start()
        {
            if (interactPoint == null)
            {
                interactPoint = transform;
            }

            SphereCollider collider = interactPoint.gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = discoverDistance;
        }

        public virtual void OnInteract()
        {
            Debug.Log($"{gameObject.name} is being interacted with.");
            canInteract = false;
        }
    }
}
