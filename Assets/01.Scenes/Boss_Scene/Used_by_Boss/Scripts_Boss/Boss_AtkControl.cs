using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AtkControl : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;

    [Header("공격 인식용 레이캐스트 설정")]
    [SerializeField] private float atkRange1 = 1.0f; //공격1 거리x(수평)
    [SerializeField] private float atkRange2 = 1.0f; //공격2 거리x(수직)

    [SerializeField] private LayerMask playerLayer;    //감지할 레이어->플레이어
    
    [SerializeField] private float atkCoolTime1 = 1.0f; //공격1 쿨타임
    [SerializeField] private float atkCoolTime2 = 1.0f; //공격2 쿨타임

    //보스 어택 콜라이더용
    [Header("보스 공격1")]
    [SerializeField] private GameObject bossATK1;
    [Header("보스 공격2")]
    [SerializeField] private GameObject bossATK2;

    private static readonly int bossAtk1Hash = Animator.StringToHash("isATK1");
    private static readonly int bossAtk2Hash = Animator.StringToHash("isATK2");

    private bool isAttacking = false;
    
    private float nextAtkTime1 = 0.0f;
    private float nextAtkTime2 = 0.0f;

    //플레이어 위치 저장용
    private Transform player;

    //이벤트 콜라이더 위치 갱신용
    private Vector3 originCPos;

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();

        bossATK1.SetActive(false);
        bossATK2.SetActive(false);

        //플레이어 태그로 찾기 
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

        //공격 중이면 이동 중지
        if (isAttacking)
        {
            move.SetDir(Vector2.zero);
            return;
        }

        //수직 공격 먼저체크
        if (IsPlayerInRangeAtk2() && Time.time >= nextAtkTime2)
        {
            StartCoroutine(BossAtk2Co(bossAtk2Hash));
        }
        if (IsPlayerInRangeAtk1() && Time.time >= nextAtkTime1)
        {
            StartCoroutine(BossAtk1Co(bossAtk1Hash));
        }
    }

    //수평공격 레이
    private bool IsPlayerInRangeAtk1()
    {
        //보스가 바라보는 방향 기준으로 레이 쏘기
        float dir = core.spriteRenderer.flipX ? -1f : 1f;
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * dir, atkRange1, playerLayer);

        return hit.collider != null && hit.collider.CompareTag("player");
    }

    //수직공격 레이
    private bool IsPlayerInRangeAtk2()
    {
        //보스 기준 앞에 박스로 영역체크
        float dir = core.spriteRenderer.flipX ? -1f : 1f;
        Vector2 origin = (Vector2)transform.position+new Vector2(dir * atkRange2, 0.0f);
        //박스크기
        Vector2 size = new Vector2(0.01f, 0.5f);

        RaycastHit2D hit = Physics2D.BoxCast(origin,size,0.0f,Vector2.zero,0.0f,playerLayer);
        return hit.collider != null && hit.collider.CompareTag("player");
    }

    private IEnumerator BossAtk1Co(int hash)
    {
        isAttacking = true;
        move.canMove = false;

        //공격 애니메이션 실행
        core.anim.SetTrigger(hash);

        yield return null;
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;

        yield return new WaitForSeconds(animLength);

        //다음 공격까지 쿨타임
        move.canMove = true;
        isAttacking = false;
        nextAtkTime1 = Time.time + atkCoolTime1;
    }

    private IEnumerator BossAtk2Co(int hash)
    {
        isAttacking = true;
        move.canMove = false;

        //공격 애니메이션 실행
        core.anim.SetTrigger(hash);

        yield return null;
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;

        yield return new WaitForSeconds(animLength);

        //다음 공격까지 쿨타임
        move.canMove = true;
        isAttacking = false;
        nextAtkTime2 = Time.time + atkCoolTime2;
    }

    private void OnDrawGizmosSelected()
    {
        //수평용
        if (core == null) core = GetComponent<Character_Core>();
        if (core == null || core.spriteRenderer == null) return;
        Gizmos.color = Color.red;
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * dir * atkRange1);

        //수직용
        Gizmos.color = Color.yellow;
        Vector2 origin = (Vector2)transform.position + new Vector2(dir * atkRange2, 0.0f);
        Vector2 size = new Vector2(0.01f, 0.5f);
        Gizmos.DrawCube(origin, size);
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
        //콜라이더 위치 엄청살짝 갱신
        originCPos = bossATK1.transform.localPosition;
        bossATK1.transform.localPosition = originCPos + new Vector3(0.001f,0.0f,0.0f);
        bossATK1.SetActive(true);
        bossATK1.transform.localPosition = originCPos;
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
        //위치갱신
        originCPos = bossATK2.transform.localPosition;
        bossATK2.transform.localPosition = originCPos + new Vector3(0.001f, 0.0f, 0.0f);
        bossATK2.SetActive(true);
        bossATK2.transform.localPosition = originCPos;
        SoundManager.Instance.PlayEffect("AxeSmash_SFX");
    }
    //보스 공격 콜라이더2 비활성화
    public void DisableBossAttack2Collider()
    {
        bossATK2.SetActive(false);
    }
    //====================================================
}
