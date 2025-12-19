using UnityEngine;
using ProjectCycle.EnemyControl;
using ProjectCycle.PlayerControl;
using ProjectCycle.GameSystems;

namespace ProjectCycle.Combat
{
    public class EnemyCombatController : MonoBehaviour
    {
        private EnemyStats myStats;
        EnemyStateMachine enemy;
        PlayerStateMachine player;
        [SerializeField] private float attackDistance;

        private void Start()
        {
            myStats = GetComponent<EnemyStats>();
            enemy = GetComponent<EnemyStateMachine>();
            player = FindFirstObjectByType<PlayerStateMachine>();
        }

        private void Update()
        {
            if (GameManager.instance.gameState == GameState.Play)
            {
                if (player != null)
                {
                    if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
                    {
                        if (enemy.CurrentState.GetType() != typeof(EnemyAttackState))
                        {
                            enemy.StartAttack(myStats.atk);
                        }
                    }
                }
                else
                {
                    player = FindFirstObjectByType<PlayerStateMachine>();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerStateMachine newPlayer = other.GetComponent<PlayerStateMachine>();

            if (newPlayer == player)
            {
                GameManager.instance.PlayerManager.TakeDamage(myStats.atk);
            }
        }
    }
}
