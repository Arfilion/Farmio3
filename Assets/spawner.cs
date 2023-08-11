using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform restart;
    [SerializeField] GameObject bossPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DayNightCycle.instance.oleada == 10)
        {
            Instantiate(bossPrefab, transform.position, transform.rotation);
            player.transform.position = restart.position;

            Destroy(gameObject);
        }
    }
}
