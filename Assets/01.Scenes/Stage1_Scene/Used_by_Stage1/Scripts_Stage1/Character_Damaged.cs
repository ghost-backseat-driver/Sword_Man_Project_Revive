using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//피격시 넉백+무적 플레이어랑 적이랑 같이 쓸거 염두
public class Character_Damaged : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;
    private Character_HP hp;

    [Header("피격 관련 설정")]
    [Header("넉백시 X 힘")]
    [SerializeField] private float knockbackForceX = 2.0f;
    [Header("넉백시 Y 힘")]
    [SerializeField] private float knockbackForceY = 1.0f;
    [Header("스턴 시간")]
    [SerializeField] private float stunTime = 0.5f;
    [Header("무적 시간")]
    [SerializeField] private float invincibleTime = 1.0f;

    [Header("피격무적 중 점멸-컬러")]
    //일단 RGBA 아무거나 넣어놔서 이거 보면서 체크
    [SerializeField] private Color blinkColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    //맞은 판정 발동, 무적타임 발동할거
    private bool isDamaged = false;
    private bool isInvincible = false;

    //HP에서 무적상태인지 체크할 용도의 불(읽기용) get
    public bool IsInvincible => isInvincible;

    //Player_ParryBox 에서 사용할 무적상태 set 함수
    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }

    private static readonly int damagedHash = Animator.StringToHash("isDamaged");

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();
        hp = GetComponent<Character_HP>();
    }

    // 피격 시 호출-> 공격자 위치 참고해서 넉백 방향 계산할 것
    public void OnHit(Vector2 attackerPos)
    {
        //무적이거나 죽었으면 하지마
        if (isInvincible || hp.isDead) return;
        StartCoroutine(DamagedCo(attackerPos));
    }

    private IEnumerator DamagedCo(Vector2 attackerPos)
    {
        isDamaged = true;
        isInvincible = true;

        //이동 봉쇄 해버리고 
        move.canMove = false;
        move.SetDir(Vector2.zero);

        //애니메이션 가져오고
        core.anim.SetTrigger(damagedHash);

        //넉백 방향 계산
        //맞는 위치 공격자 위치 기반으로 가져오고
        Vector2 hitDir = ((Vector2)transform.position - attackerPos).normalized;
        //넉백 위치는 맞는 맞는 위치에 x,y 힘 값 곱하고
        Vector2 knockback = new Vector2(hitDir.x * knockbackForceX, knockbackForceY);
        
        //이동값 초기화 한 다음
        core.rb.velocity = Vector2.zero;
        //밀어버려
        core.rb.AddForce(knockback, ForceMode2D.Impulse);

        //피격 사운드 가져와야되고..아 여기서 가져오면 또 통일되는데 일단 주석처리
        SoundManager.Instance.PlayEffect("Player_ATK2_SFX");

        //스턴타임 만큼 기다리고,
        yield return new WaitForSeconds(stunTime);

        //스턴 타임 끝나고 나서 이동봉쇄 풀자
        move.canMove = true;
        isDamaged = false;

        //무적시간 동안 점멸할 코루틴 가져와
        yield return StartCoroutine(InvincibleBlinkCo());
    }

    private IEnumerator InvincibleBlinkCo()
    {
        float elapsed = 0f;
        SpriteRenderer spriteRenderer = core.spriteRenderer;
        Color original = spriteRenderer.color;

        //블링크 컬러랑 오리지널이랑 왔다갔다 할 반복문
        while (elapsed < invincibleTime)
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = original;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.2f;
        }

        //마지막에 원래 색으로 돌려놓고 false 때려
        spriteRenderer.color = original;
        isInvincible = false;
    }
}
