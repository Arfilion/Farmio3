using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    
    public Image fadeImage;
    public float fadeDuration = 1.0f;
    public float pauseDuration = 1.0f;

    public Color transparentColor = new Color(0f, 0f, 0f, 0f);
    public Color blackColor = new Color(0f, 0f, 0f, 1f);
    public static Fader instance;

    public void Awake()
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
    public void Start  ()
    {
        StartCoroutine(FadeSequence());
    }

    public IEnumerator FadeSequence()
    {
        // Fade to black
        yield return StartCoroutine(FadeImage(fadeImage, transparentColor, blackColor, fadeDuration));

        // Pause with the screen blackened
        yield return new WaitForSeconds(pauseDuration);

        // Fade from black
        yield return StartCoroutine(FadeImage(fadeImage, blackColor, transparentColor, fadeDuration));
    }

    public  IEnumerator FadeImage(Image image, Color startColor, Color targetColor, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time <= endTime)
        {
            float t = (Time.time - startTime) / duration;
            image.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        image.color = targetColor;
    }
}
