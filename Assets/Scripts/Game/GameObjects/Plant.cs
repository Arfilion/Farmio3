using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plant : Interactable
{
    public static Plant instance;

    public int life;
    public bool inmune;

    public int maxIrrigation = 100;
    [SerializeField] int currentIrrigation = 0;
    public int minIrrigationToGrow = 50;

    public int finishDays;
    public enum GrowthState { FertilSoil, BuriedSeed, FirstLeafs, Vegetation, Flowering, FullGrow, Dead }
    public GrowthState growthState = GrowthState.FertilSoil;

    private void Awake()
    {
        instance = this;
    }

    protected override void Start()
    {
        base.Start();
        finishDays = 0;
        inmune = false;
        Player.instance.EnableMovement();
        life = 100;
    }

    public override void Interact()
    {
        Irrigate();
    }

    public virtual void Grow()
    {
        if (growthState == GrowthState.FertilSoil)
        {
            growthState = GrowthState.BuriedSeed;
            //currentGrowthTime = 0.0f;
        }
        else if (growthState == GrowthState.BuriedSeed)
        {
            /*currentGrowthTime += Time.deltaTime;
            if (currentGrowthTime >= growthTime)
            {
                growthState = GrowthState.FirstLeafs;
                currentGrowthTime = 0.0f;
            }*/
            if (DayNightCycle.instance.hours == 8 && DayNightCycle.instance.minutes == 8 && currentIrrigation >= minIrrigationToGrow)
            {
                growthState = GrowthState.FirstLeafs;
                currentIrrigation -= (currentIrrigation / 2);
            }
        }
        else if (growthState == GrowthState.FirstLeafs)
        {
            if (DayNightCycle.instance.hours == 8 && DayNightCycle.instance.minutes == 6 && currentIrrigation >= minIrrigationToGrow)
            {
                growthState = GrowthState.Vegetation;
                currentIrrigation -= (currentIrrigation / 2);
            }
        }
        else if (growthState == GrowthState.Vegetation)
        {
            if (DayNightCycle.instance.hours == 8 && DayNightCycle.instance.minutes == 4 && currentIrrigation >= minIrrigationToGrow)
            {
                growthState = GrowthState.Flowering;
                currentIrrigation -= (currentIrrigation / 2);
            }
        }
        else if (growthState == GrowthState.Flowering)
        {
            if (DayNightCycle.instance.hours == 8 && DayNightCycle.instance.minutes == 1 && currentIrrigation >= minIrrigationToGrow)
            {
                growthState = GrowthState.FullGrow;
                currentIrrigation -= (currentIrrigation / 2);
            }
        }
        else if (growthState == GrowthState.FullGrow)
        {

        }
    }

    public virtual void Die()
    {
        growthState = GrowthState.Dead;
    }

    public virtual void Irrigate()
    {
        if (currentIrrigation < maxIrrigation && (Inventory.instance.GetItemQuantity("Water") > 0))
        {
            Inventory.instance.RemoveItem(Inventory.instance.FindItemByName("Water"), 1);
            currentIrrigation += 1;
        }
    }
    public void BecomeInmune()
    {
        inmune = true;
    }
    public void RemoveInmunity()
    {
        inmune = false;
    }

    public void TakeDamage(int dmg)
    {
        if (inmune == false)
        {
            life -= dmg;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (inmune == true)
        {
            return;
        }

    }

    public GrowthState GetFullGrowthState()
    {
        GrowthState fullGrowth;
        fullGrowth = GrowthState.FullGrow;
        return fullGrowth;
    }
}
