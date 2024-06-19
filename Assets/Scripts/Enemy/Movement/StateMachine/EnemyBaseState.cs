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

        protected void CalculateMoveDirection()
        {
            var step = StateMachine.MovementSpeed * Time.deltaTime;
            var targetPosition = StateMachine.Points[StateMachine.CurrentPoint].point;
            var stateMachineTransform = StateMachine.transform;
            StateMachine.transform.position = Vector3.MoveTowards(stateMachineTransform.position, targetPosition, step);


            if (Vector3.Distance(stateMachineTransform.position, targetPosition) < 0.01f)
            {
                ChangeCurrentPoint();
            }
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

        protected void FaceMoveDirection()
        {
            Vector3 faceDirection = new(StateMachine.Velocity.x, 0f, StateMachine.Velocity.z);

            if (faceDirection == Vector3.zero)
                return;

            StateMachine.transform.rotation = Quaternion.Slerp(StateMachine.transform.rotation,
                Quaternion.LookRotation(faceDirection), StateMachine.LookRotationDampFactor * Time.deltaTime);
        }

        protected void Move()
        {
            StateMachine.Rigidbody.AddForce(StateMachine.Velocity * Time.deltaTime);
        }
    }
}