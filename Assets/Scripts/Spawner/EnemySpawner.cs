using System;
using UnityEngine;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] private Transform _transform;

        private void Start()
        {
            Instantiate(_enemy, _transform);
        }
    }
}