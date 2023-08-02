using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    //TP2

    public static Inventory instance;

    public Dictionary<Item, int> items = new Dictionary<Item, int>();

    public Image waterBarImage;
    public TMP_Text waterQtyText;
    private float maxWaterAmount = 100f;
    private float currWaterAmount = 0f;

    private void Awake()
    {
        instance = this;
        waterBarImage.fillAmount = 0;
    }

    private void Start()
    {
        // Example initialization of an item
        Item moneyItem = CreateScriptableObjectItem("Money", 200);
        AddItem(moneyItem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PrintInventory();
        }

        if (!FindItemByName("Bucket"))
        {
            waterBarImage.enabled = false;
        }
        else
        {
            waterBarImage.enabled = true;
        }

        if (waterBarImage.enabled)
        {
            UpdateWaterBar();
            waterQtyText.text = FindItemByName("Bucket").quantity + "/100";
        }
    }

    public void AddItem(Item newItem)
    {
        if (items.ContainsKey(newItem))
        {
            items[newItem] += newItem.quantity;
        }
        else
        {
            items[newItem] = newItem.quantity;
        }
    }

    public void RemoveItem(Item itemToRemove, int qty)
    {
        if (items.ContainsKey(itemToRemove))
        {
            if (items[itemToRemove] >= qty)
            {
                items[itemToRemove] -= qty;
                if (items[itemToRemove] <= 0)
                {
                    items.Remove(itemToRemove);
                }
            }
            else
            {
                Debug.LogWarning("Insufficient quantity of " + itemToRemove.name + " in the inventory.");
            }
        }
        else
        {
            Debug.LogWarning(itemToRemove.name + " not found in the inventory.");
        }
    }

    public int GetItemQuantity(string name)
    {
        foreach (var item in items)
        {
            if (item.Key.itemName == name)
            {
                return item.Value;
            }
        }
        return 0;
    }

    public Item FindItemByName(string name)
    {
        foreach (var item in items.Keys)
        {
            if (item.itemName == name)
            {
                return item;
            }
        }
        return null;
    }

    private void PrintInventory()
    {
        foreach (var item in items)
        {
            Debug.Log(item.Key.itemName + " x " + item.Value);
        }
    }

    private void UpdateWaterBar()
    {
        float fillAmount = Mathf.Clamp01(currWaterAmount / maxWaterAmount);
        waterBarImage.fillAmount = fillAmount;
    }

    public Item CreateScriptableObjectItem(string name, int quantity)
    {
        Item item = ScriptableObject.CreateInstance<Item>();
        item.itemName = name;
        item.quantity = quantity;
        return item;
    }

    public void BuyItem(int cost)
    {
        if (CanAfford(cost))
        {
            RemoveItem(FindItemByName("Money"), cost);
        }
    }

    private bool CanAfford(int cost)
    {
        if(GetItemQuantity("Money") > cost)
        {
            return true;
        }
        return false;
    }
}
