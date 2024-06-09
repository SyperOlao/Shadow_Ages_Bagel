using UnityEngine;

namespace Player.Movement.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;
        
        public void SwitchState(State state)
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