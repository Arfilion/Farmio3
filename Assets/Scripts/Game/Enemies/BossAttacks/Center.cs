using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    public bool isHit;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            DoLogic();

        }
    }

    public void DoLogic()
    {
        print(Player.instance.health);

        Player.instance.health -= 10;
        print(Player.instance.health);
    }
}