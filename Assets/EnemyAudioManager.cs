using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    public List<AudioClip> musicList;


    // Start is called before the first frame update
    void Start()
    {
        digger digger = FindObjectOfType<digger>();

        if (digger != null)
        {
            digger.OnEnemyMove += EnemyWalkSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemyWalkSound()
    {
        AudioSource.PlayClipAtPoint(musicList[0], transform.position);
    }
}
