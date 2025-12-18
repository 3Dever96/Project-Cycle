using UnityEngine;
using UnityEngine.AI;
using ProjectCycle.PlayerControl;

namespace ProjectCycle.EnemyControl
{
    // This class implements a state machine for controlling enemy behavior.
    // It manages transitions between different states (e.g., PatrolState, ChaseState) and handles enemy movement.
    public class EnemyStateMachine : MonoBehaviour
    {
        // Reference to the NavMeshAgent component attached to the enemy GameObject.
        public NavMeshAgent Agent { get; private set; }

        // Reference to the PlayerStateMachine component to track the player's state and position.
        public PlayerStateMachine Player { get; private set; }

        // The current state of the enemy.
        public EnemyState CurrentState { get; private set; }

        // Reference to the enemy's patrol state.
        public EnemyPatrolState PatrolState { get; private set; }

        // Reference to the enemy's chase state.
        public EnemyChaseState ChaseState { get; private set; }

        public EnemyAttackState AttackState { get; private set; }

        // The original position of the enemy, used for returning to the patrol area.
        public Vector3 OriginPoint { get; private set; }

        // Speed at which the enemy moves while patrolling.
        public float walkSpeed;

        // Speed at which the enemy moves while chasing the player.
        public float runSpeed;

        // Distance within which the enemy starts chasing the player.
        public float chaseDistance;

        // Called when the script is initialized.
        private void Start()
        {
            // Initialize the NavMeshAgent component for pathfinding and movement.
            Agent = GetComponent<NavMeshAgent>();

            // Find the PlayerStateMachine in the scene to track the player.
            Player = FindFirstObjectByType<PlayerStateMachine>();

            // Retrieve references to the patrol and chase state components.
            PatrolState = GetComponent<EnemyPatrolState>();
            ChaseState = GetComponent<EnemyChaseState>();
            AttackState = GetComponent<EnemyAttackState>();

            // Store the initial position of the enemy as the origin point.
            OriginPoint = transform.position;

            // Set the initial state of the enemy to PatrolState.
            SetState(PatrolState);
        }

        // Called every fixed frame to update the enemy's state logic.
        private void FixedUpdate()
        {
            // If the player reference is null, attempt to find the PlayerStateMachine in the scene.
            if (Player == null)
            {
                Player = FindFirstObjectByType<PlayerStateMachine>();
            }
            else
            {
                // If there is a current state, update it every fixed frame.
                if (CurrentState != null)
                {
                    CurrentState.UpdateState(this);
                }
            }
        }

        // Method to transition the enemy to a new state.
        public void SetState(EnemyState newState)
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

        public void StartAttack(float attack)
        {
            SetState(AttackState);
            StartCoroutine(AttackState.InitiateAttack(this));
        }
    }
}
