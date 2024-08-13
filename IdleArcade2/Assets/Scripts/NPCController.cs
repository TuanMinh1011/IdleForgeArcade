using System;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Action actionSwitchStateOrder;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void GoToBuyWeapon(Transform pointNPC)
    {
        agent.SetDestination(pointNPC.position);
    }

    public void GoHome()
    {
        Destroy(this.gameObject);
    }

    //private void Start()
    //{
    //    //for (int i = 0; i < gameManager.checkQueueNPC.Count; i++)
    //    //{
    //    //    if (gameManager.checkQueueNPC[i] == null)
    //    //    {
    //    //        agent.SetDestination(gameManager.queueNPC[i].position);
    //    //        gameManager.checkQueueNPC[i] = this.gameObject;
    //    //        return;
    //    //    }
    //    //}
    //    agent.SetDestination(gameManager.pointNPC.position);
    //}

    private void Update()
    {
        // Check if we've reached the destination
        if (!agent.pathPending && !agent.isStopped)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));
                    actionSwitchStateOrder?.Invoke();
					agent.Stop();
				}
			}
        }
    }
}
