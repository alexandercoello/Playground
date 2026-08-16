using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Spawners
{
    public class NonPlayerSpawner : BaseSpawner
    {

        protected override void Start()
        {
            base.Start();
            //SpawnPoint = this.transform;
            SpawnObject();
        }

        void Update()
        {
   
        }

        
        
    }
}