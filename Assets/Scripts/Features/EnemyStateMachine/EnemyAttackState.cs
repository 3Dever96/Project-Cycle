using System.Collections;
using UnityEngine;

namespace ProjectCycle.EnemyControl
{
    public class EnemyAttackState : EnemyState
    {
        [SerializeField] GameObject hurtBox;

        public override void StartState(EnemyStateMachine enemy)
        {
            hurtBox.SetActive(false);
            enemy.Agent.isStopped = true;
            enemy.Agent.SetDestination(transform.position);
        }

        public override void UpdateState(EnemyStateMachine enemy)
        {
            
        }

        public override void ChangeState(EnemyStateMachine enemy)
        {
            
        }

        public override void ExitState(EnemyStateMachine enemy)
        {
            hurtBox.SetActive(false);
            enemy.Agent.isStopped = false;
        }

        public IEnumerator InitiateAttack(EnemyStateMachine enemy)
        {
            if (!hurtBox.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.5f);
                hurtBox.SetActive(true);
                yield return new WaitForSeconds(1f);
                hurtBox.SetActive(false);
                enemy.SetState(enemy.ChaseState);
            }
        }
    }
}
