using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [Header("설정")]
    public Transform bgCamera; // 카메라 변수
    [SerializeField] float scrollingSpeed = 1.0f; // 초당 이동 속도
    [SerializeField] int spriteCount = 2; // 이어 붙일 배경 스프라이트 수

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
        // scrollingSpeed 로 오른쪽으로 이동, 균일하게->타임 델타타임  
        transform.position += Vector3.left * scrollingSpeed * Time.deltaTime;

        // 현재 카메라의 왼쪽 경계 계산
        float camLeftEdge = bgCamera.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        //현재 배경의 오른쪽 경계 계산
        float bgRightEdge = transform.position.x + spriteWidth * 0.5f;

        // 배경 오른쪽 끝이 카메라 왼쪽보다 작으면, 배경 오른쪽으로 이동
        if (bgRightEdge < camLeftEdge)
        {
            transform.position += Vector3.right * (spriteWidth * spriteCount);
        }
    }
}
