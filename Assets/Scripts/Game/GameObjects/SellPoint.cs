using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fleitas Gabriel

public class SellPoint : Interactable
{
    int interactTimes = 0;
    public bool hasInteract = false;

    private void Update()
    {
        if (hasInteract)
        {
            Player.instance.DisableMovement();
            Item moneyItem = Inventory.instance.FindItemByName("Money");
            if (interactTimes == 1)
            {
                UIT.uiText.text = "Presione el numero indicado: " +
                    "\n 1.- Tomate x " + Inventory.instance.GetItemQuantity("Tomato") + "\nPrecio x unidad: $15" +
                    "\n 2.- Zanahoria x " + Inventory.instance.GetItemQuantity("Carrot") + "\nPrecio x unidad: $7" +
                    "\n 3.- Papa x " + Inventory.instance.GetItemQuantity("Potato") + "\nPrecio x unidad: $9" +
                    "\n 0.- Salir";
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (Inventory.instance.GetItemQuantity("Tomato") > 0)
                    {
                        Inventory.instance.RemoveItem(Inventory.instance.FindItemByName("Tomato"), 1);
                        moneyItem.quantity += 15;
                        Inventory.instance.items[moneyItem] = moneyItem.quantity;
                    }
                    else
                    {
                        print("No tienes mas zanahorias");
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (Inventory.instance.GetItemQuantity("Carrot") > 0)
                    {
                        Inventory.instance.RemoveItem(Inventory.instance.FindItemByName("Carrot"), 1);
                        moneyItem.quantity += 7;
                        Inventory.instance.items[moneyItem] = moneyItem.quantity;
                    }
                    else
                    {
                        print("No tienes mas zanahorias");
                    }
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    if (Inventory.instance.GetItemQuantity("Potato") > 0)
                    {
                        Inventory.instance.RemoveItem(Inventory.instance.FindItemByName("Potato"), 1);
                        moneyItem.quantity += 9;
                        Inventory.instance.items[moneyItem] = moneyItem.quantity;
                    }
                    else
                    {
                        print("No tienes mas papas");
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    interactTimes = 0;
                    UIT.uiText.text = "";
                    UIT.UnableImg();
                }
            }
            else if (interactTimes == 0)
            {
                Player.instance.EnableMovement();
            }
        }
    }

    public override void Interact()
    {
        hasInteract = true;
        interactTimes = 1;
    }
}