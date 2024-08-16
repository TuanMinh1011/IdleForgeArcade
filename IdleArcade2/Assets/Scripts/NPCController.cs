using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum StateAnimationNPC
{
    None,
    Going,
    Waiting,
}

public enum StateNPC
{
    Go,
    Wait,
    StepForward,
    GoHome
}

public class NPCController : MonoBehaviour
{
    private NavMeshAgent agent;
    //public Action actionSwitchStateOrder;
    private Animator animator;

    private StateAnimationNPC currentAnimationState;
    private StateNPC currentState;

    private NPCManager npcManager;
    private OrderManager orderManager;
    private Transform pointNPC;

    private int currentPoint = -1;
    private bool checkWaiting;
    //private bool checkSpawn;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        npcManager = GameManager.Instance.GetComponentInChildren<NPCManager>();
        orderManager = GameManager.Instance.GetComponentInChildren<OrderManager>();

        currentPoint = -1;
    }

    //private void Start()
    //{
    //    SwitchStateNPC(StateNPC.None);
    //}

    //private void SwitchStateAnimationNPC(StateAnimationNPC newState)
    //{
    //    currentAnimationState = newState;

    //    switch (currentAnimationState)
    //    {
    //        case StateAnimationNPC.None:
    //            {
    //                break;
    //            }
    //        case StateAnimationNPC.Going:
    //            {
    //                animator.SetBool("Run", true);
    //                break;
    //            }
    //        case StateAnimationNPC.Waiting:
    //            {
    //                currentPoint = npcManager.npcQueues.IndexOf(transform.gameObject);
    //                checkWaiting = true;
    //                animator.SetBool("Run", false);
    //                break;
    //            }
    //    }
    //}

    public void SwitchStateNPC(StateNPC state)
    {
        currentState = state;

        switch (currentState)
        {
            case StateNPC.Go:
                {
                    animator.SetBool("Run", true);

                    // Check lại vị trí lúc đang chạy
                    if (npcManager.GetQueueIndex(pointNPC) != npcManager.npcQueues.Count)
                    {
                        Debug.Log("currentPoint: " + npcManager.GetQueueIndex(pointNPC) + " " + npcManager.npcQueues.Count);
                        if (npcManager.GetQueuePoint(npcManager.npcQueues.Count) != null)
                        {
                            GoToBuyWeapon(npcManager.GetQueuePoint(npcManager.npcQueues.Count));
                        }
                    }

                    //Chạy tới chỗ mua weapon thì dừng lại
                    if (agent.remainingDistance <= agent.stoppingDistance && transform.rotation.y != 0)
                    {
                        if ((int)npcManager.GetQueuePoint(npcManager.npcQueues.Count).position.x == (int)this.transform.position.x)
                        {
                            transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));

                            if (!checkWaiting)
                            {
                                npcManager.npcQueues.Add(transform.gameObject);

                                //if (npcManager.npcQueues.Count <= 4 && !checkSpawn)
                                //{
                                //    Debug.Log("Den noi SpawnNPC");
                                //    npcManager.StartSpawnNPC();
                                //    checkSpawn = true;
                                //}
                                //else
                                //{
                                //    //GoHome(npcManager.HomeTransform());
                                //}
                                SwitchStateNPC(StateNPC.Wait);
                            }
                        }

                        Debug.Log("GetQueueIndex: " + npcManager.GetQueueIndex(this.pointNPC));
                        if (npcManager.GetQueueIndex(this.pointNPC) == 0)
                        {
                            Debug.Log("Dat hang ne");
                            orderManager.RandomOrder();
                        }

                    }

                    break;
                }
            case StateNPC.StepForward:
                {
                    animator.SetBool("Run", true);

                    Debug.Log("HEHE: " + npcManager.GetQueueIndex(this.pointNPC));

                    //if (agent.remainingDistance <= agent.stoppingDistance && transform.rotation.y != 0)
                    //{

                    //    transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));

                    //    //if (!checkWaiting)
                    //    //{
                    //    //    npcManager.npcQueues.Add(transform.gameObject);

                    //    //    //if (npcManager.npcQueues.Count <= 4 && !checkSpawn)
                    //    //    //{
                    //    //    //    Debug.Log("Den noi SpawnNPC");
                    //    //    //    npcManager.StartSpawnNPC();
                    //    //    //    checkSpawn = true;
                    //    //    //}
                    //    //    //else
                    //    //    //{
                    //    //    //    //GoHome(npcManager.HomeTransform());
                    //    //    //}
                    //    //}

                    //    Debug.Log("GetQueueIndex2: " + npcManager.GetQueueIndex(this.pointNPC));
                    //    if (npcManager.GetQueueIndex(this.pointNPC) == 0)
                    //    {
                    //        Debug.Log("Dat hang ne 2");
                    //        orderManager.RandomOrder();
                    //    }
                    //}

                    Debug.Log("GetQueueIndex2: " + npcManager.GetQueueIndex(this.pointNPC));
                    if (npcManager.GetQueueIndex(this.pointNPC) == 0)
                    {
                        Debug.Log("Dat hang ne 2");
                        orderManager.RandomOrder();
                    }

                    SwitchStateNPC(StateNPC.Wait);
                    break;
                }
            case StateNPC.Wait:
                {
                    //SwitchStateAnimationNPC(StateAnimationNPC.Waiting);

                    //if (npcManager.GetQueueIndex(this.transform) == 0)
                    //{
                    //    orderManager.RandomOrder();
                    //}

                    animator.SetBool("Run", false);
                    break;
                }
            case StateNPC.GoHome:
                {
                    if (agent.remainingDistance <= agent.stoppingDistance && transform.rotation.y != 0)
                    {
                        if (npcManager.GetQueueIndex(pointNPC) == -1)
                        {
                            Debug.Log("Go Home " + npcManager.GetQueueIndex(pointNPC));
                            if (agent.remainingDistance <= agent.stoppingDistance)
                            {
                                Destroy(this.gameObject);
                            }
                        }
                        Debug.Log("GoHome");
                    }

                    animator.SetBool("Run", true);
                    break;
                }
        }
    }

    private void Update()
    {
        SwitchStateNPC(currentState);



        //    //if (currentState == StateNPC.GoHome)
        //    //{
        //    //    //if (agent.remainingDistance <= agent.stoppingDistance)
        //    //    //{
        //    //    //    Destroy(this.gameObject);
        //    //    //}
        //    //}
        //    //else if (currentState == StateNPC.Go)
        //    //{
        //    //    if (npcManager.GetQueueIndex(pointNPC) != npcManager.npcQueues.Count)
        //    //    {
        //    //        Debug.Log("currentPoint: " + npcManager.GetQueueIndex(pointNPC) + " " + npcManager.npcQueues.Count);
        //    //        //GoToBuyWeapon(npcManager.GetQueuePoint(npcManager.npcQueues.Count));
        //    //    }
        //    //}

        //    //if (npcManager.npcQueues.IndexOf(transform.gameObject) != currentPoint)
        //    //{
        //    //    //GoToBuyWeapon();
        //    //}
    }

    public void GoToBuyWeapon(Transform pointNPC)
    {
        this.pointNPC = pointNPC;
        agent.SetDestination(pointNPC.position);
        //SwitchStateAnimationNPC(StateAnimationNPC.Going);
        //SwitchStateNPC(StateNPC.Go);
    }

    public void GoHome(Transform homePoint)
    {
        GoToBuyWeapon(homePoint);
        //SwitchStateAnimationNPC(StateAnimationNPC.Going);
        SwitchStateNPC(StateNPC.GoHome);
    }

    public void StepForward(Transform pointNPC)
    {
        if (npcManager.npcQueues.IndexOf(transform.gameObject) != currentPoint)
        {
            GoToBuyWeapon(pointNPC);
            SwitchStateNPC(StateNPC.StepForward);
        }
    }

    //public void GoHome()
    //{
    //    Destroy(this.gameObject);
    //}

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

    // private void Update()
    // {
    //     // Check if we've reached the destination
    //     if (!agent.pathPending && !agent.isStopped)
    //     {
    //         if (agent.remainingDistance <= agent.stoppingDistance)
    //         {
    //             if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
    //             {
    //                 transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 0));
    //                 actionSwitchStateOrder?.Invoke();
    //		agent.Stop();
    //	}
    //}
    //     }
    // }
}
