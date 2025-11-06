using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BossPhase", menuName = "Boss/Phase")]
public class BossPhaseSO : ScriptableObject
{
    [Header("페이즈 이름")]
    public string phaseName = "Phase";

    [Header("페이즈 진입 HP 범위 (100비율로 0~1)")]
    [Range(0f, 1f)] public float minHPPercent = 0f;
    [Range(0f, 1f)] public float maxHPPercent = 1f;

    [Header("페이즈로 진입할 때 호출할 애니메이션-트리거이름")]
    public string enterAnimTrigger = "";

    [Header("진입 시 애니메이터를 완전히 정지시키고 싶을때 체크용도")]
    public bool stopAllAnimations = false;

    [Header("기존코드 행동 제어용-체크하면 false")]
    public bool disableAtkControl = false; //Boss_AtkControl 차단용
    public bool disableMove = false; //이동 차단용

    [Header("특수페이즈 유지 시간-훨윈드용")]
    public float specialDuration = 0.0f;

    [Header("특수페이즈 지속/해제 플래그")]
    public bool specialPhaseON = false;

    [Header("페이즈 진입 시 활성화할 게임오브젝트[리스트]-스폰 페이즈용")]
    public GameObject[] activateOnEnter;
}
