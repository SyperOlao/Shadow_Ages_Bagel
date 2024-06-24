using System;
using Unity.VisualScripting;
using UnityEngine;
using State = Common.StateMachine.State;

namespace Enemy.Movement.StateMachine
{
    public abstract class EnemyBaseState : State
    {
        protected readonly EnemyStateMachine StateMachine;

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected void Move()
        {
            var step = StateMachine.MovementSpeed * Time.deltaTime;
            if (StateMachine.Points.Count <= 0)
            {
                StateMachine.SwitchState(new EnemyIdleState(StateMachine, float.PositiveInfinity));
                return;
            }
            var targetPosition = StateMachine.Points[StateMachine.CurrentPoint].point;
            var targetTime = StateMachine.Points[StateMachine.CurrentPoint].time;
            var stateMachineTransform = StateMachine.transform;
            StateMachine.transform.position = Vector3.MoveTowards(stateMachineTransform.position, targetPosition, step);


            if (!(Vector3.Distance(stateMachineTransform.position, targetPosition) < StateMachine.PointRadius)) return;
            
            ChangeCurrentPoint();
            StateMachine.SwitchState(new EnemyIdleState(StateMachine, targetTime));
        }

        private void ChangeCurrentPoint()
        {
            var currentPoint = StateMachine.CurrentPoint;
            var length = StateMachine.Points.Count;

            if (currentPoint >= length - 1)
            {
                StateMachine.CurrentPoint = 0;
            }
            else
            {
                StateMachine.CurrentPoint++;
            }
        }

 
    }
}