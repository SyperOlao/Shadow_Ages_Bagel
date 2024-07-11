using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;


namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _transform;
        [SerializeField] private int _poolMaxSize = 10;
        [SerializeField] private int _poolCapacity = 2;
        [SerializeField] private Canvas HealthBarCanvas;
        
        
        private NavMeshTriangulation Triangulation;
        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(
                createFunc: OnCreate,
                actionOnGet: ActionOnGet,
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        private GameObject OnCreate()
        {
            
            return Instantiate(_prefab);
            StartCoroutine(SpawnAgents());
       
        }
        
        private static Vector3 ChooseRandomPointOnNavMesh(NavMeshTriangulation triangulation)
        {
            var firstVertex = Random.Range(0, triangulation.vertices.Length);
            var secondVertex = Random.Range(0, triangulation.vertices.Length);

            return Vector3.Lerp(triangulation.vertices[firstVertex], triangulation.vertices[secondVertex], Random.value);
        }

        private IEnumerator SpawnAgents()
        {
            var wait = new WaitForSeconds(1f / _poolCapacity);
            for (var i = 0; i < _poolMaxSize; i++)
            {
                var agent = Instantiate(_prefab, ChooseRandomPointOnNavMesh(Triangulation), Quaternion.identity);
              //  agent.GetComponent<RandomPositionMover>().Triangulation = Triangulation;
                agent.GetComponent<Enemy.Enemy>().SetupHealthBar(HealthBarCanvas, _camera);
                
                yield return wait;
            }
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