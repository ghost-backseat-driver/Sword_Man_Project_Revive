using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

//플레이어 입력 관련한 것만 집어넣을것, 기능 자체는 캐릭터_스크립트들에 구현

public class Player_Control : MonoBehaviour
{
    //만들어둔 무브 스크립트 가져올 변수
    private Character_Core core;
    private Character_Move move;
    private Character_Jump jump;

    //캐릭터 어택 콜라이더용
    [Header("약공격")]
    [SerializeField] private GameObject normalATK;
    [Header("강공격")]
    [SerializeField] private GameObject strongATK;

    //공격 애니메이션 해쉬
    private static readonly int normalAtkHash = Animator.StringToHash("isATK1");
    private static readonly int strongAtkHash = Animator.StringToHash("isATK2");

    //공격중일때 입력값 막을 용도의 불문
    private bool isAttacking = false;

    //점프 사운드 넣을것
    //사운드매니저 만들어놨으니까, 점프요청할때 사운드 호출하면돼 아래쪽에
    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();
        jump = GetComponent<Character_Jump>();

        //공격용 콜라이더 비활성화로 시작
        normalATK.SetActive(false);
        strongATK.SetActive(false);
    }

    private void Update()
    {
        //공격중에는 입력값 무시
        if (isAttacking) return;

        //이동관련========================
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) dir.x = -1.0f;
        if (Input.GetKey(KeyCode.RightArrow)) dir.x = 1.0f;

        //이동 방향 전달===================
        move.SetDir(dir);

        //점프관련=========================
        if (Input.GetKeyDown(KeyCode.S))
        {
            //점프 요청
            jump.RequestJump(); //점프 사운드는 Character_Jump쪽에
        }
        //=================================

        //공격 인풋 == 약공격 ==============
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AtkType(normalAtkHash, "normal"));
        }
        //공격 인풋 == 강공격 ==============
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(AtkType(strongAtkHash, "strong"));
        }
        //=================================
    }

    private IEnumerator AtkType(int hash, string type)
    {
        //공격중, 이동불가상태
        isAttacking = true;
        move.canMove = false; 

        //움직임 완전 봉쇄
        move.SetDir(Vector2.zero);

        //현재 속도 제로로(관성으로 튀어나가는거 방지)
        core.rb.velocity = Vector2.zero;

        //현재 바라보는 방향 계산
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;
        //공격중 앞으로 살짝 밀어버리기 //여기 고쳐야됨 move 쪽이랑 같이
        core.rb.AddForce(new Vector2(dir * 3.0f, 0.0f),ForceMode2D.Impulse);

        core.anim.SetTrigger(hash);

        //yield return null; 한 프레임 기다렸다가 애니메이션 스테이트 전환 되게
        //첫번째 제어-공격 애니메이션 길이만큼 대기 -애니메이터 스테이트 공격길이 설정후 주석 풀어
        //AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        //float animLength = stateInfo.length;

        //두번째 제어-기다릴 시간 지정(임시)
        yield return new WaitForSeconds(1.0f); //animLength 길이로 변경할것 

        //공격상태 해제, 이동가능상태
        move.canMove = true;
        isAttacking = false;
    }

    //애니메이션 이벤트로 호출할 것들

    //약공격 콜라이더 활성화
    public void EnableNormalAttackCollider()
    {
        normalATK.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //약공격 콜라이더 비활성화
    public void DisableNormalAttackCollider()
    {
        normalATK.SetActive(false);
    }
    //강공격 콜라이더 활성화
    public void EnableStrongAttackCollider()
    {
        strongATK.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //강공격 콜라이더 비활성화
    public void DisableStrongAttackCollider()
    {
        strongATK.SetActive(false);
    }
}
