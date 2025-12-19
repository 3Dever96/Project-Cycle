using UnityEngine;
using UnityEngine.InputSystem;
using ProjectCycle.PlayerControl;
using ProjectCycle.GameSystems;

namespace ProjectCycle.Combat
{
    public class PlayerCombatController : MonoBehaviour
    {
        PlayerStateMachine player;

        private void Start()
        {
            player = GetComponent<PlayerStateMachine>();
        }

        private void OnEnable()
        {
            GameManager.instance.Input.onActionTriggered += OnAction;
        }

        private void OnDisable()
        {
            GameManager.instance.Input.onActionTriggered -= OnAction;
        }

        private void OnAction(InputAction.CallbackContext context)
        {
            if (GameManager.instance.gameState == GameState.Play)
            {
                if (context.action.name == "Attack")
                {
                    if (context.performed)
                    {
                        player.SetState(player.AttackState);
                        StartCoroutine(player.AttackState.Attack(player));
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (player.CurrentState.GetType() == typeof(PlayerAttackState))
            {
                EnemyStats enemy = other.GetComponentInParent<EnemyStats>();

                if (enemy != null)
                {
                    enemy.TakeDamage(GameManager.instance.PlayerManager.atk);
                }
            }
        }
    }
}
