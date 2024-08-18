using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float moveSpeed;
    [SerializeField] private bl_Joystick joystick;
    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);

            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ShopIron"))
        {
            var w = other.gameObject.GetComponentInChildren<Canvas>();
            if (w != null)
            {
                w.transform.GetChild(0).gameObject.SetActive(true);
                WaitingBar waiting = other.gameObject.GetComponentInChildren<WaitingBar>();
                if (waiting != null)
                {
                    gameManager.StartTakeIron(waiting);
                }
            }
        }

        if (other.gameObject.CompareTag("ShopWood"))
        {
            var w = other.gameObject.GetComponentInChildren<Canvas>();
            if (w != null)
            {
                w.transform.GetChild(0).gameObject.SetActive(true);
                WaitingBar waiting = other.gameObject.GetComponentInChildren<WaitingBar>();
                if (waiting != null)
                {
                    gameManager.StartTakeWood(waiting);
                }
            }
        }

        if (other.gameObject.CompareTag("ForgeSword"))
        {
            var w = other.gameObject.GetComponentInChildren<Canvas>();
            if (w != null)
            {
                w.transform.GetChild(0).gameObject.SetActive(true);
                WaitingBar waiting = other.gameObject.GetComponentInChildren<WaitingBar>();
                if (waiting != null)
                {
                    gameManager.StartMakeSword(waiting);
                }
            }
        }

        if (other.gameObject.CompareTag("Selling"))
        {
            var w = other.gameObject.GetComponentInChildren<Canvas>();
            if (w != null)
            {
                w.transform.GetChild(0).gameObject.SetActive(true);
                WaitingBar waiting = other.gameObject.GetComponentInChildren<WaitingBar>();
                if (waiting != null)
                {
                    gameManager.StartSelling(waiting);
                }
            }
        }

        if (other.gameObject.CompareTag("TrashCan"))
        {
            var w = other.gameObject.GetComponentInChildren<Canvas>();
            if (w != null)
            {
                w.transform.GetChild(0).gameObject.SetActive(true);
                WaitingBar waiting = other.gameObject.GetComponentInChildren<WaitingBar>();
                if (waiting != null)
                {
                    gameManager.StartTrashCan(waiting);
                }
            }
        }

        if (other.gameObject.CompareTag("Upgrade"))
        {
            gameManager.StartUpgrade();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");

        var w = other.gameObject.GetComponentInChildren<Canvas>();
        if (w != null)
        { w.transform.GetChild(0).gameObject.SetActive(false); }

        gameManager.StopAllCoroutines();
    }
}
