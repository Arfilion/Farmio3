using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawned : MonoBehaviour
{
    public GameObject Canvas;
    public static bossSpawned instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BarView()
    {

        if (boss.instance != null)
        {
            Canvas.SetActive(true);
        }
        else
        {
            Canvas.SetActive(false);
        }
    }
}
