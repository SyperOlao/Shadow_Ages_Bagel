using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class PlayerMoveState: PlayerBaseState
    {
        private readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
        private readonly int MoveBlendTreeHash = Animator.StringToHash("MoveBlendTree");
        private const float AnimationDampTime = 0.1f;
        private const float CrossFadeDuration = 0.1f;

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            StateMachine.Velocity.y = Physics.gravity.y;

            StateMachine.Animator.CrossFadeInFixedTime(MoveBlendTreeHash, CrossFadeDuration);
            
        }

        public override void Tick()
        {
            CalculateMoveDirection();
            FaceMoveDirection();
            Move();

            StateMachine.Animator.SetFloat(MoveSpeedHash, StateMachine.InputReader.MoveComposite.sqrMagnitude > 0f ? 1f : 0f, AnimationDampTime, Time.deltaTime);
        }

        public override void Exit()
        {
            
        }
        
    }
}