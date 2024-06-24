using System.Collections.Generic;
using DataClasses;
using UnityEngine;

namespace Enemy.Movement.StateMachine
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyStateMachine : Common.StateMachine.StateMachine
    {
        public Vector3 Velocity;

        public float PointRadius { get; set; } = 0.01f;
        
        public float LookRotationDampFactor { get; private set; } = 10f;

        public float MovementSpeed { get; private set; } = 5f;
        public Animator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }


        [SerializeField] public Transform Transform;
        [SerializeField] public List<PointsWithTimeStandby> Points;
        [SerializeField] public float PointOfView;


        [SerializeField] public int CurrentPoint;

        private void Start()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();

            SwitchState(new EnemyMoveState(this));
        }
    }
}