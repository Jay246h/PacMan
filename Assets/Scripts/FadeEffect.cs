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
            // Alpha ���� 1���� 0���� : Fade Out
            yield return StartCoroutine(OnFade(1, 0));
            // Alpha ���� 0���� 1���� : Fade In
            yield return StartCoroutine(OnFade(0, 1));
        }
    }

    private IEnumerator OnFade(float start, float end)
    {
        float percent = 0;
        while (percent < 1)
        {
            // fadeTime �ð����� while() �ݺ��� ����
            percent += Time.deltaTime / fadeTime;

            // percent�� 0 -> 1�� �����ϰ� �� ���� ���� Alpha ���� ��ȭ��Ŵ
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(start,end, percent);
            spriteRenderer.color = color;

            yield return null;
        }
    }

   
}
