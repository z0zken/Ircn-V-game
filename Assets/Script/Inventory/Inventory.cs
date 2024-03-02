using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;
public class Inventory 
{
    public int PlayerBudget { get;  set; }
    public List<Item> items;
    public Inventory(int numSlots)
    {
       this.items = new List<Item>();
        for(int i = 0; i < numSlots; i++)
        {
            this.items.Add(new Item());
        }
    }
    public void Add(Item item)
    {
        // I. Kiểm tra slot hiện tại còn k
        foreach(var itemTemp in items)
        {
            // 1. neu slot rong 
            if (itemTemp.type == CollectableType.NONE)
            {
                itemTemp.AssignItem(item);
                return;
            }
            // 2. neu slot cung Type[CollectableType]
            if (itemTemp.type == item.type)
            {
                if (itemTemp.isMax())
                {
                    continue;
                }
                else if(itemTemp.isAddAble(item.count))
                {
                    itemTemp.AddQuantity(item.count);
                    return;
                }
                else
                {
                    var numOverbalance = itemTemp.count + item.count - itemTemp.maxAllowed;
                    itemTemp.AddQuantity(item.count - numOverbalance);
                    item.ThrowQuantity(item.count - numOverbalance);
                }
                
            }
        }
        // II. Neeus k con them 1slot moi 
    }

}
