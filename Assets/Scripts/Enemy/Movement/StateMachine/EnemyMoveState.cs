using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyMoveState : EnemyBaseState
    {
        private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int _moveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;


        public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            StateMachine.Velocity.y = Physics.gravity.y;

            StateMachine.Animator.CrossFadeInFixedTime(_moveBlendTreeHash, CrossFadeDuration);
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
        }
    }
}