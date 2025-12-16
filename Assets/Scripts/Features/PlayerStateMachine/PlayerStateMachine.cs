using UnityEngine;

namespace ProjectCycle.PlayerControl
{
    // This script implements a state machine for controlling the player's behavior.
    // It manages transitions between different states (e.g., GroundState, AirState) and handles player movement.

    [RequireComponent(typeof(CharacterController))]
    public class PlayerStateMachine : MonoBehaviour
    {
        // Reference to the CharacterController component attached to the player GameObject.
        public CharacterController Controller { get; private set; }
        public Animator Animator { get; private set; }

        // The current state of the player.
        public PlayerState CurrentState { get; private set; }

        // Reference to the player's ground state.
        public PlayerGroundState GroundState { get; private set; }

        // Reference to the player's air state.
        public PlayerAirState AirState { get; private set; }

        // Current horizontal movement speed of the player.
        public float CurrentSpeed { get; set; }

        // Current vertical movement speed of the player (e.g., for jumping or falling).
        public float VerticalSpeed { get; set; }

        // The direction in which the player is facing.
        public Vector3 Direction { get; set; }

        // The calculated velocity of the player based on speed and direction.
        public Vector3 Velocity { get; set; }

        private void Start()
        {
            // Initialize the CharacterController component.
            Controller = GetComponent<CharacterController>();
            Animator = GetComponentInChildren<Animator>();

            // Retrieve references to the ground and air state components.
            GroundState = GetComponent<PlayerGroundState>();
            AirState = GetComponent<PlayerAirState>();

            // Set the initial state of the player to GroundState.
            SetState(GroundState);
        }

        private void FixedUpdate()
        {
            // If there is a current state, update it every fixed frame.
            if (CurrentState != null)
            {
                CurrentState.UpdateState(this);
            }
        }

        // Method to transition the player to a new state.
        public void SetState(PlayerState newState)
        {
            // Exit the current state if it exists.
            if (CurrentState != null)
            {
                CurrentState.ExitState(this);
            }

            // Set the new state as the current state.
            CurrentState = newState;

            // Start the new state if it exists.
            if (CurrentState != null)
            {
                CurrentState.StartState(this);
            }
        }

        // Method to move the player based on the current speed, direction, and vertical speed.
        public void MovePlayer()
        {
            // Calculate the velocity based on the current speed and direction.
            Vector3 vel = CurrentSpeed * Direction;
            vel.y = VerticalSpeed; // Add vertical speed for jumping or falling.
            Velocity = vel;

            // Move the player using the CharacterController component.
            Controller.Move(Velocity * Time.deltaTime);
        }

        // Method to rotate the player to face the current movement direction.
        public void FaceDirection(float turnSpeed)
        {
            // Smoothly rotate the player towards the desired direction based on the turn speed.
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Direction), turnSpeed * Time.deltaTime);
        }
    }
}