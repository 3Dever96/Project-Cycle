using UnityEngine;
using ProjectCycle.GameSystems;

namespace ProjectCycle.Interactable
{
    public class EndCollectable : Interactable
    {
        private void Update()
        {
            transform.Rotate(new Vector3(0f, 50f, 0f) * Time.deltaTime);
        }

        public override void OnInteract()
        {
            base.OnInteract();

            GameManager.instance.SetVictory();
        }
    }
}
