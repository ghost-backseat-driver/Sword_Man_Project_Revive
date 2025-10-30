using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_Arrow : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField] private float speed = 5.0f;

    [Header("생존 시간 (초)")]
    [SerializeField] private float lifeTime = 3.0f;

    private Vector2 moveDir;

    private Coroutine lifeCoroutine;

    // 방향을 설정하고 이동 시작
    public void Shoot(Vector2 direction)
    {
        moveDir = direction.normalized;

        // 이전 코루틴이 실행 중이면 종료
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
        }

        lifeCoroutine = StartCoroutine(LifeTimerCo());
    }

    private void Update()
    {
        // 계속 이동
        transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
    }

    // 일정 시간 후 풀로 반환
    private IEnumerator LifeTimerCo()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool();
    }

    // 플레이어와 충돌 시 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            // 플레이어 피격 처리
            Character_HP playerHp = collision.GetComponent<Character_HP>();
            if (playerHp != null)
            {
                playerHp.TakeDamage(1, transform.position); // 필요에 따라 데미지 조정
            }

            ReturnToPool();
        }
        // 필요 시, 다른 충돌 처리 추가 가능
    }
    // 풀로 복귀
    private void ReturnToPool()
    {
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }

        GameManagers.Pool.ReturnPool(this);
    }
}
