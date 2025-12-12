using UnityEngine;

namespace ProjectCycle.EnemyControl
{
    // This abstract class defines the structure for an enemy's state in the state machine.
    // It serves as a base class for specific enemy states (e.g., PatrolState, ChaseState).
    public abstract class EnemyState : MonoBehaviour
    {
        // Abstract method to define the behavior when the state starts.
        // This method must be implemented by derived classes.
        public abstract void StartState(EnemyStateMachine enemy);

        // Abstract method to define the behavior during the state's update cycle.
        // This method must be implemented by derived classes.
        public abstract void UpdateState(EnemyStateMachine enemy);

        // Abstract method to handle state transitions.
        // This method must be implemented by derived classes.
        public abstract void ChangeState(EnemyStateMachine enemy);

        // Abstract method to define the behavior when exiting the state.
        // This method must be implemented by derived classes.
        public abstract void ExitState(EnemyStateMachine enemy);
    }
}
