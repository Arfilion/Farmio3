using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyGeneric;

public class Interactor : MonoBehaviour
{
    public static Interactor instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Interactable> interactables = new List<Interactable>();
    float min = 0;

    public event Action OnPlayerInteract;

    void Update()
    {
        min = float.MaxValue;
        Vector3 sourcePos = this.transform.position;

        //TP2
        Interactable nearest = MyGeneric.MyGeneric.FindNearestObject(sourcePos, interactables, obj => Vector3.Distance(sourcePos, obj.Position));


        if (nearest != null)
        {
            if ((nearest.name == "Canilla" || nearest.GetComponent<CarrotPlant>()) && Input.GetKey(KeyCode.E))
            {
                nearest.Interact();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                nearest.Interact();
            }
        }
    }

    public void Add(Interactable interact)
    {
        if (!interactables.Contains(interact))
        {
            interactables.Add(interact);
        }
    }

    public void Remove(Interactable interact)
    {
        if (interactables.Contains(interact))
        {
            interactables.Remove(interact);
        }
    }
}
