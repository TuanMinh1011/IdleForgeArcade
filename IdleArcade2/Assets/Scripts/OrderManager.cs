using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StateOrder
{
	None,
	Sword1,
	Sword2,
	Sword3,
	Shield1
}

public class OrderManager : MonoBehaviour
{
	public StateOrder orderState;
	private int level;

	[Header("Image")]
	[SerializeField] private Image imageOrder;
	[SerializeField] private Sprite sword1Sprite;
	[SerializeField] private Sprite sword2Sprite;
	[SerializeField] private Sprite sword3Sprite;
	[SerializeField] private Sprite shield1Sprite;

	public void RandomOrder()
	{
		int random = Random.Range(0, level);
		//Debug.Log(random);
		if (random == 0)
		{
			SwitchStateOrder(StateOrder.Sword1);
		}
        else if (random == 1)
        {
			SwitchStateOrder(StateOrder.Sword2);
        }
		else if (random == 2)
		{
			SwitchStateOrder(StateOrder.Sword3);
		}
		else if (random == 3)
		{
			SwitchStateOrder (StateOrder.Shield1);
		}
    }

	public void SwitchStateOrder(StateOrder newState)
	{
		orderState = newState;

		switch (orderState)
		{
			case StateOrder.None:
				{
					imageOrder.sprite = null;
					break;
				}
			case StateOrder.Sword1:
				{
					imageOrder.sprite = sword1Sprite;
					break;
				}
			case StateOrder.Sword2:
				{
					imageOrder.sprite = sword2Sprite;
					break;
				}
			case StateOrder.Sword3:
				{
                    imageOrder.sprite = sword3Sprite;
                    break;	
				}
			case StateOrder.Shield1:
				{
                    imageOrder.sprite = shield1Sprite;
                    break;	
				}
		}
	}

	public int GetLevel()
	{
		return level;
	}

	public void SetLevel()
	{
		level++;
	}
}
