using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _repeatRate = 1f;
        [SerializeField] private int _poolCapacity = 5;
        [SerializeField] private int _poolMaxSize = 5;

        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: ActionOnGet,
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        private void ActionOnGet(GameObject obj)
        {
            obj.transform.position = _transform.position;
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.SetActive(true);
        }

        private void Start()
        {
            Invoke(nameof(GetObject), 0.0f);
        }

        private void GetObject()
        {
            _pool.Get();
        }

        private void OnTriggerEnter(Collider other)
        {
            _pool.Release(other.gameObject);
        }
    }
}