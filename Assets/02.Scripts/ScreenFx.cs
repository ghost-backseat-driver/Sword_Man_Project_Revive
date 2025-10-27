using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFx : MonoBehaviour
{
    [Header("Fade-out ����")]
    [SerializeField] private Image screenImage; // ��ũ���̵� ������ �̹���
    [SerializeField] public float blinkSpeed { get; private set; } = 0.05f;   // ���� �ӵ�
    [SerializeField] public int blinkCount { get; private set; } = 5;         // ���� Ƚ��
    [SerializeField] public float fadeDuration { get; private set; } = 1.0f;  // ���̵�ƿ� �ð�
    //����� �ڷ�ƾ �����
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

    // ����->���̵�ƿ� ����, ������ �ݹ� ȣ��
    public void Play(Action onComplete)
    {
        StopCurrentRoutine();
        ScreenFX = StartCoroutine(PlayRoutine(onComplete));
    }

    //�ڷ�ƾ ����
    private IEnumerator PlayRoutine(Action onComplete)
    {
        // �г�������
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

        // �г� ���̵�ƿ� ���
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

        // �� ������? �ݹ� ȣ��
        onComplete?.Invoke();
    }

    //������ ��ư ���� ��� ������ ����
    private void StopCurrentRoutine()
    {
        if (ScreenFX != null)
        {
            StopCoroutine(ScreenFX);
            ScreenFX = null;
        }
    }
}
