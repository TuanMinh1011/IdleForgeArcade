using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingBar : MonoBehaviour
{
    private Slider slider;

    private float count = 0;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        this.gameObject.SetActive(false);

        count = slider.value;
    }

    private void Update()
    {
        count += Time.deltaTime;

        if (count <= slider.maxValue)
        {
            slider.value = count;
        }
        //else
        //{
        //    count = 0;
        //}
    }

    public void StartSliderWaiting(float maxValue)
    {
        this.count = 0;
        slider.maxValue = maxValue;
    }
}
