using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [Header("페이드-인 할 이미지/패널")]
    [SerializeField] private Image fadeInImage;
    [Header("페이드-인 시간")]
    [SerializeField] private float fadeInDuration = 1.0f; // 페이드인 시간

    private void Awake()
    {
        if (fadeInImage != null)
        {
            Color color = fadeInImage.color;
            color.a = 1.0f; //알파값 1부터 시작
            fadeInImage.color = color;
        }
    }

    private void Start()
    {
        if (fadeInImage != null)
        {
            StartCoroutine(FadeInRoutine());
        }
    }

    private IEnumerator FadeInRoutine()
    {
        Color color = fadeInImage.color;
        float timer = 0.0f;

        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1.0f, 0.0f, timer / fadeInDuration);
            fadeInImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeInImage.color = color;
        gameObject.SetActive(false); // 끝나면 비활성화
    }
}
