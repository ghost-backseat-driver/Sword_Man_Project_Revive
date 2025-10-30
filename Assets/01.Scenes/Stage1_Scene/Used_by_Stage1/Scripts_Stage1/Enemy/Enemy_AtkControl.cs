using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AtkControl : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;

    [Header("공격 인식용 레이캐스트 설정")]
    [SerializeField] private float atkRange = 1.0f; // 공격 거리
    [SerializeField] private LayerMask playerLayer;    // 감지할 레이어->플레이어
    [SerializeField] private float atkCoolTime = 1.0f; // 공격 쿨타임

    //에너미 어택 콜라이더용
    [Header("에너미 공격1")]
    [SerializeField] private GameObject enemyATK1;
    [Header("에너미 공격2")]
    [SerializeField] private GameObject enemyATK2;

    private static readonly int enemyAtk1Hash = Animator.StringToHash("isATK1");

    private bool isAttacking = false;
    private float nextAtkTime = 0.0f;

    //플레이어 위치 저장용
    private Transform player;

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();

        enemyATK1.SetActive(false);

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
            StartCoroutine(EnemyAtkCo(enemyAtk1Hash));
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

    private IEnumerator EnemyAtkCo(int hash)
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
    //에너미1 공격 콜라이더 활성화
    public void EnableEnemyAttack1Collider()
    {
        ColliderPos(enemyATK1);
        enemyATK1.SetActive(true);
        SoundManager.Instance.PlayEffect("Heavy_SwordSwing_SFX");
    }
    //에너미1 공격 콜라이더 비활성화
    public void DisableEnemyAttack1Collider()
    {
        enemyATK1.SetActive(false);
    }
    //====================================================
    //에너미2 공격 콜라이더 활성화
    public void EnableEnemyAttack2Collider()
    {
        ColliderPos(enemyATK1);
        enemyATK1.SetActive(true);
        SoundManager.Instance.PlayEffect("SpearSwing_SFX");
    }
    //에너미2 공격 콜라이더 비활성화
    public void DisableEnemyAttack2Collider()
    {
        enemyATK1.SetActive(false);
    }
    //====================================================
    //에너미2 에 붙일 일반 콜라이더 활성화
    public void EnableEnemyshieldCollider()
    {
        ColliderPos(enemyATK2);
        enemyATK2.SetActive(true);
    }
    //에너미2 공격 콜라이더 비활성화
    public void DisableEnemyshieldCollider()
    {
        enemyATK2.SetActive(false);
    }
}
