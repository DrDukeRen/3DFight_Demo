using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 复活点设置
/// </summary>
public class CheckPiont : MonoBehaviour {

    public static CheckPiont isActivePT;//当前复活点
    public CheckPiont firstPT;
    private WidgetStates widgetStates;
    private void Start()
    {
        widgetStates = GameObject.FindWithTag("Player").GetComponent<WidgetStates>();
        isActivePT = firstPT;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActivePT != this)
        {
            isActivePT = this;
        }
        widgetStates.AddHealth(widgetStates.maxHphealth);
        widgetStates.AddEnergy(widgetStates.maxBoost);
    }
}
