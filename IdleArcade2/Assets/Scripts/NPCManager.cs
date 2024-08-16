
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("NPC Manager")]
    //[SerializeField] private Transform pointNPC;
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private Transform npcTransform;
    [SerializeField] private Transform homeTransform;
    //[SerializeField] private List<GameObject> npcList;
    //[SerializeField] public List<Transform> queueNPC;
    //[SerializeField] public List<GameObject> checkQueueNPC;

    [Header("Queue Point")]
    [SerializeField] private List<Transform> queuePoints;
    public List<GameObject> npcQueues;

    private int countQueue;

    //private int count = 0;

    private void Start()
    {
        //StartCoroutine(SpawnNPC());
        StartSpawnNPC();

        countQueue = npcQueues.Count;
    }

    private void Update()
    {
        //Debug.Log(countQueue);
        if (npcQueues.Count > countQueue)
        {
            countQueue++;
            StartSpawnNPC();
        }
    }

    public void StartSpawnNPC()
    {
        //StartCoroutine(SpawnNPC());
        //Debug.Log("StartSpawnNPC");
        if (GetQueuePoint(npcQueues.Count) != null)
        {
            GameObject npc = Instantiate(npcPrefab, npcTransform.position, Quaternion.identity);
            npc.GetComponent<NPCController>().GoToBuyWeapon(GetQueuePoint(npcQueues.Count));
            npc.name = countQueue.ToString();
            //npc.GetComponent<NPCController>().SwitchStateNPC(StateNPC.Go);
        }
    }

    public void StartGoHomeNPC()
    {
        npcQueues[0].GetComponent<NPCController>().GoHome(homeTransform);
        npcQueues.RemoveAt(0);

        if (countQueue > 0)
        {
            countQueue--;
        }

        if (npcQueues.Count != 0)
        {
            foreach (GameObject npc in npcQueues)
            {
                npc.GetComponent<NPCController>().StepForward(queuePoints[npcQueues.IndexOf(npc)]);
            }

            //StartSpawnNPC();
        }

        if (countQueue >= 3)
        {
            countQueue = 2;
        }
        //Debug.Log("StartSpawnNPC");
    }

    //public Transform HomeTransform()
    //{
    //    return homeTransform;
    //}

    public Transform GetQueuePoint(int index)
    {
        if (index >= 4)
        {
            return null;
        }
        return queuePoints[index];
    }

    public int GetQueueIndex(Transform transform)
    {
        return queuePoints.IndexOf(transform);
    }
    ////private IEnumerator SpawnNPC()
    ////{
    ////    //count++;

    ////    //if (count > 1)
    ////    //{
    ////    //    yield return new WaitForSeconds(10f);
    ////    //}
    ////    GameObject npc = Instantiate(npcPrefab, npcTransform);
    ////    npc.GetComponent<NPCController>().GoToBuyWeapon(queuePoints[npcQueues.Count]);
    ////}
}
