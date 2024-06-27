using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyAttackState: EnemyBaseState
    {
        public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
    
        }

        public override void Tick()
        {
            MoveToPlayer();
        }
        
        private void MoveToPlayer()
        {
            var step = StateMachine.MovementSpeed * Time.deltaTime * 0.5f;
            var position = StateMachine.transform.position;
            var lastPlayerPosition = StateMachine.FieldOfView.LastPlayerPosition;
            Move(position, lastPlayerPosition,
                step);
            FaceToDirection( lastPlayerPosition, position);
        }

        public override void Exit()
        {
           
        }
    }
}