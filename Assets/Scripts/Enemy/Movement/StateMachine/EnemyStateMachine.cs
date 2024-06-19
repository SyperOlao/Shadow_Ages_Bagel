using System.Collections.Generic;
using DataClasses;
using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    [RequireComponent(typeof(Animator))]
    public class EnemyStateMachine : Common.StateMachine.StateMachine
    {
        public Vector3 Velocity;

        public float MovementSpeed { get; private set; } = 5f;
        public Animator Animator { get; private set; }
        
        [SerializeField] public Transform EnemyPosition;
        [SerializeField] public List<PointsWithTimeStandby> Points;
        [SerializeField] public float PointOfView;

        private void Start()
        {
            Animator = GetComponent<Animator>();

            SwitchState(new EnemyMoveState(this));
        }
    }
}