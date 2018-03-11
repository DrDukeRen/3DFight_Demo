using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品拾起脚本
/// </summary>
public class PickUp : MonoBehaviour {

    private  bool isPickUp=false ;
    public WidgetInventory.InventoryItem itemType;//定义该掉落的物品的属性，是干嘛用的。能量还是生命值？
    public int itemAmount = 1;//添加的数量
    private void OnTriggerEnter(Collider other)
    {
        if (isPickUp)
        {
            return;
        }
        //捡起这个物品
        WidgetInventory widgetInventory = other.GetComponent<WidgetInventory>();//获取玩家身上的脚本
        widgetInventory.GetItem(itemType, itemAmount);


        isPickUp = true;
        Destroy(gameObject);
    }
}
