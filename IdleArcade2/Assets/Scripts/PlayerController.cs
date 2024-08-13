using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            gameManager.StartTakeIron();
        }

        if (other.gameObject.CompareTag("ShopWood"))
        {
            gameManager.StartTakeWood();
        }

        if (other.gameObject.CompareTag("ForgeSword"))
        {
            gameManager.StartMakeSword();
        }

        if (other.gameObject.CompareTag("Selling"))
        {
            gameManager.StartSelling();
		}

        if (other.gameObject.CompareTag("TrashCan"))
        {
            gameManager.StartTrashCan();
        }

        if (other.gameObject.CompareTag("Upgrade"))
        {
            gameManager.StartUpgrade();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        gameManager.StopAllCoroutines();
    }
}
