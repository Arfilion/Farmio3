using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    public GameObject rangedEnemyPrefab;
    public GameObject meleeEnemyPrefab;
    public GameObject diggerEnemyPrefab;
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

        if (counter>=reload && DayNightCycle.instance.isNight)
        {

            index= Random.Range(1, 4);
            switch (index)
            {
                case 1:
                    Instantiate(meleeEnemyPrefab, transform.position, transform.rotation);
                    break;
                case 2:
                    Instantiate(rangedEnemyPrefab, transform.position, transform.rotation);
                    break;
                case 3:
                    Instantiate(diggerEnemyPrefab, transform.position, transform.rotation);
                    break;
                default:
                    break;
            }
            counter = 0;
        }
        StartCoroutine(Spawn());

    }
    IEnumerator Spawn()
    {
        randomPosition = Random.Range(0, 4);
        transform.position = Positions[randomPosition].transform.position;
        yield return new WaitForSeconds(reload);
    }
}
