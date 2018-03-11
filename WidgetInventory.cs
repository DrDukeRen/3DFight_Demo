using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 来处理捡起来的物品数量，以及类型
/// </summary>
public class WidgetInventory : MonoBehaviour {

    //定义一个枚举类型
    public enum InventoryItem
    {
         ENERGPACK,//用来恢复能量值
         REPAIRKIT//用来恢复生命值
    }
    private WidgetStates widgetStates;
    private float repaorkKitHealAmt = 5.0f;//恢复五点生命值
    private float energyPackHealAmt = 5.0f;//恢复五点能量值

    Dictionary<InventoryItem, int> widgetDict;//字典？

    private void Start()
    {
        widgetStates = GetComponent<WidgetStates>();
        widgetDict = new Dictionary<InventoryItem, int>();

        widgetDict.Add(InventoryItem.ENERGPACK, 1);
        widgetDict.Add(InventoryItem.REPAIRKIT, 2);
    }
    //得到类型自增数量
    public void GetItem(InventoryItem item,int amoun)
    {
        widgetDict[item] += amoun;
        print(widgetDict[item]);
    }
    //使用能量值和生命值
    public void UseItem(InventoryItem item, int amoun)
    {
        if (widgetDict[item]<=0)
        {
            return;
        }
        widgetDict[item] -= amoun;
        switch (item)
        {
            case InventoryItem.ENERGPACK:
                widgetStates.AddEnergy(energyPackHealAmt);
                break;
            case InventoryItem.REPAIRKIT:
                widgetStates.AddHealth(repaorkKitHealAmt);
                break;
        }
    }

    //现有的数量与总数对比
    public bool  CompareItemCount(InventoryItem item, int comNumber)
    {
        return widgetDict[item] >= comNumber;
    }
    //得到当前数量值
    public int GetItemCount(InventoryItem item)
    {
        return widgetDict[item];
    }
}
