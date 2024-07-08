using System;
using UnityEngine;

namespace Enemy.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(1)] private float _maxHealth = 100f;
        public float MaxHealth => _maxHealth;
        public float CurrentHealth { get; private set; }

        public event Action<float> Changed;

        private void Start()
        {
            CurrentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, _maxHealth);
            Changed?.Invoke(damage);
        }
    }
}