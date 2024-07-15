using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;


namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        public Camera Camera;
        [SerializeField] private int poolMaxSize = 10;
        [SerializeField] private int poolCapacity = 2;
        [SerializeField] private Canvas healthBarCanvas;


        private NavMeshTriangulation _triangulation;
        private ObjectPool<GameObject> _pool;

        private void Awake()
        {
            _triangulation = NavMesh.CalculateTriangulation();
            _pool = new ObjectPool<GameObject>(
                createFunc: OnCreate,
                actionOnGet: ActionOnGet,
                actionOnRelease: obj => obj.SetActive(false),
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: poolCapacity,
                maxSize: poolMaxSize
            );
        }

        private GameObject OnCreate()
        {
            var obj = Instantiate(prefab, ChooseRandomPointOnNavMesh(_triangulation), Quaternion.identity);
            var navMeshAgent = obj.GetComponent<NavMeshAgent>();
            navMeshAgent.GetComponent<Enemy.Enemy>().SetupHealthBar(healthBarCanvas, Camera);

            navMeshAgent.enabled = true; 
            return obj;
        }

        private static Vector3 ChooseRandomPointOnNavMesh(NavMeshTriangulation triangulation)
        {
            var firstVertex = Random.Range(0, triangulation.vertices.Length);
            var secondVertex = Random.Range(0, triangulation.vertices.Length);

            return Vector3.Lerp(triangulation.vertices[firstVertex], triangulation.vertices[secondVertex],
                Random.value);
        }

        private IEnumerator SpawnAgents()
        {
            var wait = new WaitForSeconds(1f / poolCapacity);
            for (var i = 0; i < poolMaxSize; i++)
            {
                _pool.Get();
                yield return wait;
            }
        }

        private void ActionOnGet(GameObject obj)
        {
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.SetActive(true);
        }

        private void Start()
        {
            StartCoroutine(SpawnAgents());
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