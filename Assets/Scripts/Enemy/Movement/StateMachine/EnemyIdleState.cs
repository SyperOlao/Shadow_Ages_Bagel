using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyIdleState: EnemyBaseState
    {
        private float _timer;

        public EnemyIdleState(EnemyStateMachine stateMachine, float timer = 0.0f) : base(stateMachine)
        {
            _timer = timer;
        }

        public override void Enter()
        {
          
        }

        public override void Tick()
        {                 
            _timer -= Time.deltaTime;
            LookAround();
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new EnemyMoveState(StateMachine));
            }
        }

        private void LookAround()
        {
            var transform = StateMachine.transform;
            StateMachine.transform.RotateAround(transform.position, transform.up, 2f*Time.deltaTime);
        }

        public override void Exit()
        {
        }
    }
}