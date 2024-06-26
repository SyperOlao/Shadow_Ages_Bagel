using System;
using System.Collections.Generic;
using DataClasses;
using Enemy.FOV;
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

        [SerializeField] public FieldOfView FieldOfView;
        [SerializeField] public List<PointsWithTimeStandby> Points;
        [SerializeField] public int CurrentPoint;

        private void OnEnable()
        {
            FieldOfView.IsPlayerVisible += SwitchToWarning;
        }

        private void OnDisable()
        {
            FieldOfView.IsPlayerVisible -= SwitchToWarning;
        }


        private void SwitchToWarning()
        {
            if (FieldOfView.CanSeePlayer)
            {
                SwitchState(new EnemyWarningState(this));        
            }
        }

        private void Start()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();

            SwitchState(new EnemyMoveState(this));
        }
    }
}