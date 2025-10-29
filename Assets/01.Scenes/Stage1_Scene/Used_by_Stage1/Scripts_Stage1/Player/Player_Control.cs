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

    //플레이어 어택 콜라이더용
    [Header("약공격")]
    [SerializeField] private GameObject normalATK;
    [Header("강공격1번 프레임")]
    [SerializeField] private GameObject strongATK1st;
    [Header("강공격2번 프레임")]
    [SerializeField] private GameObject strongATK2nd;

    //플레이어 패링 콜라이더용
    [Header("방어")]
    [SerializeField] private GameObject parry;

    //공격 애니메이션 해쉬
    private static readonly int normalAtkHash = Animator.StringToHash("isATK1");
    private static readonly int strongAtkHash = Animator.StringToHash("isATK2");

    //패링 애니메이션 해쉬
    private static readonly int parryHash = Animator.StringToHash("isParry");

    //공격중일때 입력값 막을 용도의 불문
    private bool isAttacking = false;

    //점프 사운드 넣을것
    //사운드매니저 만들어놨으니까, 점프요청할때 사운드 호출하면돼 아래쪽에
    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();
        jump = GetComponent<Character_Jump>();
    }

    private void Update()
    {
        //공격중에는 입력값 무시
        if (isAttacking) return;

        //공격 인풋 == 약공격 ==============
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AtkTypeCo(normalAtkHash, "normal"));
        }
        //공격 인풋 == 강공격 ==============
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(AtkTypeCo(strongAtkHash, "strong"));
        }
        //=================================

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

        //패링관련==========================
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(ParryCo(parryHash));
        }
        //==================================
    }

    //공격관련 코루틴
    private IEnumerator AtkTypeCo(int hash, string type)
    {
        //공격중, 이동불가상태
        isAttacking = true;
        move.canMove = false; 

        //움직임 완전 봉쇄
        move.SetDir(Vector2.zero); //이거 안되는듯? 참조하면서 업데이트 거치게 되니까 엉킨듯..? 일단 납둬

        //현재 속도 제로로(관성으로 튀어나가는거 방지)
        core.rb.velocity = Vector2.zero;

        //현재 바라보는 방향 계산
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;
        //공격중 앞으로 살짝 밀어버리기 //여기 고쳐야됨 move 쪽이랑 같이
        core.rb.AddForce(new Vector2(dir * 3.0f, 0.0f),ForceMode2D.Impulse);

        //애니메이션
        core.anim.SetTrigger(hash);

        yield return null; //한 프레임 기다렸다가 애니메이션 스테이트 전환 되게
        //첫번째 제어-공격 애니메이션 길이만큼 대기 -애니메이터 스테이트 공격길이 설정후 주석 풀어
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;

        //두번째 제어-기다릴 시간 지정
        yield return new WaitForSeconds(animLength); //animLength 길이로 변경할것 

        //공격상태 해제, 이동가능상태
        move.canMove = true;
        isAttacking = false;
    }

    //패링관련 코루틴
    private IEnumerator ParryCo(int hash)
    {
        //이동 봉쇄하고
        move.canMove = false;
        move.SetDir(Vector2.zero);
        core.rb.velocity = Vector2.zero;

        //애니메이션
        core.anim.SetTrigger(hash);
        
        yield return null;
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;
        yield return new WaitForSeconds(animLength);

        move.canMove= true;
    }

    //애니메이션 이벤트로 호출할 것들=========================

    //플레이어 방향에 맞게 이벤트 콜라이더 플립용
    private void FlipCollider(GameObject atkCollider)
    {
        //콜라이더의 로컬 위치
        Vector3 pos = atkCollider.transform.localPosition;
        //왼쪽이면 true -1.0f 곱해버리기->오른쪽 배치 기준 반전 시키기.
        pos.x = Mathf.Abs(pos.x) * (core.spriteRenderer.flipX ? -1.0f : 1.0f);
        //계산된 위치 콜라이더에 적용
        atkCollider.transform.localPosition = pos;
    }

    //약공격 콜라이더 활성화
    public void EnableNormalAttackCollider()
    {
        FlipCollider(normalATK);
        normalATK.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //약공격 콜라이더 비활성화
    public void DisableNormalAttackCollider()
    {
        normalATK.SetActive(false);
    }
    //강공격1번 프레임 콜라이더 활성화
    public void EnableStrongAttack1stCollider()
    {
        FlipCollider(strongATK1st);
        strongATK1st.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //강공격1번 프레임 콜라이더 비활성화
    public void DisableStrongAttack1stCollider()
    {
        strongATK1st.SetActive(false);
    }

    //강공격2번 프레임 콜라이더 활성화
    public void EnableStrongAttack2ndCollider()
    {
        FlipCollider(strongATK2nd);
        strongATK2nd.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //강공격2번 프레임 콜라이더 비활성화
    public void DisableStrongAttack2ndCollider()
    {
        strongATK2nd.SetActive(false);
    }

    //패링 프레임 콜라이더 활성화
    public void EnableParryCollider()
    {
        //플립안할거
        parry.SetActive(true);
        SoundManager.Instance.PlayEffect("Player_ShieldReady_SFX");
    }
    //패링 프레임 콜라이더 비활성화
    public void DisableParryCollider()
    {
        parry.SetActive(false);
    }
}
