using UnityEngine;

namespace ProjectCycle.EnemyControl
{
    // This class represents the enemy's patrol state in the state machine.
    // It handles the logic for the enemy to roam around a specified area and transition to other states.
    public class EnemyPatrolState : EnemyState
    {
        // The current destination the enemy is moving towards during patrol.
        private Vector3 destination;

        // The maximum distance the enemy can roam from its origin point.
        [SerializeField] private float roamDistance;

        // The maximum time the enemy waits before moving to a new destination.
        [SerializeField] private float waitTime;

        // The current wait time before the enemy starts moving again.
        private float currentWaitTime;

        // Flag to indicate whether the enemy is currently moving.
        private bool isMoving;

        // Called when the patrol state starts.
        public override void StartState(EnemyStateMachine enemy)
        {
            // Set the enemy's movement speed to the walking speed.
            enemy.Agent.speed = enemy.walkSpeed;

            // Randomize the initial wait time before the enemy starts moving.
            currentWaitTime = Random.Range(0, waitTime);

            // Set the moving flag to false, indicating the enemy is initially idle.
            isMoving = false;
        }

        // Called every frame to update the patrol state logic.
        public override void UpdateState(EnemyStateMachine enemy)
        {
            if (!isMoving)
            {
                // If the enemy is not moving, decrease the wait time.
                currentWaitTime -= Time.deltaTime;

                // When the wait time reaches zero, calculate a new random destination and start moving.
                if (currentWaitTime <= 0f)
                {
                    isMoving = true;

                    // Generate random x and z offsets within the roam distance.
                    float x = Random.Range(-roamDistance, roamDistance + 1f);
                    float z = Random.Range(-roamDistance, roamDistance + 1f);

                    // Calculate the new destination based on the origin point and random offsets.
                    destination = enemy.OriginPoint + new Vector3(x, 0f, z);

                    // Set the enemy's NavMeshAgent destination and enable movement.
                    enemy.Agent.destination = destination;
                    enemy.Agent.isStopped = false;
                }
            }
            else
            {
                // If the enemy is moving, reset the wait time for the next idle period.
                currentWaitTime = Random.Range(0, waitTime);

                // Check if the enemy has reached its destination.
                if (Vector3.Distance(transform.position, destination) <= 0.5f)
                {
                    // Stop the enemy and set the moving flag to false.
                    isMoving = false;
                    enemy.Agent.isStopped = true;
                }
            }

            // Check if the state needs to change (e.g., to ChaseState).
            ChangeState(enemy);
        }

        // Handles state transitions based on conditions.
        public override void ChangeState(EnemyStateMachine enemy)
        {
            // Transition to ChaseState if the player is within the chase distance.
            if (Vector3.Distance(transform.position, enemy.Player.transform.position) <= enemy.chaseDistance)
            {
                enemy.SetState(enemy.ChaseState);
            }
        }

        // Called when exiting the patrol state.
        public override void ExitState(EnemyStateMachine enemy)
        {
            // Currently, no specific behavior is defined for exiting the patrol state.
        }
    }
}
