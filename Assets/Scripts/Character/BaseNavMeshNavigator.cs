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

        /// <summary>
        /// Attempts to navigate to the specified position on the NavMesh.
        /// </summary>
        /// <param name="position">The target position to navigate to.</param>
        /// <returns>True if the navigation was successful, false otherwise.</returns>
        protected bool AttemptToNavigateWithSamplePosition(Vector3 position)
        {
            NavMeshHit navmeshHit;
            if (NavMesh.SamplePosition(position, out navmeshHit, 1.0f, NavMesh.AllAreas))
            {
                NavMeshAgent.SetDestination(navmeshHit.position);
                return true;
            }

            return false;
        }
    }
}