using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHPGauge : MonoBehaviour
{
    public UILabel hpPointLabel;

    public GameObject[] gauges;

    public void SetHPGauge(int value)
    {
        int removeCount = gauges.Length - value;
        hpPointLabel.text = value.ToString();

        for(int i = 0; i < gauges.Length; i++)
        {
            if(i == removeCount)
                return;
            gauges[i].SetActive(false);
        }
    }

    public void InitializeHPGauge()
    {
        for(int i = 0; i < gauges.Length; i++)
        {
            gauges[i].SetActive(true);
        }

        hpPointLabel.text = "15";
    }
}
