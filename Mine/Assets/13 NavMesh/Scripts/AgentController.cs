using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Myproject
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentController : MonoBehaviour
    {
        public Transform pointer;
        private NavMeshAgent agent;
        private readonly bool isStop;
        public NavMeshAgent ogre;
        private readonly bool isOgreStop;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            // AI에게 특정 지점으로 이동하도록 하는 함수
            agent.SetDestination(pointer.position);
            agent.isStopped = isStop;

            _ = ogre.SetDestination(agent.transform.position);
            ogre.isStopped = isOgreStop;
        }
    }
}
