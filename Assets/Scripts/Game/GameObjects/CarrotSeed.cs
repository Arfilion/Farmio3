using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSeed : Interactable
{
    public Item itemTemplate;
    public int cost;

    [SerializeField] int blister;

    public override void Interact()
    {
        CreateNewItem();
        Inventory.instance.BuyItem(cost);
    }

    public void CreateNewItem()
    {
        Item newItem = itemTemplate;
        Inventory.instance.AddItem(newItem);
    }
}
