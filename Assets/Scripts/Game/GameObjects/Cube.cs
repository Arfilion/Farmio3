using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Interactable
{
    public Item itemTemplate;

    public override void Interact()
    {
        InteractableUnableImg();
        CreateNewItem();
        Interactor.instance.Remove(this);
        this.gameObject.SetActive(false);
    }

    public void CreateNewItem()
    {
        Item newItem = itemTemplate;
        Inventory.instance.AddItem(newItem);
    }
}
