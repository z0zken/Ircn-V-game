using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Slot_UI : MonoBehaviour
{
    [SerializeField]
    public Image imageBackGround;
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public void SetItem(Item item)
    {
        if (item == null) return;
        itemIcon.sprite = item.icon;
        itemIcon.color = new Color(1, 1, 1, 1);
        quantityText.text = item.count.ToString();
    }
    public void SetQuantity(int money)
    {
        quantityText.text = money.ToString();
    }
    public void SetEmpty()
    {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1);
        quantityText.text = "0";
    }
    public void SetBackGroundColor(Color color)
    {
        imageBackGround.color = color;
    }

}
