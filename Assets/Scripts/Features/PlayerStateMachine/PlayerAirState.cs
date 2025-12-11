using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectCycle.PlayerControl
{
    public class PlayerAirState : PlayerState
    {
        [SerializeField] private float gravity;
        [SerializeField] private float fallSpeed;
        [SerializeField] private float turnSpeed;

        private float verticalSpeed;

        protected override void OnAction(InputAction.CallbackContext context)
        {
            if (context.action.name == "Jump")
            {
                if (context.canceled)
                {
                    verticalSpeed = Mathf.Min(verticalSpeed, 0f);
                }
            }
        }

        public override void StartState(PlayerStateMachine player)
        {
            verticalSpeed = player.VerticalSpeed;
        }

        public override void UpdateState(PlayerStateMachine player)
        {
            if (verticalSpeed > fallSpeed)
            {
                verticalSpeed += gravity * Time.deltaTime;
            }

            player.VerticalSpeed = verticalSpeed;

            player.FaceDirection(turnSpeed);
            player.MovePlayer();

            ChangeState(player);
        }

        public override void ChangeState(PlayerStateMachine player)
        {
            if (player.VerticalSpeed <= 0f && Physics.CheckSphere(transform.position, player.Controller.radius - 0.1f, LayerMask.GetMask("Solid")))
            {
                player.SetState(player.GroundState);
            }
        }

        public override void ExitState(PlayerStateMachine player)
        {
            
        }
    }
}
