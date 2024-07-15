using Enemy.FOV;
using Enemy.Health.FaceCamera;
using Enemy.Movement.StateMachine;
using Interfaces;
using UI.Battle;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(FieldOfView))]
    [RequireComponent(typeof(EnemyStateMachine))]
    public class Enemy: MonoBehaviour, IDamageable
    {
        [SerializeField] private Health.Health _health;
        [SerializeField] private HealthProgressBar _healthBar;
        

        public void OnTakeDamage(float damage)
        {
            _health.TakeDamage(damage);
        
            if (_health.CurrentHealth < 0)
            {
                OnDied();
            }
        }
        
        
        private void OnDied()
        {
            Destroy(gameObject, 1f);
            Destroy(_healthBar.gameObject, 1f);
        }
        
        public void SetupHealthBar(Canvas canvas, Camera Camera)
        {
            _healthBar.transform.SetParent(canvas.transform);
            if (_healthBar.TryGetComponent(out FaceCamera faceCamera))
            {
                faceCamera.Camera = Camera;
            }
        }
    }
}