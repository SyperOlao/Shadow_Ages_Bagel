using System;
using UnityEngine;

namespace Enemy.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField, Min(1)] private float maxHealth = 100f;
        public float MaxHealth => maxHealth;
        public float CurrentHealth { get; private set; }

        public event Action<float> Changed;

        private void Start()
        {
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, maxHealth);
            Changed?.Invoke(damage);
        }
    }
}