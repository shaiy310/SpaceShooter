using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fadder : MonoBehaviour
{
    public static Fadder Instance { get; private set; }

    Image image;

    void Start()
    {
        Instance = this;
        image = GetComponent<Image>();
    }

    public IEnumerator FadeAnimation(Color color, float duration = 0.25f, float alpha = 1)
    {
        yield return FadeOut(color, duration / 2, alpha);
        yield return FadeIn(color, duration / 2, alpha);
    }

    public IEnumerator FadeIn(Color color, float duration, float alpha = 1)
    {
        var timePassed = 0f;

        while (timePassed < duration) {
            color.a = (1 - timePassed / duration) * alpha;
            image.color = color; 
            
            timePassed += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        color.a = 0;
        image.color = color;
    }

    public IEnumerator FadeOut(Color color, float duration, float alpha = 1)
    {
        var timePassed = 0f;

        while (timePassed < duration) {
            color.a = timePassed / duration;
            image.color = color;
            timePassed += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        color.a = alpha;
        image.color = color;
    }
}
