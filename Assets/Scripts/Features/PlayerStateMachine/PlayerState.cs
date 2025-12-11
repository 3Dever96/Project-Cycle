using UnityEngine;
using UnityEngine.InputSystem;
using ProjectCycle.GameSystems;

namespace ProjectCycle.PlayerControl
{
	public class PlayerState : MonoBehavior
	{
		public void OnEnable()
		{	GameManager.instance.Input.OnActionCallback += OnAction;
		}
		
		public void OnDisable()
		{	GameManager.instance.Input.OnActionCallback -= OnAction;
		}
		
		protected abstract void OnAction(InputAction.CallbackContext context);
		
		public abstract void StartState(PlayerStateMachine player);
		
		public abstract void UpdateState(PlayerStateMachine player);
		
		public abstract void ChangeState(PlayerStateMachine player);
		
		public abstract void ExitState(PlayerStateMachine player);
	}
}