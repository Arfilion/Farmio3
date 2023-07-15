using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canilla : Interactable
{
    public Item itemTemplate;
    Item Bucket;

    public int maxWater = 100;
    int currentWater;

    private void Update()
    {
        Bucket = Inventory.instance.FindItemByName("Bucket");
        currentWater = Inventory.instance.GetItemQuantity("Bucket");
    }

    public override void Interact()
    {
        if (Bucket)
        {
            if (currentWater < maxWater)
            {
                CreateNewItem();
            }
        }
    }

    public void CreateNewItem()
    {
        Item newItem = itemTemplate;
        Inventory.instance.AddItem(newItem);
    }
}
