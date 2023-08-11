using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class digger : enemyEntity
{
    public SkinnedMeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        canBeHit = true;
        Find();
        Hide();
        readyToHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss.instance != null)
        {
            Destroy(gameObject);
        }
        counter += Time.deltaTime;
        Hit(spawnPoint);
        Checker();
        Movement();
        if (DayNightCycle.instance.isNight == false)
        {
            Destroy(gameObject);
        }
    }
    public void Hide()
    {
        canBeHit = false;
        renderer.enabled = false;
    }
    public void Reveal()
    {
        canBeHit = true;
        renderer.enabled = true;

        GetComponent<digger>().DestroyChild();
    }
    public void DestroyChild()
    {
        Transform child = transform.GetChild(0);

        if (child != null)
        {
            Destroy(child.gameObject);
        }
    }
}
