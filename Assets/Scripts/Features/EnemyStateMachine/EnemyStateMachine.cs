using UnityEngine;
using UnityEngine.AI;
using ProjectCycle.PlayerControl;

namespace ProjectCycle.EnemyControl
{
    public class EnemyStateMachine : MonoBehaviour
    {
        public NavMeshAgent Agent { get; private set; }
        public PlayerStateMachine Player { get; private set; }

        public EnemyState CurrentState { get; private set; }
        public EnemyPatrolState PatrolState { get; private set; }

        public Vector3 OriginPoint { get; private set; }

        [Header("Movement Variables")]
        public float walkSpeed;
        public float runSpeed;

        private void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
            Player = FindFirstObjectByType<PlayerStateMachine>();

            PatrolState = GetComponent<EnemyPatrolState>();

            OriginPoint = transform.position;

            SetState(PatrolState);
        }

        private void FixedUpdate()
        {
            if (Player == null)
            {
                Player = FindFirstObjectByType<PlayerStateMachine>();
            }
            else
            {
                if (CurrentState != null)
                {
                    CurrentState.UpdateState(this);
                }
            }
        }

        public void SetState(EnemyState newState)
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
    }
}
