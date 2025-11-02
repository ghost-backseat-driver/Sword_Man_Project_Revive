using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFx : MonoBehaviour
{
    [Header("Fade-out 설정")]
    [SerializeField] private Image screenImage; // 블링크페이드 적용할 이미지
    [SerializeField] public float blinkSpeed { get; private set; } = 0.05f;   // 점멸 속도
    [SerializeField] public int blinkCount { get; private set; } = 5;         // 점멸 횟수
    [SerializeField] public float fadeDuration { get; private set; } = 1.0f;  // 페이드아웃 시간
    //사용할 코루틴 저장용
    private Coroutine ScreenFX;

    private void Awake()
    {
        if (screenImage != null)
        {
            Color screenC = screenImage.color;
            screenC.a = 0.0f;
            screenImage.color = screenC;
        }
    }

    // 점멸->페이드아웃 실행, 끝나면 콜백 호출
    public void Play(Action onComplete)
    {
        StopCurrentRoutine();
        ScreenFX = StartCoroutine(PlayRoutine(onComplete));
    }

    //코루틴 실행
    private IEnumerator PlayRoutine(Action onComplete)
    {
        // 패널점멸기능
        Color screenC = screenImage.color;
        for (int i = 0; i < blinkCount; i++)
        {
            while (screenC.a < 1.0f)
            {
                screenC.a += Time.deltaTime / blinkSpeed;
                screenImage.color = screenC;
                yield return null;
            }

            while (screenC.a > 0.0f)
            {
                screenC.a -= Time.deltaTime / blinkSpeed;
                screenImage.color = screenC;
                yield return null;
            }
        }

        // 패널 페이드아웃 기능
        float timer = 0.0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            screenC.a = Mathf.Lerp(0.0f, 1.0f, timer / fadeDuration);
            screenImage.color = screenC;
            yield return null;
        }

        screenC.a = 1.0f;
        screenImage.color = screenC;

        ScreenFX = null;

        // 다 끝났냐? 콜백 호출
        onComplete?.Invoke();
    }

    //여러번 버튼 누를 경우 대참사 방지
    private void StopCurrentRoutine()
    {
        if (ScreenFX != null)
        {
            StopCoroutine(ScreenFX);
            ScreenFX = null;
        }
    }
}
