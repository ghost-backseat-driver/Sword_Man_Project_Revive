using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AtkControl : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;

    [Header("공격 인식용 레이캐스트 설정")]
    [SerializeField] private float atkRange = 1.0f; // 공격 거리
    [SerializeField] private LayerMask playerLayer;    // 감지할 레이어->플레이어
    [SerializeField] private float atkCoolTime = 1.0f; // 공격 쿨타임

    //보스 어택 콜라이더용
    [Header("보스 공격1")]
    [SerializeField] private GameObject bossATK1;
    [Header("보스 공격2")]
    [SerializeField] private GameObject bossATK2; //아직 안만듦

    private static readonly int bossAtk1Hash = Animator.StringToHash("isATK1");

    private bool isAttacking = false;
    private float nextAtkTime = 0.0f;

    //플레이어 위치 저장용
    private Transform player;

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();

        bossATK1.SetActive(false);

        // 플레이어 태그로 찾기 
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        //플레이어 위치 모르면 중지
        if (player == null) return;

        // 공격 중이면 이동 중지
        if (isAttacking)
        {
            move.SetDir(Vector2.zero);
            return;
        }

        // 공격 가능 조건 체크
        if (Time.time >= nextAtkTime && IsPlayerInRange())
        {
            StartCoroutine(BossAtk1Co(bossAtk1Hash));
        }
    }

    private bool IsPlayerInRange()
    {
        // 적이 바라보는 방향 기준으로 레이 쏘기
        float dir = core.spriteRenderer.flipX ? -1f : 1f;
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * dir, atkRange, playerLayer);

        return hit.collider != null && hit.collider.CompareTag("player");
    }

    private IEnumerator BossAtk1Co(int hash)
    {
        isAttacking = true;
        move.canMove = false;

        // 공격 애니메이션 실행
        core.anim.SetTrigger(hash);

        yield return null;
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;

        yield return new WaitForSeconds(animLength);

        // 다음 공격까지 쿨타임
        move.canMove = true;
        isAttacking = false;
        nextAtkTime = Time.time + atkCoolTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (core == null) core = GetComponent<Character_Core>();
        if (core == null || core.spriteRenderer == null) return;
        Gizmos.color = Color.red;
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * dir * atkRange);
    }

    private void ColliderPos(GameObject atkCollider)
    {
        //에너미가 바라보는 기준 플립된 상태면 -1왼쪽 
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;

        //콜라이더의 로컬 위치
        Vector3 pos = atkCollider.transform.localPosition;
        //왼쪽이면 true -1.0f 곱해버리기->오른쪽 배치 기준 반전 시키기.
        pos.x = Mathf.Abs(pos.x) * dir;
        //계산된 위치 콜라이더에 적용
        atkCollider.transform.localPosition = pos;
    }

    //====================================================
    //보스 공격 콜라이더1 활성화
    public void EnableBossAttack1Collider()
    {
        ColliderPos(bossATK1);
        bossATK1.SetActive(true);
        SoundManager.Instance.PlayEffect("AxeSwing1_SFX");
    }
    //보스 공격 콜라이더1 비활성화
    public void DisableBossAttack1Collider()
    {
        bossATK1.SetActive(false);
    }
    //====================================================
    //보스 공격 콜라이더2 활성화 //아직 안만듦
    public void EnableBossAttack2Collider()
    {
        ColliderPos(bossATK2);
        bossATK2.SetActive(true);
        //SoundManager.Instance.PlayEffect("SpearSwing_SFX");
    }
    //보스 공격 콜라이더2 비활성화
    public void DisableBossAttack2Collider()
    {
        bossATK2.SetActive(false);
    }
    //====================================================
}
