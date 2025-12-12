using UnityEngine;

namespace ProjectCycle.EnemyControl
{
    public class EnemyPatrolState : EnemyState
    {
        private Vector3 destination;
        [SerializeField] private float roamDistance;
        [SerializeField] private float waitTime;
        private float currentWaitTime;

        private bool isMoving;

        public override void StartState(EnemyStateMachine enemy)
        {
            enemy.Agent.speed = enemy.walkSpeed;

            currentWaitTime = Random.Range(0, waitTime);
            isMoving = false;
        }

        public override void UpdateState(EnemyStateMachine enemy)
        {
            if (!isMoving)
            {
                currentWaitTime -= Time.deltaTime;

                if (currentWaitTime <= 0f)
                {
                    isMoving = true;
                    
                    float x = Random.Range(-roamDistance, roamDistance + 1f);
                    float z = Random.Range(-roamDistance, roamDistance + 1f);

                    destination = enemy.OriginPoint + new Vector3(x, 0f, z);
                    
                    enemy.Agent.destination = destination;
                    enemy.Agent.isStopped = false;
                }
            }
            else
            {
                currentWaitTime = Random.Range(0, waitTime);

                if (Vector3.Distance(transform.position, destination) <= 0.5f)
                {
                    isMoving = false;
                    enemy.Agent.isStopped = true;
                }
            }
        }

        public override void ChangeState(EnemyStateMachine enemy)
        {
            
        }

        public override void ExitState(EnemyStateMachine enemy)
        {
            
        }
    }
}
