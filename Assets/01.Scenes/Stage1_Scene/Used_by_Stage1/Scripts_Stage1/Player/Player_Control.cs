using System.Collections;
using System.Collections.Generic;
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
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) dir.x = -1.0f;
        if (Input.GetKey(KeyCode.RightArrow)) dir.x = 1.0f;

        //이동 방향 전달
        move.SetDir(dir);

        if (Input.GetKeyDown(KeyCode.S))
        {
            //점프 요청
            jump.RequestJump();
        }
        //공격 인풋 == 약공격
        if (Input.GetKeyDown(KeyCode.A))
        {
            //어택1 콜라이더 활성화
            normalATK.SetActive(true);
            //애니메이션 (트리거)
            core.anim.SetTrigger(normalAtkHash);
            //사운드 추가 할 것
            SoundManager.Instance.PlayEffect("swordSwingSFX1");// 사운드 조절할 방법 찾아야돼 지금 누르는대로 재생되잖아.
            //일정시간뒤 비활성화
        }
        //공격 인풋 == 강공격
        if (Input.GetKeyDown(KeyCode.D))
        {
            //어택2 콜라이더 활성화
            strongATK.SetActive(true);
            //애니메이션 (트리거)
            core.anim.SetTrigger(strongAtkHash);
            //사운드 추가할 것
            SoundManager.Instance.PlayEffect("swordSwingSFX1");
        }
    }
    /*
    현재 이 스크립트에 구현된 것
    플레이어 입력값(x축이동,점프)을 Character_Move,Jump에 전달
     */
}
