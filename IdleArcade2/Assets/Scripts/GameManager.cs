using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum StateHand
{
    Nonee,
    Sword1,
    Sword2,
    Sword3,
    Shield1
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private StateHand handState;
    private OrderManager orderManager;
    private int coin = 0;
    private int coinForWeapon;
    private int coinForLevel;

    [SerializeField] private Transform handTransform;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("GameObject")]
    [SerializeField] private GameObject shopWood;
    [SerializeField] private GameObject forge1;
    [SerializeField] private GameObject forge2;
    [SerializeField] private GameObject forge3;
    [SerializeField] private GameObject forge4;
    [SerializeField] private GameObject upgrade;

    [Header("Resource")]
    [SerializeField] private GameObject ironPrefab;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private GameObject swordPrefab2;
    [SerializeField] private GameObject swordPrefab3;
    [SerializeField] private GameObject shieldPrefab;
    //[SerializeField] private Dictionary<int, GameObject> listResource = new Dictionary<int, GameObject>();
    [SerializeField] private List<Rescouce> listResource = new List<Rescouce>();

    private void Awake()
    {
        orderManager = GetComponentInChildren<OrderManager>();

        Instance = this;
    }

    private void Start()
    {
        //GameObject npc = Instantiate(npcPrefab, npcTransform);
        //      npc.GetComponent<NPCController>().GoToBuyWeapon(pointNPC);
        //      npc.GetComponent<NPCController>().actionSwitchStateOrder = () => { orderManager.SwitchStateOrder(StateOrder.Sword1); };
        //npcList.Add(npc);

        coinForLevel = 60;
        orderManager.SetLevel();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Debug.Log("F");
        //    //GetComponentInChildren<NPCManager>().StartGoHomeNPC();
        //    Selling();
        //}
    }

    public void StartTakeIron(WaitingBar waiting)
    {
        StartCoroutine(TakeIron(waiting));
    }

    public void StartMakeSword(WaitingBar waiting)
    {
        StartCoroutine(MakeSword(waiting));
    }

    public void StartSelling(WaitingBar waiting)
    {
        StartCoroutine(Selling(waiting));
    }

    public void StartTakeWood(WaitingBar waiting)
    {
        StartCoroutine(TakeWood(waiting));
    }

    public void StartTrashCan(WaitingBar waiting)
    {
        StartCoroutine(TrashCan(waiting));
    }

    public void StartUpgrade()
    {
        StartCoroutine(Upgrade());
    }

    private IEnumerator Selling(WaitingBar waiting)
    {
        if (orderManager.orderState.ToString() == handState.ToString())
        {
            waiting.StartSliderWaiting(1f);

            yield return new WaitForSeconds(1f);

            Debug.Log("OrderState: " + orderManager.orderState.ToString());
            Debug.Log("HandState: " + handState.ToString());

            orderManager.SwitchStateOrder(StateOrder.None);
            SwitchHandState(StateHand.Nonee);

            coin += coinForWeapon;
            coinText.text = coin.ToString();

            GetComponentInChildren<NPCManager>().StartGoHomeNPC();
            //         npcList[0].GetComponent<NPCController>().GoHome();
            //         npcList.Remove(npcList[0]);

            //GameObject npc = Instantiate(npcPrefab, npcTransform);
            //npc.GetComponent<NPCController>().GoToBuyWeapon(pointNPC);
            //npc.GetComponent<NPCController>().actionSwitchStateOrder = () => { orderManager.RandomOrder(); };

            if (orderManager.GetLevel() < 4 && coin >= coinForLevel)
            {
                upgrade.SetActive(true);
            }

            //npcList.Add(npc);
        }
    }

    private IEnumerator TakeIron(WaitingBar waiting)
    {
        int i = listResource.Count;
        while (listResource.Count < 3 && handState == StateHand.Nonee)
        {

            waiting.StartSliderWaiting(2f);

            yield return new WaitForSeconds(2f);
            var iron = Instantiate(ironPrefab, handTransform);
            listResource.Add(new Rescouce() { Index = 1, Object = iron });

            if (listResource.Count > 1)
            {
                GameObject firstIron = listResource[i - 1].Object;
                GameObject secondIron = listResource[i].Object;
                secondIron.transform.position = new Vector3(firstIron.transform.position.x, firstIron.transform.position.y + 0.21f, firstIron.transform.position.z);
            }
            i++;
            Debug.Log(listResource.Count);
        }
    }

    private IEnumerator TakeWood(WaitingBar waiting)
    {
        int i = listResource.Count;
        while (listResource.Count < 3 && handState == StateHand.Nonee)
        {

            waiting.StartSliderWaiting(2f);

            yield return new WaitForSeconds(2f);
            var iron = Instantiate(woodPrefab, handTransform);
            listResource.Add(new Rescouce() { Index = 2, Object = iron });

            if (listResource.Count > 1)
            {
                GameObject firstIron = listResource[i - 1].Object;
                GameObject secondIron = listResource[i].Object;
                secondIron.transform.position = new Vector3(firstIron.transform.position.x, firstIron.transform.position.y + 0.21f, firstIron.transform.position.z);
            }
            i++;
            Debug.Log(listResource.Count);
        }
    }

    private IEnumerator MakeSword(WaitingBar waiting)
    {
        if (listResource.Count == 3)
        {
            waiting.StartSliderWaiting(2f);

            yield return new WaitForSeconds(2f);


            //SwitchHandState(StateHand.Sword);
            switch (CheckRescouceSword())
            {
                case 1:
                    {
                        SwitchHandState(StateHand.Sword1);
                        break;
                    }
                case 2:
                    {
                        SwitchHandState(StateHand.Sword2);
                        break;
                    }
                case 3:
                    {
                        SwitchHandState(StateHand.Sword3);
                        break;
                    }
                case 4:
                    {
                        SwitchHandState(StateHand.Shield1);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    private IEnumerator TrashCan(WaitingBar waiting)
    {
        if (listResource.Count > 0 || handState != StateHand.Nonee)
        {
            waiting.StartSliderWaiting(2f);
            yield return new WaitForSeconds(2f);
            SwitchHandState(StateHand.Nonee);
            ClearIron();
        }
    }

    private IEnumerator Upgrade()
    {
        yield return new WaitForSeconds(2f);

        upgrade.SetActive(false);
        coin -= coinForLevel;
        coinText.text = coin.ToString();

        orderManager.SetLevel();

        Debug.Log("Level: " + orderManager.GetLevel());

        switch (orderManager.GetLevel())
        {
            case 2:
                {
                    orderManager.SwitchStateOrder(StateOrder.Sword2);
                    coinForLevel = 120;
                    forge1.SetActive(false);
                    forge2.SetActive(true);
                    forge3.SetActive(false);
                    forge4.SetActive(false);
                    shopWood.SetActive(true);
                    break;
                }
            case 3:
                {
                    orderManager.SwitchStateOrder(StateOrder.Sword3);
                    coinForLevel = 200;
                    forge1.SetActive(false);
                    forge2.SetActive(false);
                    forge3.SetActive(true);
                    forge4.SetActive(false);
                    break;
                }
            case 4:
                {
                    orderManager.SwitchStateOrder(StateOrder.Shield1);
                    forge1.SetActive(false);
                    forge2.SetActive(false);
                    forge3.SetActive(false);
                    forge4.SetActive(true);
                    break;
                }
        }
    }

    private void ClearIron()
    {
        listResource.Clear();
        foreach (Transform child in handTransform)
        {
            Destroy(child.gameObject);
        }
    }

    private void SwitchHandState(StateHand newState)
    {
        handState = newState;

        switch (handState)
        {
            case StateHand.Nonee:
                {
                    foreach (Transform child in handTransform)
                    {
                        Destroy(child.gameObject);
                    }
                    break;
                }
            case StateHand.Sword1:
                {
                    ClearIron();
                    Instantiate(swordPrefab, handTransform);
                    coinForWeapon = 30;
                    break;
                }
            case StateHand.Sword2:
                {
                    ClearIron();
                    Instantiate(swordPrefab2, handTransform);
                    coinForWeapon = 40;
                    break;
                }
            case StateHand.Sword3:
                {
                    ClearIron();
                    Instantiate(swordPrefab3, handTransform);
                    coinForWeapon = 50;
                    break;
                }
            case StateHand.Shield1:
                {
                    ClearIron();
                    Instantiate(shieldPrefab, handTransform);
                    coinForWeapon = 60;
                    break;
                }
        }
    }

    private int CheckRescouceSword()
    {
        int countIron = 0;
        int countWood = 0;
        foreach (Rescouce index in listResource)
        {
            if (index.Index == 1)
            {
                countIron++;
            }

            if (index.Index == 2)
            {
                countWood++;
            }
        }

        if (countIron == 2 && countWood == 1)
        {
            return 2;
        }
        else if (countIron == 3 && countWood == 0)
        {
            return 1;
        }
        else if (countIron == 1 && countWood == 2)
        {
            return 3;
        }
        else if (countIron == 0 && countWood == 3)
        {
            return 4;
        }

        return -1;
    }
}

public class Rescouce
{
    public int Index;
    public GameObject Object;
}
