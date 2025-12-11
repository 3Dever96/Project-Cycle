using UnityEngine;
using ProjectCycle.GameSystems;

namespace ProjectCycle.PlayerControl
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerStateMachine : MonoBehavior
	{
		public CharacterController Controller { get; private set; }
		
		public PlayerState CurrentState { get; private set; }
		
		private void Start()
		{
			Controller = GetComponent<CharacterController>();
		}
		
		private void FixedUpdate()
		{
			if (CurrentState != null)
			{
				CurrentStare.UpdateState(this);
			}
		}
		
		public void SetState(PlayerState newState)
		{
			if (CurrentState != null)
			{
				CurrentStare.EndState(this);
			}
			
			CurrentState = newState;
			
			if (CurrentState != null)
			{
				CurrentState.StartState(this);
			}
		}
	}
}