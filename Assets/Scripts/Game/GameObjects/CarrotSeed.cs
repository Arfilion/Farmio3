using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSeed : Interactable
{
    //Fleitas Gabriel
    public Item itemTemplate;
    public int cost;

    public override void Interact()
    {
        if (Inventory.instance.CanAfford(cost))
        {
            CreateNewItem();
            Inventory.instance.BuyItem(cost);
        }
        else
        {
            UIT.uiText.text = "No tienes suficiente dinero";
        }
    }

    public void CreateNewItem()
    {
        Item newItem = itemTemplate;
        Inventory.instance.AddItem(newItem);
    }
}
