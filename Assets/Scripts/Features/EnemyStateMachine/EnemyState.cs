using UnityEngine;

namespace ProjectCycle.EnemyControl
{
    public abstract class EnemyState : MonoBehaviour
    {
        public abstract void StartState(EnemyStateMachine enemy);
        public abstract void UpdateState(EnemyStateMachine enemy);
        public abstract void ChangeState(EnemyStateMachine enemy);
        public abstract void ExitState(EnemyStateMachine enemy);
    }
}
