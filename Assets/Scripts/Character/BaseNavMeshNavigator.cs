using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Scripts.Character
{
    public class BaseNavMeshNavigator : MonoBehaviour
    {
        public NavMeshAgent NavMeshAgent;
        public Animator animator;
        private bool isNavigating = false;

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

            if (isNavigating)
            {
                CheckIfNavigationIsComplete();
            }
        }

        /// <summary>
        /// Attempts to navigate to the specified position on the NavMesh.
        /// </summary>
        /// <param name="position">The target position to navigate to.</param>
        /// <returns>True if the navigation was successful, false otherwise.</returns>
        protected bool AttemptToNavigateWithSamplePosition(Vector3 position, float sampleDistance = 1.0f)
        {
            NavMeshHit navmeshHit;
            if (NavMesh.SamplePosition(position, out navmeshHit, sampleDistance, NavMesh.AllAreas))
            {
                NavMeshAgent.SetDestination(navmeshHit.position);
                isNavigating = true;
                return true;
            }

            return false;
        }

        protected void SetNavigationTargetToCurrentLocation()
        {
            NavMeshAgent.SetDestination(transform.position);
        }

        void CheckIfNavigationIsComplete()
        {
            if (!NavMeshAgent.pathPending && !NavMeshAgent.hasPath)
            {
                isNavigating = false;
                OnNavigationComplete();
            }
        }

        protected virtual void OnNavigationComplete()
        {
            // Override this method to handle navigation completion logic.
        }
    }
}