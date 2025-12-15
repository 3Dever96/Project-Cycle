using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectCycle.PlayerControl
{
    // This class represents the player's ground state in the state machine.
    // It handles player movement, turning, jumping, and transitions to other states while on the ground.
    public class PlayerGroundState : PlayerState
    {
        [Header("Ground Speed Variables")]
        [SerializeField] private float maxSpeed; // Maximum speed the player can achieve on the ground.
        [SerializeField] private float accel; // Acceleration rate when the player starts moving.
        [SerializeField] private float decel; // Deceleration rate when the player changes direction while moving.
        [SerializeField] private float fric; // Friction applied when the player stops moving.

        [Header("Ground Turn Variables")]
        [SerializeField] private float maxTurnAngle; // Maximum angle the player can turn while moving.
        [SerializeField] private float turnSpeed; // Speed at which the player rotates to face the movement direction.

        [Header("Vertical Speed Variables")]
        [SerializeField] private float jumpSpeed; // Vertical speed applied when the player jumps.
        [SerializeField] private float stickForce; // Force applied to keep the player grounded.

        private float moveSpeed; // Current movement speed of the player.
        private Vector3 direction; // Direction of movement.
        private Vector2 move; // Input vector for movement.
        private bool jump; // Flag to indicate if the player is attempting to jump.

        // Handles input actions for movement and jumping.
        protected override void OnAction(InputAction.CallbackContext context)
        {
            if (context.action.name == "Move")
            {
                // Read the movement input (e.g., from keyboard or joystick).
                move = context.ReadValue<Vector2>();
            }

            if (context.action.name == "Jump")
            {
                // Set the jump flag to true when the jump action is performed.
                if (context.performed)
                {
                    jump = true;
                }
            }
        }

        // Called when the ground state starts.
        public override void StartState(PlayerStateMachine player)
        {
            // Initialize the player's direction and vertical speed.
            player.Direction = transform.forward;
            player.VerticalSpeed = stickForce;
            jump = false; // Reset the jump flag.
        }

        // Called every frame to update the ground state logic.
        public override void UpdateState(PlayerStateMachine player)
        {
            // Calculate the movement direction based on camera orientation and input.
            direction = Camera.main.transform.right * move.x + Camera.main.transform.forward * move.y;
            direction.y = 0f; // Ensure the direction is horizontal.
            direction = direction.normalized; // Normalize the direction vector.

            // Handle movement and speed adjustments based on input.
            if (move != Vector2.zero)
            {
                // Check if the player needs to turn to face the movement direction.
                if (Vector3.Angle(direction, player.Direction) > maxTurnAngle)
                {
                    // Decelerate if the player is turning.
                    if (player.CurrentSpeed > 0f)
                    {
                        player.CurrentSpeed -= decel * Time.deltaTime;
                    }
                    else
                    {
                        // Stop the player and update the direction.
                        player.CurrentSpeed = 0f;
                        player.Direction = direction;
                    }
                }
                else
                {
                    // Accelerate the player towards the target speed.
                    if (player.CurrentSpeed < moveSpeed)
                    {
                        player.CurrentSpeed += accel * Time.deltaTime;
                    }
                    else
                    {
                        player.CurrentSpeed = moveSpeed;
                    }

                    // Update the player's direction.
                    player.Direction = direction;
                }
            }
            else
            {
                // Apply friction to slow down the player when there is no movement input.
                if (player.CurrentSpeed > 0f)
                {
                    player.CurrentSpeed -= fric * Time.deltaTime;
                }
                else
                {
                    player.CurrentSpeed = 0f;
                }
            }

            // Calculate the player's movement speed based on the input magnitude.
            moveSpeed = maxSpeed * move.magnitude;

            // Apply vertical speed for jumping if the jump flag is set.
            if (jump)
            {
                player.VerticalSpeed = jumpSpeed;
            }

            // Rotate the player to face the movement direction.
            player.FaceDirection(turnSpeed);

            // Move the player based on the calculated velocity.
            player.MovePlayer();

            // Check if the state needs to change (e.g., to AirState).
            ChangeState(player);
        }

        // Handles state transitions based on conditions.
        public override void ChangeState(PlayerStateMachine player)
        {
            // Transition to AirState if the player is airborne or not grounded.
            if (player.VerticalSpeed > 0f || !Physics.CheckSphere(transform.position, player.Controller.radius - 0.1f, LayerMask.GetMask("Solid")))
            {
                player.SetState(player.AirState);
            }
        }

        // Called when exiting the ground state.
        public override void ExitState(PlayerStateMachine player)
        {
            // Currently, no specific behavior is defined for exiting the ground state.
        }
    }
}
