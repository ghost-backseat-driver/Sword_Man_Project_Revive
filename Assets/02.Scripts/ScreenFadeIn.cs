using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [Header("���̵�-�� �� �̹���/�г�")]
    [SerializeField] private Image fadeInImage;
    [Header("���̵�-�� �ð�")]
    [SerializeField] private float fadeInDuration = 1.0f; // ���̵��� �ð�

    private void Awake()
    {
        if (fadeInImage != null)
        {
            Color color = fadeInImage.color;
            color.a = 1.0f; //���İ� 1���� ����
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
        gameObject.SetActive(false); // ������ ��Ȱ��ȭ
    }
}
