using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerCircle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Player.instance.TakeDamage();

        }
    }
}

