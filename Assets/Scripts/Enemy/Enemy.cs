using System;
using Enemy.FOV;
using Enemy.Health.FaceCamera;
using Enemy.Movement.StateMachine;
using Interfaces;
using Spawner;
using UI.Battle;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(FieldOfView))]
    [RequireComponent(typeof(EnemyStateMachine))]
    public class Enemy: MonoBehaviour, IDamageable
    {
        [SerializeField] private Health.Health health;
        [SerializeField] private HealthProgressBar healthBar;
        public event Action<GameObject> Die;

        public void OnTakeDamage(float damage)
        {
            health.TakeDamage(damage);

            if (health.CurrentHealth < 0)
            {
                Die?.Invoke(gameObject);
            }
        }
        
        
        private void OnDied()
        {
            
            // Destroy(gameObject, 1f);
            // Destroy(healthBar.gameObject, 1f);
        }
        
        public void SetupHealthBar(Canvas canvas, Camera camera1)
        {
            healthBar.transform.SetParent(canvas.transform);
            if (healthBar.TryGetComponent(out FaceCamera faceCamera))
            {
                faceCamera.Camera = camera1;
            }
        }
    }
}