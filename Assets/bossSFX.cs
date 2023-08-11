using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSFX : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> musicList;
    // Start is called before the first frame update
    private void Start()
    {
        Player player = FindObjectOfType<Player>().GetComponent<Player>();
        boss boss = FindObjectOfType<boss>();

        if (boss != null)
        {
            boss.RoarSound += PlayRoarSound;
            boss.SideSound += PlaySideSound;
            boss.InsideSound += PlayInsideSound;
            boss.EnrageSound += PlayEnrageSound;
            boss.SwingSound += PlaySwingSound;

        }
    }
    private void PlayRoarSound()
    {
        AudioSource.PlayClipAtPoint(musicList[0], transform.position);


    }
    private void PlaySideSound()
    {

        AudioSource.PlayClipAtPoint(musicList[1], transform.position);

    }
    private void PlayInsideSound()
    {

        AudioSource.PlayClipAtPoint(musicList[2], transform.position);

    }
    private void PlayEnrageSound()
    {

        AudioSource.PlayClipAtPoint(musicList[3], transform.position);
    }
    private void PlaySwingSound()
    {

        AudioSource.PlayClipAtPoint(musicList[4], transform.position);
    }
}
