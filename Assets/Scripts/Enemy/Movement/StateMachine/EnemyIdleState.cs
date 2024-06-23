using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    public class EnemyIdleState: EnemyBaseState
    {
        private float _timer;
        private const int TimeStep = 1;

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
            if (_timer <= 0)
            {
                StateMachine.SwitchState(new EnemyMoveState(StateMachine));
            }
        }

        public override void Exit()
        {
        }
    }
}