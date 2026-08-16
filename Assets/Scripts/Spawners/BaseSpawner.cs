using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Spawners
{
    public abstract class BaseSpawner : MonoBehaviour
    {
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
            GameObject spawnObject = Instantiate(SpawnObjectPrefab, SpawnPoint.position, SpawnPoint.rotation);
        }
        
    }
}