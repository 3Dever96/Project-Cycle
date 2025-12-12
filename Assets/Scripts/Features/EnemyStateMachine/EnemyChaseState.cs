using UnityEngine;

namespace ProjectCycle.EnemyControl
{
    // This class represents the enemy's chase state in the state machine.
    // It handles the logic for the enemy to chase the player and transition to other states.
    public class EnemyChaseState : EnemyState
    {
        // Called when the chase state starts.
        public override void StartState(EnemyStateMachine enemy)
        {
            // Set the enemy's movement speed to the running speed for chasing the player.
            enemy.Agent.speed = enemy.runSpeed;

            // Ensure the NavMeshAgent is not stopped, allowing the enemy to move.
            enemy.Agent.isStopped = false;
        }

        // Called every frame to update the chase state logic.
        public override void UpdateState(EnemyStateMachine enemy)
        {
            // Check the distance between the enemy and the player.
            if (Vector3.Distance(transform.position, enemy.Player.transform.position) > 1.5f)
            {
                // If the player is farther than 1.5 units, set the player's position as the enemy's destination.
                enemy.Agent.isStopped = false;
                enemy.Agent.destination = enemy.Player.transform.position;
            }
            else
            {
                // If the player is within 1.5 units, stop the enemy's movement.
                enemy.Agent.isStopped = true;
            }

            // Check if the state needs to change (e.g., back to PatrolState).
            ChangeState(enemy);
        }

        // Handles state transitions based on conditions.
        public override void ChangeState(EnemyStateMachine enemy)
        {
            // Transition to PatrolState if the player is outside the chase distance.
            if (Vector3.Distance(transform.position, enemy.Player.transform.position) > enemy.chaseDistance)
            {
                enemy.SetState(enemy.PatrolState);
            }
        }

        // Called when exiting the chase state.
        public override void ExitState(EnemyStateMachine enemy)
        {
            // Stop the enemy's movement when exiting the chase state.
            enemy.Agent.isStopped = true;
        }
    }
}
