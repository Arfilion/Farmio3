using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Shield : PowerUp
{
    public delegate void PowerUpPicker();
    public event PowerUpPicker PickedPowerUp;
    public List<GameObject> plants;

    // Start is called before the first frame update
    void Start()
    {
        duration = 10;
        name = "shield";

    }

    // Update is called once per frame


    protected override void DoLogic()
    {
        PickedPowerUp?.Invoke();

        plants = GameObject.FindGameObjectsWithTag("plant").ToList();
        foreach (GameObject plant in plants)
        {
            plant.GetComponent<Plant>().inmune = true;
        }
        PickUp();
    }        
    
}
