using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectCycle.PlayerControl
{
    // This class represents the player's air state in the state machine.
    // It handles player movement, gravity, falling, and transitions to other states while in the air.
    public class PlayerAirState : PlayerState
    {
        [SerializeField] private float gravity; // The gravity force applied to the player while in the air.
        [SerializeField] private float fallSpeed; // The maximum falling speed of the player.
        [SerializeField] private float turnSpeed; // The speed at which the player rotates to face the movement direction.

        private float verticalSpeed; // The current vertical speed of the player.

        // Handles input actions for jumping while in the air.
        protected override void OnAction(InputAction.CallbackContext context)
        {
            if (context.action.name == "Jump")
            {
                // If the jump action is canceled, limit the vertical speed to prevent further upward movement.
                if (context.canceled)
                {
                    verticalSpeed = Mathf.Min(verticalSpeed, 0f);
                }
            }
        }

        // Called when the air state starts.
        public override void StartState(PlayerStateMachine player)
        {
            // Initialize the vertical speed with the player's current vertical speed.
            verticalSpeed = player.VerticalSpeed;
        }

        // Called every frame to update the air state logic.
        public override void UpdateState(PlayerStateMachine player)
        {
            // Apply gravity to the vertical speed if it exceeds the maximum fall speed.
            if (verticalSpeed > fallSpeed)
            {
                verticalSpeed += gravity * Time.deltaTime;
            }

            // Update the player's vertical speed.
            player.VerticalSpeed = verticalSpeed;

            // Rotate the player to face the movement direction.
            player.FaceDirection(turnSpeed);

            // Move the player based on the calculated velocity.
            player.MovePlayer();

            // Check if the state needs to change (e.g., to GroundState).
            ChangeState(player);
        }

        // Handles state transitions based on conditions.
        public override void ChangeState(PlayerStateMachine player)
        {
            // Transition to GroundState if the player is grounded and vertical speed is zero or less.
            if (player.VerticalSpeed <= 0f && Physics.CheckSphere(transform.position, player.Controller.radius - 0.1f, LayerMask.GetMask("Solid")))
            {
                player.SetState(player.GroundState);
            }
        }

        // Called when exiting the air state.
        public override void ExitState(PlayerStateMachine player)
        {
            // Currently, no specific behavior is defined for exiting the air state.
        }
    }
}
