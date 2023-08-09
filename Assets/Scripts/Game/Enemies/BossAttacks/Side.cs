using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Side : MonoBehaviour
{
    [SerializeField] ParticleSystem VFX;

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
        if (other.gameObject.tag == "player")
        {
            Player.instance.TakeDamage();

        }
    }
}
