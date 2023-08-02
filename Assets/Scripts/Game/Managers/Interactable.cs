using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    public UiText UIT;

    //TP2
    public Vector3 Position { get; set; }

    protected virtual void Start()
    {
        if (SceneManager.GetActiveScene().name == "Farm")
        {
            UIT = FindObjectOfType<UiText>().GetComponent<UiText>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactor interactor = other.GetComponent<Interactor>();
        if (other.name == "Player")
        {
            if (interactor)
            {
                interactor.Add(this);
                UIT.EnableImg();
                if (this.name == "Canilla")
                {
                    if (Inventory.instance.FindItemByName("Bucket"))
                    {
                        UIT.uiText.text = "Manten presionado [E] para llenar el balde";
                    }
                    else
                    {
                        UIT.uiText.text = "Busca el balde para llenar";
                    }
                }
                else if (this.GetComponent<Plant>())
                {
                    Plant plant = GetComponent<Plant>();
                    if (plant.growthState == plant.GetFullGrowthState())
                    {
                        UIT.uiText.text = "Presione [E] para cosechar";
                    }
                    else
                    {
                        UIT.uiText.text = "Manten presionado [E] para regar";
                    }
                }else if (this.tag == "StoreItem")
                {
                    UIT.uiText.text = "Presione [E] para comprar \n        Costo: $" + this.GetComponent<CarrotSeed>().cost;
                }
                else if (this.tag == "SellPoint")
                {
                    if (Inventory.instance.CanSell())
                    {
                        UIT.uiText.text = "Presione [E] para vender";
                    }
                    else
                    {
                        UIT.uiText.text = "No tienes items para vender";
                    }
                }
                else
                {
                    UIT.uiText.text = "Presionado [E] para interactuar";
                }
            }
        }
        else
        {
            InteractableUnableImg();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactor interactor = other.GetComponent<Interactor>();

        if (other.name == "Player")
        {
            if (interactor)
            {
                interactor.Remove(this);
                InteractableUnableImg();
            }
        }

    }

    public void InteractableUnableImg()
    {
        UIT.UnableImg();
        UIT.uiText.text = "";
    }
}