using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthBarUi : MonoBehaviour
{
    [SerializeField] Image healthBarImage;

    // Start is called before the first frame update
    void Start()
    {
        healthBarImage = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

        if (boss.instance != null)
        {
            healthBarImage.fillAmount = (Player.instance.health / 100f);

        }
       
    }
}
