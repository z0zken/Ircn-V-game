using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public static Inventory_UI instance;
    public List<Slot_UI> slots_UI;
    [SerializeField] 
    public player playerr;
    private void Awake()
    {
        //slots_UI = new List<Slot_UI>(playerr.inventory.items.Count);
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(false);
        /*for (int i = 0; i < playerr.inventory.items.Count; i++)
        {
            slots_UI.Add(new Slot_UI());
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
        SetUp();
    }
    public void selectItem()
    {
        slots_UI[playerr.select_item_index].SetBackGroundColor(Color.green);
    }
    public void unSelectItem()
    {
        slots_UI[playerr.select_item_index].SetBackGroundColor(new Color(190 / 255f, 176 / 255f, 176 / 255f));
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            //SetUp();
        }
        else
        {
            inventoryPanel.SetActive(false);

        }
    }
    public void SetUp()
    {
        if(slots_UI.Count == playerr.inventory.items.Count)
        {
            for(int i = 0; i< slots_UI.Count; i++)
            {
                if (playerr.inventory.items[i].type != CollectableType.NONE)
                {
                    slots_UI[i].SetItem(playerr.inventory.items[i]);
                }
                else
                {
                    slots_UI[i].SetEmpty();
                }
            }
        }
        
    }
}
