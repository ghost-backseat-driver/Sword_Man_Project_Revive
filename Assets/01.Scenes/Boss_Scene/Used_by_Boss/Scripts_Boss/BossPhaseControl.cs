using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//Character_HP랑 연결해서 체력에따른 BossPhaseSO 활성화,비활성화
[RequireComponent(typeof(Character_HP))]
public class BossPhaseControl : MonoBehaviour
{
    [Header("발동할 페이즈들 리스트")]
    public BossPhaseSO[] phases;

    [Header("참조할 컴포넌트")]//일단 관련있을거같은거만 집어넣음..보고 수정필요
    public Boss_AtkControl atkControl; //기존 공격 컨트롤러
    public Character_Move characterMove; //이동 제어용
    public Animator animator; //보스 애니메이터 core.anim쪽
    public Character_HP hp; //HP 무조건 있어야지

    //내부 상태
    private BossPhaseSO currentPhase = null; //현재페이즈 null상태로 시작
    private bool specialActive = false; //특수패턴도 비활성화 상태로 시작 플래그 세워야지

    private Coroutine specialPhaseCo;

    private void Reset()
    {
        //에디터에서 컴포넌트 자동 할당
        hp = GetComponent<Character_HP>();
        atkControl = GetComponent<Boss_AtkControl>();
        characterMove = GetComponent<Character_Move>();
        animator = GetComponent<Character_Core>()?.anim;
    }

    private void Start()
    {
        //널 났으면, 여기서 겟컴포넌트하고 시작
        if (hp == null) hp = GetComponent<Character_HP>();
        if (atkControl == null) atkControl = GetComponent<Boss_AtkControl>();
        if (characterMove == null) characterMove = GetComponent<Character_Move>();
        if (animator == null) animator = GetComponent<Character_Core>()?.anim;
    }

    private void Update()
    {
        if (hp == null || hp.isDead) return;
        if (specialActive) return; //플래그 고정, Hp변동할때 자동전환방지

        //hp퍼센트를 0~1비율로 맞춰주고
        float hpPercent = (float)hp.GetHP() / Mathf.Max(1, hp.GetMaxHP());

        //현재 페이즈와 다른SO를 찾기
        BossPhaseSO found = null; //찾지못함 플래그
        foreach (var p in phases)
        {
            //Hp비율에 맞는 페이즈 찾기 계속 돌려
            if (p == null) continue;
            if (hpPercent <= p.maxHPPercent && hpPercent >= p.minHPPercent)
            {
                found = p;
                break;
            }
        }

        if (found != currentPhase)
        {
            //현재페이즈 나가고,찾은페이즈 들어오고
            ExitCurrentPhase();
            EnterPhase(found);
        }
    }

    //페이즈 들어갈때 기능
    private void EnterPhase(BossPhaseSO phase)
    {
        if (phase == null)
        {
            currentPhase = null;
            return;
        }

        currentPhase = phase;

        //애니메이션 처리
        if (animator != null)
        {
            if (phase.stopAllAnimations)
            {
                //애니메이터 끄면 모든 애니메이션 클립 멈춤!
                animator.enabled = false;
            }
            else
            {
                //애니메이터가 비활성화 상태면 다시 활성화
                if (!animator.enabled)
                {
                    animator.enabled = true;
                }

                //enterAnimTrigger가 있으면 트리거로 전환
                if (!string.IsNullOrEmpty(phase.enterAnimTrigger))
                {
                    animator.SetTrigger(phase.enterAnimTrigger);
                }
            }
        }

        //물리행동 제어-지멋대로 움직이면 곤란
        if (atkControl != null)
        {
            atkControl.enabled = !phase.disableAtkControl;
        }

        if (characterMove != null)
        {
            characterMove.canMove = !phase.disableMove;
        }

        //활성화할 게임오브젝트 켜기-수정필요
        if (phase.activateOnEnter != null)
        {
            //배열 길이만큼 싹 돌면서
            for (int i = 0; i < phase.activateOnEnter.Length; i++)
            {
                //하나씩 꺼내자
                GameObject spawnSkull = phase.activateOnEnter[i];
                if (spawnSkull != null)
                {
                    //단일 오브젝트만 들어가는 문제->그냥 프리팹으로 만들어서 생성해버리자
                    Instantiate(spawnSkull, transform.position, Quaternion.identity);
                }
            }
        }
        //시간 기반 페이즈패턴 처리
        if (phase.specialPhaseON)
        {
            specialActive = true;
            //지속시간 관련
            if (phase.specialDuration > 0.0f)
            {
                if (specialPhaseCo != null)
                {
                    StopCoroutine(specialPhaseCo);
                }
                specialPhaseCo = StartCoroutine(SpecialPhaseTimerCo(phase.specialDuration));
            }
        }
    }

    private IEnumerator SpecialPhaseTimerCo(float duration)
    {
        yield return new WaitForSeconds(duration);
        //다시 페이즈 전환허용
        specialActive = false;
        //제어도 복구
        ExitCurrentPhase();
        //현재 페이즈 종료
        currentPhase = null;
    }

    private void ExitCurrentPhase()
    {
        if (currentPhase == null) return;

        //애니메이션 복구용 animator활성화
        if (animator != null && currentPhase.stopAllAnimations)
        {
            animator.enabled = true;
        }

        //물리행동 제어한거 복구용
        if (atkControl != null && currentPhase.disableAtkControl)
            atkControl.enabled = true;

        if (characterMove != null && currentPhase.disableMove)
            characterMove.canMove = true;
    }
}
