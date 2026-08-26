using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Character
{
    public class BaseNavMeshNavigator : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent;
        public Animator animator;

        public float NavMeshAgentCurrentSpeed
        {
            get {return NavMeshAgent.velocity.magnitude; }
        }

        void FixedUpdate()
        {
            if(!animator.IsUnityNull())
            {
                animator.SetFloat("Speed", NavMeshAgentCurrentSpeed);
            }
        }
    }
}