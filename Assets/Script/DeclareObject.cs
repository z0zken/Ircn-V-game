using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script
{

    public class Item
    {
        public CollectableType type;
        public int count { get; private set; }
        public int maxAllowed;
        public Sprite icon;
        public Item()
        {
            type = CollectableType.NONE;
            count = 0;
            maxAllowed = 999;
        }
        public Item(CollectableType type, int count , Sprite icon)
        {
            this.type = type;
            this.count = count;
            this.icon = icon;
            maxAllowed = 999;
        }
        public bool isAddAble(int quantity)
        {
            if (count + quantity <= maxAllowed) return true;
            return false;
        }
        public bool isMax()
        {
            return count >= maxAllowed;
        }
        public void AddQuantity(int quantity)
        {
            this.count += quantity;
        }
        public void ThrowQuantity(int quantity)
        {
            this.count -= quantity;
            if(this.count <= 0) type=CollectableType.NONE;
        }
        public void AssignItem(Item item)
        {
            this.count = item.count;
            this.type = item.type;
            this.icon = item.icon;  
        }


    }
    
}
