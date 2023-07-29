using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Side : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            print("lo hice");
        }
    }
}
