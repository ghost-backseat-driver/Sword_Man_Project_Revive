using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Coin_Launch : MonoBehaviour
{
    private Rigidbody2D rb; //코인이 될 프리팹에 RB 넣어놓을것
    private Coroutine lifeCoroutine;

    [Header("코인 생존 시간(초)")]
    [SerializeField] private float lifeTime = 8.0f; //부모 오브젝트 사망-비활성화 시간 이하로만 가능..

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //코인 포스모드 임펄스
    public void Launch(Vector2 dir, float force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * force, ForceMode2D.Impulse);

        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
        }

        lifeCoroutine = StartCoroutine(LifeTimerCo());
    }

    //일정 시간 후 풀로 반환
    private IEnumerator LifeTimerCo()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool();
    }

    //플레이어 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            //사운드 추가
            SoundManager.Instance.PlayEffect("GetCoin_SFX");

            //여기서 플레이어 보유 코인에 영향 줘야돼
            Coin_UI.Instance.AddCoin();

            ReturnToPool();
        }
    }

    //풀로 복귀
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
