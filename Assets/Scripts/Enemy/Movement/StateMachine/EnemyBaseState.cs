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
        

        protected void Move(Vector3 startPosition, Vector3 endPosition, float step)
        {
            StateMachine.transform.position = Vector3.MoveTowards(startPosition, endPosition, step);
        }

        protected void ChangeCurrentPoint()
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