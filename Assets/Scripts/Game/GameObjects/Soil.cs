using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Soil : Interactable
{
    public static Soil instance;
    public bool hasPlantedSeed = false;
    public List<GameObject> plants = new List<GameObject>();
    public event Action OnPlayerPlant;

    int interactTimes = 0;
    public bool hasInteract = false;

    private void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (hasInteract == true)
        {
            Player.instance.DisableMovement();
            if (interactTimes == 1)
            {
                UIT.uiText.text = "Presione el numero indicado: " +
                    "\n 1.- Tomate x " + Inventory.instance.GetItemQuantity("TomatoSeed") +
                    "\n 2.- Zanahoria x " + Inventory.instance.GetItemQuantity("CarrotSeed") +
                    "\n 3.- Papa x " + Inventory.instance.GetItemQuantity("PotatoSeed") +
                    "\n 0.- Salir";
            }
            else if (interactTimes == 0)
            {
                Player.instance.EnableMovement();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (Inventory.instance.GetItemQuantity("TomatoSeed") > 0)
                {
                    Instantiate(plants[0], this.transform.position, this.transform.rotation);
                    Inventory.instance.RemoveItem(Inventory.instance.FindItemByName("TomatoSeed"), 1);
                    hasPlantedSeed = true;
                    Interactor.instance.Remove(this);
                    Destroy(this.gameObject);
                }
                else
                {
                    print("No tienes mas semillas de tomate");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Inventory.instance.GetItemQuantity("CarrotSeed") > 0)
                {
                    Instantiate(plants[1], this.transform.position, this.transform.rotation);
                    Inventory.instance.RemoveItem(Inventory.instance.FindItemByName("CarrotSeed"), 1);
                    hasPlantedSeed = true;
                    OnPlayerPlant?.Invoke();
                    Interactor.instance.Remove(this);
                    Destroy(this.gameObject);
                }
                else
                {
                    print("No tienes mas semillas de Zanahoria");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Inventory.instance.GetItemQuantity("PotatoSeed") > 0)
                {
                    Instantiate(plants[2], this.transform.position, this.transform.rotation);
                    Inventory.instance.RemoveItem(Inventory.instance.FindItemByName("PotatoSeed"), 1);
                    hasPlantedSeed = true;
                    Interactor.instance.Remove(this);
                    Destroy(this.gameObject);
                }
                else
                {
                    print("No tienes mas semillas de Papa");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                interactTimes = 0;
                UIT.uiText.text = "";
                UIT.UnableImg();
            }
        }
    }

    public override void Interact()
    {
        hasInteract = true;
        interactTimes = 1;
    }

    private void OnDestroy()
    {
        if (UIT.img)
        {
            UIT.UnableImg();
            UIT.uiText.text = "";
        }
    }
}
