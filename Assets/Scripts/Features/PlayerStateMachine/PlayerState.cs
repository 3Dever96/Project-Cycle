using UnityEngine;
using UnityEngine.InputSystem;
using ProjectCycle.GameSystems;

namespace ProjectCycle.PlayerControl
{
	[RequireComponent(typeof(PlayerStateMachine))]
	public abstract class PlayerState : MonoBehaviour
	{
		public void OnEnable()
		{	
			GameManager.instance.Input.onActionTriggered += OnAction;
		}
		
		public void OnDisable()
		{	
			GameManager.instance.Input.onActionTriggered -= OnAction;
		}
		
		protected abstract void OnAction(InputAction.CallbackContext context);
		
		public abstract void StartState(PlayerStateMachine player);
		
		public abstract void UpdateState(PlayerStateMachine player);
		
		public abstract void ChangeState(PlayerStateMachine player);
		
		public abstract void ExitState(PlayerStateMachine player);
	}
}