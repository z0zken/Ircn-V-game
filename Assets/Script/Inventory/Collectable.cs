using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Script;
namespace Assets.Inventory
{
    internal class Collectable : MonoBehaviour
    {
        
        public Item item;
        [SerializeField]
        public CollectableType type;
        public int quantity;
        public Sprite icon;
        public float price;
        private void Awake()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            item = new Item(type, quantity, icon);
            spriteRenderer.sprite = icon;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
           if (collision != null && collision.tag == DeclareVariable.TAG_PLAYER) {
                player player = collision.GetComponent<player>();
                if (player)
                {
                    player.inventory.Add(item);
                }
                Destroy(this.gameObject);
                //Debug.Log("yes player");
            }
            else
            {
                //Debug.Log("NOT player");
            }
        }
    }
}

public enum CollectableType
{
    NONE, 
    TOOL_AXE, TOOL_WATERING, TOOL_HOE,
    SEED_CARROT, SEED_CORN,  SEED_PUMPKIN, SEED_BLUE_STAR_FRUIT,
    PRODUCT_PLANT_CORN, PRODUCT_PLANT_CARROT, PRODUCT_PLANT_PUMPKIN, PRODUCT_PLANT_BLUE_STAR_FRUIT
}
public enum CollectablePlant
{
    NONE, PLANT_CORN, PLANT_CARROT, PLANT_PUMPKIN, PLANT_BLUE_STAR_FRUIT
}