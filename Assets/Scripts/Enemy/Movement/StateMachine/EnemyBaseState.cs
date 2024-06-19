using Common.StateMachine;

namespace Enemy.Movement.StateMachine
{
    public abstract class EnemyBaseState: State
    {
        protected readonly EnemyStateMachine StateMachine;

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}