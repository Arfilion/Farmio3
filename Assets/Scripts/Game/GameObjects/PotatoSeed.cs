using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoSeed : Interactable
{
    public Item itemTemplate;
    public int cost;

    [SerializeField] int blister;

    public override void Interact()
    {
        if (Inventory.instance.CanAfford(cost))
        {
            CreateNewItem();
            Inventory.instance.BuyItem(cost);
        }
        else{
            UIT.uiText.text = "No tienes suficiente dinero";
        }
    }

    public void CreateNewItem()
    {
        //TP2
        Item newItem = itemTemplate;
        Inventory.instance.AddItem(newItem);
    }
}
