using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Character.Squire
{
    public class SquireNavigationManager : BaseNavMeshNavigator
    {
        //Setup Idle logic to choose a point behind the player 
        //Setup some kind of event driven system, listening for playing ping commands to control the navigation logic of the squire

        void Start()
        {
            
        }


        void Update()
        {
            
        }

        public void NavigateToRaycastHit(RaycastHit destination)
        {
            if (!NavMeshAgent.pathPending && !NavMeshAgent.hasPath)
            {
                NavMeshHit navmeshHit;
                    
                // 2. Sample within a small radius (e.g., 1.0 unit) to find the nearest valid NavMesh spot
                if (NavMesh.SamplePosition(destination.point, out navmeshHit, 1.0f, NavMesh.AllAreas))
                {
                    // 3. Move agent to the validated NavMesh position
                    NavMeshAgent.SetDestination(navmeshHit.position);
                }
            }           
            
        }

    }

}