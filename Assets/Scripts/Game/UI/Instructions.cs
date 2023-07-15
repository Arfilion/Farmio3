using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Instructions : MonoBehaviour
{
    public Image instructionsImg;
    public TMP_Text instructionsOnText;
    public float imgCd = 20;

    private void Update()
    {
        if (imgCd >= 0)
        {
            instructionsImg.enabled = true;
            instructionsOnText.enabled = true;
            instructionsOnText.text = "Instrucciones: \n1.- Agarra las semillas y plantalas \n2.- Agarra el cubo para regarlas \n" +
                "3.- Llena el cubo de agua en la canilla \n4.- Manten las plantas regadas para que crezcan \n" +
                "5.- Cosechalas cuando esten al maximo \n6.- ¡Disfruta tus cosechas!";
            imgCd -= Time.deltaTime;
        }
        else
        {
            instructionsImg.enabled = false;
            instructionsOnText.enabled = false;
            instructionsOnText.text = "";
        }
    }
}
