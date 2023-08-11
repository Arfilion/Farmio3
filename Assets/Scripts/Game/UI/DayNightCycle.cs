using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;


public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle instance;

    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text contador;
    public float seconds, minutes, hours;
    public bool isNight = false;
    public bool dayFinish = false;
    private int previousHour = 23;
    public int oleada;
    public GameObject clock;

    public Light light;
    public int lightSpeedRot;
    [SerializeField] List<Light> spotLight = new List<Light>();

    public Image nightOn;
    public TMP_Text nightOnText;
    float imgCd = 5;
    public List<GameObject> plants;

    private void Awake()
    {
        oleada = 0;
        instance = this;
        nightOn.enabled = false;
        nightOnText.enabled = false;
    }

    void Update()
    {
        DisplayTime();
        VerifyNight();
        CountWaves();

        if (hours >= 18 && hours <= 23)
        {
            TurnOnLights();
            TurnOnFog();
        }
        else
        {
            TurnOffLights();
            TurnOffFog();
        }

        if (FinishDay())
        {
            hours = 8;
            oleada += 1;
        }

        if (isNight)
        {
            if(imgCd > 0)
            {
                nightOn.enabled = true;
                nightOnText.enabled = true;
                nightOnText.text = "¡DEFIENDE LAS PLANTAS DE LOS INVASORES, SI ROMPEN TODAS PERDERAS!";
                imgCd -= Time.deltaTime;
            }
            else
            {
                nightOn.enabled = false;
                nightOnText.enabled = false;
            }
        }

        LooseCondition();
    }

    void DisplayTime()
    {
        seconds += 400* Time.deltaTime; 
        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }
        if(minutes >= 60)
        {
            hours++;
            minutes = 0;
        }
        if(hours >= 24)
        {
            seconds = 0;
            minutes = 0;
            hours = 0;
        }

        timeText.text = string.Format("{0:00} : {1:00}", hours, minutes);
        if (oleada == 10)
        {
            seconds = 0;
            minutes = 0;
            hours = 19;
            clock.SetActive(false);
        }
    }
    void CountWaves()
    {

                contador.text = string.Format("Oleada " +(1+oleada));
    }


    void VerifyNight()
    {
        if (hours >= 19 && hours <= 23)
        {
            isNight = true;
            light.transform.Rotate(Vector3.right * 4 * Time.deltaTime);
            light.color = new Color(0.08f,0.05f, 0.32f);
        }
        else
        {
            isNight = false;
            light.transform.Rotate(Vector3.right * lightSpeedRot * Time.deltaTime);
            light.color = new Color(1,0.95f,0.8f);
        }
    }

    public bool FinishDay()
    {
        if (hours >= 23)
        {
            dayFinish = true;
        }
        else
        {
            dayFinish = false;
        }
        return dayFinish;
    }

    public void LooseCondition()
    {
        if(isNight == true)
        {
            plants = GameObject.FindGameObjectsWithTag("plant").ToList();
            if(plants.Count > 0)
            {
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void TurnOnLights()
    {
        foreach (Light light in spotLight)
        {
            light.enabled = true;
        }
    }

    public void TurnOffLights()
    {
        foreach (Light light in spotLight)
        {
            light.enabled = false;
        }
    }

    public void TurnOnFog()
    {
        RenderSettings.fog = true;
    }

    public void TurnOffFog()
    {
        RenderSettings.fog = false;
    }
}
