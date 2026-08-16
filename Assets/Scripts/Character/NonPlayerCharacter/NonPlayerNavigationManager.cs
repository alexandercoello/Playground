using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

namespace Scripts.Character.NonPlayerCharacter
{
    public class NonPlayerNavigationManager : MonoBehaviour
    {
        public NavigationType NavigationType = NavigationType.Stationary;
        public NavMeshAgent NavMeshAgent;
        public Animator animator;

        public float NavMeshAgentCurrentSpeed
        {
            get {return NavMeshAgent.velocity.magnitude; }
        }

        [Header("Route")]
        public GameObject PatrolRoute;
        private List<Transform> patrolPoints = new List<Transform>();
        private int currentTargetPatrolPoint = 0;


        [Header("Wander")]
        public GameObject WanderArea;
        public float MinRestBetweenWanderTime = 0f;
        public float MaxRestBetweenWanderTime = 15f;
        private bool isResting;



        void Start()
        {
            switch(NavigationType)
            {
                case NavigationType.Stationary:
                    //Do nothing
                    break;
                case NavigationType.Route:
                    InitializeRouteNavigation();
                    break;
                case NavigationType.Wander:
                    InitializeWanderNavigation();
                    break;
                default:
                    break;
            } 
            
        }

        void Update()
        {

            switch(NavigationType)
            {
                case NavigationType.Stationary:
                    //Do nothing
                    break;
                case NavigationType.Route:
                    NavigateByRoute();
                    break;
                case NavigationType.Wander:
                    NavigateByWander();
                    break;
                default:
                    break;
            }                    
        }

        void FixedUpdate()
        {
            if(!animator.IsUnityNull())
            {
                animator.SetFloat("Speed", NavMeshAgentCurrentSpeed);
            }
        }

        void InitializeRouteNavigation()
        {
            foreach(Transform t in PatrolRoute.transform)
            {
                patrolPoints.Add(t);    
            }
        }

        void NavigateByRoute()
        {
            if(patrolPoints.IsUnityNull())
            {
                Debug.Log("NonPlayer on Route Missing Patrol Points");
                return;
            }

            if(patrolPoints.Count == 0)
                return;

            if (!NavMeshAgent.pathPending && !NavMeshAgent.hasPath) 
            {
                NavMeshAgent.SetDestination(patrolPoints[currentTargetPatrolPoint].transform.position);
                
                if(currentTargetPatrolPoint + 1 < patrolPoints.Count)
                {
                    currentTargetPatrolPoint++;
                }
                else
                {
                    currentTargetPatrolPoint = 0;
                }            
            }
        }

        void InitializeWanderNavigation()
        {
            if(WanderArea.IsUnityNull())
            {
                Debug.Log("NonPlayer WanderArea Missing");
                return;
            }

        }

        Vector3 GetRandomWanderPoint()
        {
            float randomX = WanderArea.transform.position.x + Random.Range(WanderArea.transform.lossyScale.x/2, -WanderArea.transform.lossyScale.x/2);
            float randomY = 0.1f;
            float randomZ = WanderArea.transform.position.z + Random.Range(WanderArea.transform.lossyScale.z/2, -WanderArea.transform.lossyScale.z/2);

            return new Vector3 (randomX, randomY, randomZ);
        }

        void NavigateByWander()
        {
            if (!NavMeshAgent.pathPending && !NavMeshAgent.hasPath)
            {
                NavMeshAgent.SetDestination(GetRandomWanderPoint());
                StartCoroutine(RestBetweenWander());
            }
            
        }
        
        IEnumerator RestBetweenWander()
        {
            isResting = true;
            NavMeshAgent.isStopped = true;
            yield return new WaitForSeconds(Random.Range(MinRestBetweenWanderTime, MaxRestBetweenWanderTime));

            isResting = false;
            NavMeshAgent.isStopped = false;
        }

    }

    public enum NavigationType
    {
        Stationary = 1,
        Route = 2,
        Wander = 3
    }
}