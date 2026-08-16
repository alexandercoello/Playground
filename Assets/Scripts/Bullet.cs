using UnityEngine;

namespace Scripts
{
    public class Bullet : MonoBehaviour 
    {
        public Bullet()
        {
            
        }

        public int Damage;

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
        
    }
}