using Common.Enemy.Enemy_Object;
using UnityEngine;

namespace Explore_System.Object
{
    public sealed class EnemySpawnPoint : MonoBehaviour
    {
        [field: Header("Components")]
        [field: SerializeField] private Transform spawnPoint;

        private EnemyObject _currentEnemyObject;

        private void Awake()
        {
            if (spawnPoint == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(spawnPoint)} cannot be null.");
                gameObject.SetActive(false);
            }
        }

        public void InitializeEnemy(EnemyObject enemyObject)
        {
            if (enemyObject == null)
            {
                Debug.Log($"{gameObject.name} > {GetType().Name} > {nameof(InitializeEnemy)} > {nameof(enemyObject)} cannot be null.");
                return;
            }

            if (_currentEnemyObject != null)
            {
                Debug.Log($"{gameObject.name} is already occupied.");
                return;
            }

            var prefab = enemyObject.gameObject;
            var position = new Vector3(transform.position.x, transform.position.y + (transform.position.y - enemyObject.Root.position.y), transform.position.z);
            var rotation = enemyObject.transform.rotation;
            var parent = transform;
            
            _currentEnemyObject = Instantiate(prefab, position, rotation, parent).GetComponent<EnemyObject>();
        }
    }
}