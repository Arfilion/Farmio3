using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBoss : MonoBehaviour
{
    public Image healthBarImage;

    private void Start()
    {
        healthBarImage = GetComponent<Image>();
        if (healthBarImage == null)
        {
            Debug.LogError("No se encontró un Image en este objeto.");
        }
    }

    private void Update()
    {
        if (boss.instance != null)
        {
        healthBarImage.fillAmount = (boss.instance.health / 5000f);
        }
    }     
}
