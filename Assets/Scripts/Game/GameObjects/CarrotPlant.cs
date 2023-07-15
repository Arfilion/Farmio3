using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPlant : Plant
{
    public float growthTime = 10.0f;

    public MeshFilter thisModel;
    public List<MeshFilter> models = new List<MeshFilter>();

    public Item itemTemplate;
    public GameObject soil;

    private void Awake()
    {
        thisModel = GetComponent<MeshFilter>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Interact()
    {
        if (growthState == GrowthState.FullGrow)
        {
            UIT.UnableImg();
            UIT.uiText.text = "";
            CreateNewItem();
            Instantiate(soil, this.transform.position, this.transform.rotation);
            Interactor.instance.Remove(this);
            Destroy(this.gameObject);
        }
        else
        {
            if (Inventory.instance.FindItemByName("Bucket"))
            {
                base.Interact();
            }
            else
            {
                UIT.uiText.text = "Necesitas agarrar el cubo y cargarle agua para regar";
            }
        }
    }

    private void Update()
    {
        Grow();
    }

    public override void Grow()
    {
        base.Grow();
        switch (growthState)
        {
            case GrowthState.BuriedSeed:
                thisModel.sharedMesh = models[0].sharedMesh;
                break;
            case GrowthState.FirstLeafs:
                thisModel.sharedMesh = models[1].sharedMesh;
                break;
            case GrowthState.Vegetation:
                thisModel.sharedMesh = models[2].sharedMesh;
                break;
            case GrowthState.Flowering:
                thisModel.sharedMesh = models[3].sharedMesh;
                break;
            case GrowthState.FullGrow:
                thisModel.sharedMesh = models[4].sharedMesh;
                break;
        }
    }

    public void CreateNewItem()
    {
        Item newItem = Instantiate(itemTemplate);
        newItem.quantity = Random.Range(2, 6);
        Inventory.instance.AddItem(newItem);
    }
}
