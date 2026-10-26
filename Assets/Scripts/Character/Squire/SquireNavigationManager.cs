using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Character.Squire
{
    public class SquireNavigationManager : BaseNavMeshNavigator
    {
        private Transform playerTransform;
        [SerializeField] private float followDistance = 2.5f;
        [SerializeField] private float destinationUpdateThreshold = 5f;

        private Vector3 lastFollowDestination;
        private Vector3 lastPlayerPosition;

        void Start()
        {
            if (playerTransform == null)
            {
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                if (playerObject != null)
                {
                    playerTransform = playerObject.transform;
                }
            }

            UpdateFollowDestination(true);
        }


        void Update()
        {
            UpdateFollowDestination(false);
        }

        private void UpdateFollowDestination(bool forceUpdate)
        {
            if (playerTransform == null)
            {
                return;
            }

            //Determine an intial Vector3 follow position based on the player's position and forward direction, offset by the follow distance
            //Set the squires destination to the follow position
            //Only update the follow position when the player has moved (ignoring rotation)
            Vector3 followPosition = playerTransform.position - playerTransform.forward * followDistance;

            bool playerHasMoved = playerTransform.position != lastPlayerPosition;

            if (!forceUpdate &&
                (followPosition - lastFollowDestination).sqrMagnitude < destinationUpdateThreshold * destinationUpdateThreshold ||
                !playerHasMoved)
            {
                // If the player hasn't moved significantly and the follow position hasn't changed much, we don't need to update the destination.
                return;
            }

            if(AttemptToNavigateWithSamplePosition(followPosition))
            {
                lastFollowDestination = followPosition;
                lastPlayerPosition = playerTransform.position;
            }

        }

        public void NavigateToRaycastHit(RaycastHit destination)
        {
            //Give the squire a new destination to navigate to, based on the raycast hit from the player ping event
            //The Squire doesn't have to wait to reach the destination, it can be interrupted by a new ping event from the player
            AttemptToNavigateWithSamplePosition(destination.point);
        }

        // public bool TryGetNearestNavMeshPoint(out Vector3 point, Transform target, float searchRadius = 5f)
        // {
        //     point = default;

        //     if (!NavMesh.SamplePosition(
        //             target.position,
        //             out NavMeshHit navMeshHit,
        //             searchRadius,
        //             NavMeshAgent.areaMask))
        //     {
        //         return false;
        //     }

        //     point = navMeshHit.position;
        //     return true;
        // }

        public void NavigateNearObject(Transform target, float searchRadius = 5f)
        {
            AttemptToNavigateWithSamplePosition(target.position, searchRadius);
        }

    }

}