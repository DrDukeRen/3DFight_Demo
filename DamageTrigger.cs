using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 水的死亡控制脚本
/// </summary>
public class DamageTrigger : MonoBehaviour {

    public float damageValue = 20f;
    private WidgetStates widgetStates;

    private void OnTriggerEnter(Collider other)
    {
        widgetStates = GameObject.FindWithTag("Player").GetComponent<WidgetStates>();
        widgetStates.ApplayDamage(damageValue);
        Debug.Log("我是水，你碰到我会死的！");
    }
}
