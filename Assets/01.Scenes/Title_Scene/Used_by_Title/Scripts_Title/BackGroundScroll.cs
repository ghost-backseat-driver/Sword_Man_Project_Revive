using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [Header("����")]
    public Transform bgCamera; // ī�޶� ����
    [SerializeField] float scrollingSpeed = 1.0f; // �ʴ� �̵� �ӵ�
    [SerializeField] int spriteCount = 2; // �̾� ���� ��� ��������Ʈ ��

    private float spriteWidth;

    void Start()
    {
        if (!bgCamera) bgCamera = Camera.main.transform;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteWidth = spriteRenderer.bounds.size.x;
        }
    }

    void Update()
    {
        // scrollingSpeed �� ���������� �̵�, �����ϰ�->Ÿ�� ��ŸŸ��  
        transform.position += Vector3.left * scrollingSpeed * Time.deltaTime;

        // ���� ī�޶��� ���� ��� ���
        float camLeftEdge = bgCamera.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        //���� ����� ������ ��� ���
        float bgRightEdge = transform.position.x + spriteWidth * 0.5f;

        // ��� ������ ���� ī�޶� ���ʺ��� ������, ��� ���������� �̵�
        if (bgRightEdge < camLeftEdge)
        {
            transform.position += Vector3.right * (spriteWidth * spriteCount);
        }
    }
}
