using UnityEngine;

namespace ProjectCycle.PlayerControl
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerStateMachine : MonoBehaviour
	{
		public CharacterController Controller { get; private set; }
		
		public PlayerState CurrentState { get; private set; }
		public PlayerGroundState GroundState { get; private set; }
		public PlayerAirState AirState { get; private set; }

		public float CurrentSpeed { get; set; }
		public float VerticalSpeed { get; set; }
		public Vector3 Direction { get; set; }
		public Vector3 Velocity { get; set; }
		
		private void Start()
		{
			Controller = GetComponent<CharacterController>();

			GroundState = GetComponent<PlayerGroundState>();
			AirState = GetComponent<PlayerAirState>();

			SetState(GroundState);
		}
		
		private void FixedUpdate()
		{
			if (CurrentState != null)
			{
				CurrentState.UpdateState(this);
			}
		}
		
		public void SetState(PlayerState newState)
		{
			if (CurrentState != null)
			{
				CurrentState.ExitState(this);
			}
			
			CurrentState = newState;
			
			if (CurrentState != null)
			{
				CurrentState.StartState(this);
			}
		}

		public void MovePlayer()
		{
			Vector3 vel = CurrentSpeed * Direction;
			vel.y = VerticalSpeed;
			Velocity = vel;
			Controller.Move(Velocity * Time.deltaTime);
		}

		public void FaceDirection(float turnSpeed)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Direction), turnSpeed * Time.deltaTime);
		}
	}
}