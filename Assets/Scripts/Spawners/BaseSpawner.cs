using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Scripts.Spawners
{
    public abstract class BaseSpawner : MonoBehaviour
    {
        [Inject]
        private DiContainer container;

        public GameObject SpawnObjectPrefab;
        protected Transform SpawnPoint;
        

        protected virtual void Start()
        {
            SpawnPoint = this.transform;
        }

        void Update()
        {
   
        }

        public void SpawnObject()
        {
            Instantiate(SpawnObjectPrefab, SpawnPoint.position, SpawnPoint.rotation);
        }

        public void SpawnObjectWithDependencies()
        {
            GameObject spawnObject = Instantiate(SpawnObjectPrefab, SpawnPoint.position, SpawnPoint.rotation);
            container.InjectGameObject(spawnObject);
        }
        
    }
}