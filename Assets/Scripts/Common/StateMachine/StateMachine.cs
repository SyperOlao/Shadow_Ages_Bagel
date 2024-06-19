using Player.Movement.StateMachine;
using UnityEngine;

namespace Common.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

        protected void SwitchState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        private void Update()
        {
            _currentState?.Tick();
        }
        
    }
}