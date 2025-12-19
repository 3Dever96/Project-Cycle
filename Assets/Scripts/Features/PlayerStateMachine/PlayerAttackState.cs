using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectCycle.PlayerControl
{
    public class PlayerAttackState : PlayerState
    {
        [SerializeField] private GameObject hurtBox;

        public override void StartState(PlayerStateMachine player)
        {
            hurtBox.SetActive(false);
            player.CurrentSpeed = 0f;
        }

        public override void UpdateState(PlayerStateMachine player)
        {
            
        }

        public override void ChangeState(PlayerStateMachine player)
        {
            
        }

        public override void ExitState(PlayerStateMachine player)
        {
            hurtBox.SetActive(false);
        }

        protected override void OnAction(InputAction.CallbackContext context)
        {
            
        }

        public IEnumerator Attack(PlayerStateMachine player)
        {
            if (!hurtBox.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.1f);
                hurtBox.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                hurtBox.SetActive(false);
                player.SetState(player.GroundState);
            }
        }
    }
}
