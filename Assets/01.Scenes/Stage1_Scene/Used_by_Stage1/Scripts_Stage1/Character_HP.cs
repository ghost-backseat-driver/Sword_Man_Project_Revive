using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//캐릭터의 체력-> 피격 될 시, 체력이 0일 시 죽음 처리할 로직만
public class Character_HP : MonoBehaviour
{
    //코어 들고오기
    private Character_Core core;

    [Header("체력 설정")]
    [SerializeField] private int startHp = 1;

    [Header("피격 모션 있는지?")]
    [SerializeField] private bool damageAnim = false; //플레이어 에너미 모두 피격 모션 있으면 굳이 안해도 돼

    private int totalHp;

    //사망 후처리 위한 변수 추가 해야함(재시작 기능 및 텍스트 출력위함)
    public bool isDead { get; private set; } = false;
    
    //사망/피격 애니메이션 추가시 여기 주석 풀기
    //private static readonly int deadHash = Animator.StringToHash("isDead");
    //private static readonly int damagedHash = Animator.StringToHash("isDamaged");
    private void Start()
    {
        //코어 불러오기
        core = GetComponent<Character_Core>();
        totalHp = startHp;
    }

    // 피격 대미지 함수 (외부 입력값 받아올 것)
    public void TakeDamage(int damage)
    {
        totalHp -= damage;

        //피격 애니메이션 추가시 애니메이션 발동
        //if (damageAnim)
        //{
        //    core.anim.SetTrigger(damagedHash);
        //}

        if (totalHp <= 0)
        {
            Die();
        }
    }

    // 캐릭터 죽음처리 함수 + 후처리 위한 버츄얼 사용 private-> protected virtual
    protected virtual void Die()
    {
        //후처리 위해서 추가된 내용
        if (isDead) return; //중복방지용
        isDead = true;

        //캐릭터 무브값 제어
        Character_Move move = GetComponent<Character_Move>();
        if (move != null) move.enabled = false;

        //플레이어 컨트롤 입력값 제어
        Player_Control control = GetComponent<Player_Control>();
        if (control != null) control.enabled = false;

        //사망 모션 발동
        //core.anim.SetTrigger(deadHash);

        Invoke(nameof(DieDelay), 0.4f);

    }
    private void DieDelay() //코루틴써도 되는데, 간단한거니까 메모리 측면에서 인보크
    {
        gameObject.SetActive(false);
    }
    /*
    현재 이 스크립트에 구현된 것
    캐릭터의 Hp 관리(초기체력-대미지),사망처리(이동값 제한, 사망애니메이션, 오브젝트 비활성화)
    */
}
