using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    private float minFadeTime = 1f;
    [SerializeField]
    private float maxFadeTime = 4f;

    private float fadeTime;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeTime = Random.Range(minFadeTime, maxFadeTime);

        StartCoroutine(nameof(FadeLoop));
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            // Alpha 값을 1에서 0으로 : Fade Out
            yield return StartCoroutine(OnFade(1, 0));
            // Alpha 값을 0에서 1으로 : Fade In
            yield return StartCoroutine(OnFade(0, 1));
        }
    }

    private IEnumerator OnFade(float start, float end)
    {
        float percent = 0;
        while (percent < 1)
        {
            // fadeTime 시간동안 while() 반복문 실행
            percent += Time.deltaTime / fadeTime;

            // percent는 0 -> 1로 증가하고 그 값에 따라 Alpha 값을 변화시킴
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start,end, percent);
            spriteRenderer.color = color;

            yield return null;
        }
    }

   
}
