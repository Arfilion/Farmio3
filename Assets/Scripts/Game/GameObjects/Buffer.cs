using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    public GameObject speedPrefab;
    public GameObject damagePrefab;
    public GameObject shieldPrefab;
    public GameObject healPrefab;
    private float counter;
    public float reload;
    public int index;
    public GameObject[] Positions;
    private int randomPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        counter += Time.deltaTime;

        if (counter >= reload  && DayNightCycle.instance.isNight == true)
        {

            if (boss.instance == null)
            {
                 index = Random.Range(1, 4);

            }
            else
            {
                index = Random.Range(1, 5);
            }
            switch (index)
            {
                case 1:
                    Instantiate(speedPrefab, transform.position, transform.rotation);
                    break;
                case 2:
                    Instantiate(damagePrefab, transform.position, transform.rotation);
                    break;
                case 3:
                    Instantiate(shieldPrefab, transform.position, transform.rotation);
                    break;
                case 4:
                    Instantiate(healPrefab, transform.position, transform.rotation);
                    break;
                default:
                    break;
            }
            counter = 0;
        }
        StartCoroutine ( Spawn());
    }
    IEnumerator Spawn()
    {
       
            randomPosition = Random.Range(0, 4);
            transform.position = Positions[randomPosition].transform.position;
            yield return new WaitForSeconds(reload);
            yield return null;
       
       
    }
}
