using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy3_ArrowShooter : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;
    private Character_HP hp;

    [Header("화살 프리팹")]
    [SerializeField] private Enemy3_Arrow arrowPrefab;

    [Header("발사 위치")]
    [SerializeField] private Transform firePoint;

    [Header("발사 간격")]
    [SerializeField] private float fireInterval = 1.5f;

    [Header("플레이어 인식 설정")]
    [SerializeField] private float detectRadius = 4.0f;

    [Header("인식할 레이어 -> Player")]
    [SerializeField] private LayerMask playerLayer;

    private static readonly int enemyAtk1Hash = Animator.StringToHash("isATK1");

    private bool isAttacking = false;
    private float nextFireTime = 0.0f;

    //플레이어 위치 저장용
    private Transform player;

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();

        hp = GetComponent<Character_HP>();

        // 플레이어 태그로 찾기 
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void OnEnable()
    {
        // 오브젝트 풀 생성
        GameManagers.Pool.CreatePool(arrowPrefab, 10);
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

        //감지를 여기 한번 더 넣는게 맞나..
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer);

        // 공격 가능 조건 체크
        if (hit !=null && Time.time >= nextFireTime)
        {
            StartCoroutine(ArrowAtkCo(enemyAtk1Hash));
        }
    }

    private IEnumerator ArrowAtkCo(int hash)
    {
        isAttacking = true;
        move.canMove = false;

        //추적 컴포넌트 안쓸거니까, 발사 시점에 플립해줘야되고
        if (player != null && !hp.isDead)// 죽은 상태에서도 플립되길래 급하게 추가
        {
            Vector2 dir = player.position - transform.position;
            core.spriteRenderer.flipX = dir.x < 0.0f;
        }

        //공격 애니메이션 실행
        core.anim.SetTrigger(hash);

        yield return null;
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;

        yield return new WaitForSeconds(animLength);

        // 다음 공격까지 쿨타임
        move.canMove = true;
        isAttacking = false;
        nextFireTime = Time.time + fireInterval;
    }

    //애니메이션 이벤트 콜라이더 활성화
    public void EnableEnemyArrowcollider()
    {
        //콜라이더 충돌처리는 Arrow에 있음 오직 활성화만 하면돼

        //플레이어 오버랩으로 감지
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer);
        if (hit == null) return; //감지 안되면 리턴

        // 방향 계산
        Vector2 direction = (hit.transform.position - firePoint.position).normalized;

        // 풀에서 총알 가져오기
        Enemy3_Arrow Arrow = GameManagers.Pool.GetFromPool(arrowPrefab);
        if (Arrow != null)
        {
            //발사위치로 이동
            Arrow.transform.position = firePoint.position;
            //화살 회전시켜야되는데 플립이 아니고 회전이야 
            Arrow.transform.right = direction; //화살촉이 오른쪽이니까 그냥 바라보는 방향 오른쪽으로 설정

            Arrow.Shoot(direction); // 방향 전달
        }
        //화살 발사 사운드 여기에 추가
        SoundManager.Instance.PlayEffect("ArrowSwish_SFX");

    }

    // 디버그용 감지범위
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
