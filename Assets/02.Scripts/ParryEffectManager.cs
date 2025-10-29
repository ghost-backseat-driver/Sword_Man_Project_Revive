using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//시네머신 활용해서 패리 이펙트 연출할 스크립트
public class ParryEffectManager : MonoBehaviour
{
    [Header("시네머신 메인카메라")]
    [SerializeField] private CinemachineVirtualCamera vcamMain;
    [Header("시네머신 패링성공 카메라")]
    [SerializeField] private CinemachineVirtualCamera vcamParry;

    [Header("화면 슬로우 속도")]
    [SerializeField] private float slowScale = 0.5f;
    [Header("화면 슬로우 시간")]
    [SerializeField] private float slowTime = 0.5f;

    //카메라 쉐이킹 부분 임펄스소스 쓰면된데
    [Header("카메라 흔들림 (임펄스소스)")]
    [SerializeField] private CinemachineImpulseSource impulseSource;

    //느려졌다가 원복해야 되니까, 원래 화면속도 저장용
    private float originalTimeScale;

    public void OnParrySuccess()
    {
        StartCoroutine(ParryEffectCO());
    }

    private IEnumerator ParryEffectCO()
    {

        //슬로우모션 진입==
        //오리지날 = 타임스케일=> 슬로우스케일
        originalTimeScale = Time.timeScale;
        Time.timeScale = slowScale;

        //카메라 우선순위 변경-패리 카메라 priority 값이 크면돼
        vcamParry.Priority = 30;

        //임펄스(카메라 흔들림)
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

        //슬로우 시간만큼 잠시 대기하고,
        yield return new WaitForSecondsRealtime(slowTime);

        //타임스케일 오리지널로 복구
        Time.timeScale = originalTimeScale;

        //카메라 우선순위 메인으로 복구
        vcamParry.Priority = 10;
    }
}
