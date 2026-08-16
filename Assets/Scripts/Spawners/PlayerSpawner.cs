using Unity.VisualScripting;
using UnityEngine;

namespace Scripts.Spawners
{
    public class PlayerSpawner : BaseSpawner
    {

        protected override void Start()
        {
            base.Start();
            SpawnObject();
        }

        void Update()
        {
   
        }

        
        
    }
}