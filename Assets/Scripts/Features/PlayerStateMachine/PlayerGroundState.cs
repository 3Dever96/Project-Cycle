using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectCycle.PlayerControl
{
    public class PlayerGroundState : PlayerState
    {
        [Header("Ground Speed Variables")]
        [SerializeField] private float maxSpeed;
        [SerializeField] private float accel;
        [SerializeField] private float decel;
        [SerializeField] private float fric;

        [Header("Ground Turn Variables")]
        [SerializeField] private float maxTurnAngle;
        [SerializeField] private float turnSpeed;

        [Header("Vertical Speed Variables")]
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float stickForce;

        private float moveSpeed;

        private Vector3 direction;

        private Vector2 move;
        private bool jump;

        protected override void OnAction(InputAction.CallbackContext context)
        {
            if (context.action.name == "Move")
            {
                move = context.ReadValue<Vector2>();
            }

            if (context.action.name == "Jump")
            {
                if (context.performed)
                {
                    jump = true;
                }
            }
        }

        public override void StartState(PlayerStateMachine player)
        {
            player.Direction = transform.forward;
            player.VerticalSpeed = stickForce;
            jump = false;
        }

        public override void UpdateState(PlayerStateMachine player)
        {
            direction = Camera.main.transform.right * move.x + Camera.main.transform.forward * move.y;
            direction.y = 0f;
            direction = direction.normalized;

            if (move != Vector2.zero)
            {
                if (Vector3.Angle(direction, player.Direction) > maxTurnAngle)
                {
                    if (player.CurrentSpeed > 0f)
                    {
                        player.CurrentSpeed -= decel * Time.deltaTime;
                    }
                    else
                    {
                        player.CurrentSpeed = 0f;
                        player.Direction = direction;
                    }
                }
                else
                {
                    if (player.CurrentSpeed < moveSpeed)
                    {
                        player.CurrentSpeed += accel * Time.deltaTime;
                    }
                    else
                    {
                        player.CurrentSpeed = moveSpeed;
                    }

                    player.Direction = direction;
                }
            }
            else
            {
                if (player.CurrentSpeed > 0f)
                {
                    player.CurrentSpeed -= fric * Time.deltaTime;
                }
                else
                {
                    player.CurrentSpeed = 0f;
                }
            }

            moveSpeed = maxSpeed * move.magnitude;

            if (jump)
            {
                player.VerticalSpeed = jumpSpeed;
            }

            player.FaceDirection(turnSpeed);

            player.MovePlayer();

            ChangeState(player);
        }

        public override void ChangeState(PlayerStateMachine player)
        {
            if (player.VerticalSpeed > 0f || !Physics.CheckSphere(transform.position, player.Controller.radius - 0.1f, LayerMask.GetMask("Solid")))
            {
                player.SetState(player.AirState);
            }
        }

        public override void ExitState(PlayerStateMachine player)
        {
            
        }
    }
}
