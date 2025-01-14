using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    private Image sceneFaceImage;

    private void Awake()
    {
        sceneFaceImage = GetComponent<Image>();
    }

    public IEnumerator FaceInCoroutine(float duration)
    {
        Color startColor = new Color(sceneFaceImage.color.r, sceneFaceImage.color.g, sceneFaceImage.color.b, 1);
        Color targetColor = new Color(sceneFaceImage.color.r, sceneFaceImage.color.g, sceneFaceImage.color.b, 0);
    
        yield return FadeCoroutine(startColor, targetColor, duration);
    
        gameObject.SetActive(false);
    }

    public IEnumerator FaceOutCoroutine(float duration)
    {
        Color startColor = new Color(sceneFaceImage.color.r, sceneFaceImage.color.g, sceneFaceImage.color.b, 0);
        Color targetColor = new Color(sceneFaceImage.color.r, sceneFaceImage.color.g, sceneFaceImage.color.b, 1);
    
        gameObject.SetActive(true);
        yield return FadeCoroutine(startColor, targetColor, duration);
    }

    private IEnumerator FadeCoroutine(Color startColor, Color targetColor, float duration)
    {
        float elapsedTime = 0;
        float elapsedPercentage = 0;

        while (elapsedPercentage < 1)
        {
            elapsedPercentage = elapsedTime / duration;
            sceneFaceImage.color = Color.Lerp(startColor, targetColor, elapsedPercentage);
        
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
