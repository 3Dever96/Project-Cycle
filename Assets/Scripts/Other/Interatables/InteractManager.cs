using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ProjectCycle.GameSystems;

namespace ProjectCycle.Interactable
{
    public class InteractManager : MonoBehaviour
    {
        private List<Interactable> interactables = new List<Interactable>();

        private void FixedUpdate()
        {
            if (interactables.Count > 0)
            {
                interactables.Sort(SortInteractables);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Interactable interact = other.GetComponent<Interactable>();

            if (interact != null)
            {
                if (!interactables.Contains(interact) && interact.canInteract)
                {
                    interactables.Add(interact);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Interactable interact = other.GetComponent<Interactable>();

            if (interact != null)
            {
                if (interactables.Contains(interact))
                {
                    interactables.Remove(interact);
                }
            }
        }

        private int SortInteractables(Interactable a, Interactable b)
        {
            float distA = Vector3.Distance(a.transform.position, transform.position);
            float distB = Vector3.Distance(b.transform.position, transform.position);

            if (distA < distB)
            {
                return -1;
            }

            if (distA > distB)
            {
                return 1;
            }

            return 0;
        }

        public void OnEnable()
        {
            GameManager.instance.Input.onActionTriggered += OnAction;
        }

        public void OnDisable()
        {
            GameManager.instance.Input.onActionTriggered -= OnAction;
        }

        public void OnAction(InputAction.CallbackContext context)
        {
            if (context.action.name == "Interact")
            {
                if (context.performed)
                {
                    if (interactables.Count > 0)
                    {
                        Interactable interactable = interactables[0];

                        if (Vector3.Distance(transform.position, interactable.interactPoint.position) < interactable.interactDistance)
                        {
                            interactable.OnInteract();
                            interactables.Remove(interactable);
                        }
                    }
                }
            }
        }
    }
}
