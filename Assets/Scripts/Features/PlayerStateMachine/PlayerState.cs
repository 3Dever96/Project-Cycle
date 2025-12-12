using UnityEngine;
using UnityEngine.InputSystem;
using ProjectCycle.GameSystems;

namespace ProjectCycle.PlayerControl
{
    // This abstract class defines the structure for a player's state in the state machine.
    // It serves as a base class for specific player states (e.g., GroundState, AirState).
    [RequireComponent(typeof(PlayerStateMachine))]
    public abstract class PlayerState : MonoBehaviour
    {
        // Called when the state is enabled.
        public void OnEnable()
        {
            // Subscribe to the input action triggered event from the GameManager's Input system.
            GameManager.instance.Input.onActionTriggered += OnAction;
        }

        // Called when the state is disabled.
        public void OnDisable()
        {
            // Unsubscribe from the input action triggered event to avoid memory leaks.
            GameManager.instance.Input.onActionTriggered -= OnAction;
        }

        // Abstract method to handle input actions.
        // This method must be implemented by derived classes to define specific behavior for input actions.
        protected abstract void OnAction(InputAction.CallbackContext context);

        // Abstract method to define the behavior when the state starts.
        // This method must be implemented by derived classes.
        public abstract void StartState(PlayerStateMachine player);

        // Abstract method to define the behavior during the state's update cycle.
        // This method must be implemented by derived classes.
        public abstract void UpdateState(PlayerStateMachine player);

        // Abstract method to handle state transitions.
        // This method must be implemented by derived classes.
        public abstract void ChangeState(PlayerStateMachine player);

        // Abstract method to define the behavior when exiting the state.
        // This method must be implemented by derived classes.
        public abstract void ExitState(PlayerStateMachine player);
    }
}
