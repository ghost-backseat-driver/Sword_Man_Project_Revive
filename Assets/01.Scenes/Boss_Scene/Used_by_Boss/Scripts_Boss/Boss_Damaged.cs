using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Damaged : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;
    private Character_HP hp;

    [Header("피격 관련 설정")]
    [Header("넉백시 X 힘")]
    [SerializeField] private float knockbackForceX = 2.0f;
    [Header("넉백시 Y 힘")]
    [SerializeField] private float knockbackForceY = 1.0f;

    [Header("피격시 점멸")]
    [SerializeField] private Color blinkColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    [Header("점멸 시간")]
    [SerializeField] private float blinkDuration = 1.0f;

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();
        hp = GetComponent<Character_HP>();
    }

    public void OnHit(Vector2 attackerPos)
    {
        //죽었으면 하지말고
        if (hp.isDead) return;

        //넉백 방향 계산
        //맞는 위치 공격자 위치 기반으로 가져오고
        Vector2 hitDir = ((Vector2)transform.position - attackerPos).normalized;
        //넉백 위치는 맞는 맞는 위치에 x,y 힘 값 곱하고
        Vector2 knockback = new Vector2(hitDir.x * knockbackForceX, knockbackForceY);

        //이동값 초기화 한 다음
        core.rb.velocity = Vector2.zero;
        //밀어버려
        core.rb.AddForce(knockback, ForceMode2D.Impulse);

        //보스 피격사운드 추가해야되고,-일단 통일
        SoundManager.Instance.PlayEffect("Player_ATK2_SFX");

        //피격 점멸 코루틴시작
        StartCoroutine(BlinkCo());
    }

    private IEnumerator BlinkCo()
    {
        //넉백 받는동안 이동 차단-코루틴 밖에 놓으니까 위에 이동값 초기화에 다 먹힘..
        move.canMove = false;

        float elapsed = 0f;
        SpriteRenderer spriteRenderer = core.spriteRenderer;
        Color original = spriteRenderer.color;

        //블링크 컬러랑 오리지널이랑 왔다갔다 할 반복문
        while (elapsed < blinkDuration)
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = original;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }
        //이동 풀어
        move.canMove = true;
        //마지막에 원래 색으로 돌려놓기
        spriteRenderer.color = original;
    }
}
