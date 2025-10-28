using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

//캐릭터 무브 받고, 체이싱 기능 구현
//플레이어를 인식하면, 랜덤워크 스크립트를 비활성화
//추적시 이동 속도 증가
public class Enemy_Chaser : MonoBehaviour
{
    private Character_Move move;
    private Transform player;
    //랜덤워크 비활성화 용 변수
    private Enemy_RandomWalk randomWalk;

    //어쩔수 없는 애니메이터 변수 선언..
    private Animator anim;

    [Header("플레이어 추적 범위")]
    [SerializeField] private float chaseRange = 2.5f;

    [Header("인식할 레이어")]
    [SerializeField] private LayerMask playerLayer;

    [Header("추적속도 증가량*")]
    [SerializeField] private float chaseSpeed = 1.5f;

    private void Awake()
    {
        move = GetComponent<Character_Move>();
        randomWalk = GetComponent<Enemy_RandomWalk>();
    }

    //태그 비교 -소문자 한거 잊지말기 레이어는 대문자
    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if (playerObj != null) player = playerObj.transform;
    }

    private void Update()
    {
        if (player == null) return;

        //체이싱 관련
        if (IsPlayerInRange())
        {
            //범위 안이면, 랜덤워크 상태 비활성화
            if (randomWalk != null && randomWalk.enabled) randomWalk.enabled = false;
            Vector2 originDir = (player.position - transform.position).normalized;
            Vector2 plusDir = new Vector2(originDir.x * chaseSpeed, 0.0f); //추적속도 플러스->방향이라서 곱하기로, 더하기면 방향자체가 고꾸라짐
            move.SetDir(new Vector2(plusDir.x, 0)); // 추적속도로 X축 방향만 추적
        }
        else
        {
            //범위 밖이면, 랜덤워크 상태 다시 활성화
            if (randomWalk != null && !randomWalk.enabled) randomWalk.enabled = true;
            move.SetDir(Vector2.zero); // 추적 안함
        }
    }

    //인식범위 오버랩서클
    private bool IsPlayerInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, chaseRange, playerLayer);
        return hit != null;
    }

    //확인용 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
    /*
     현재 이 스크립트에 구현된 것
    -캐릭터 무브에 전달해줄 (Enemy)->(Player) 추적 기능+ 추적속도증가
    -랜덤워크 비활성화 기능 + 랜덤워크 없이 단독 사용 가능
     */
}
