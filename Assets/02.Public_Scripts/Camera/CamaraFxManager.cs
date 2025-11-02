using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//시네머신 활용해서 패리 이펙트 연출할 스크립트
public class CamaraFxManager : MonoBehaviour
{
    [Header("시네머신 메인카메라")]
    [SerializeField] private CinemachineVirtualCamera vcamMain;
    [Header("시네머신 효과발동 카메라")]
    [SerializeField] private CinemachineVirtualCamera vcamFX;

    [Header("화면 슬로우 속도")]
    [SerializeField] private float slowScale = 0.5f;
    [Header("화면 슬로우 시간")]
    [SerializeField] private float slowTime = 0.5f;

    //카메라 쉐이킹 부분 임펄스소스 쓰면된데
    [Header("카메라 흔들림 (임펄스소스)")]
    [SerializeField] private CinemachineImpulseSource impulseSource;

    //느려졌다가 원복해야 되니까, 원래 화면속도 저장용
    private float originalTimeScale;

    //슬로우 타임 횟수 중첩 카운트 - 중첩버그 해결용 추가
    private int slowCount = 0;

    private void Awake()
    {
        //타임스케일 초기값 저장하고 시작
        originalTimeScale = Time.timeScale;
    }

    public void OnCameraFX()
    {
        StartCoroutine(CamaraFxCO());
    }

    private IEnumerator CamaraFxCO()
    {
        //코루틴 시작될때 마다 중첩 스택 쌓기
        slowCount++;

        //슬로우모션 진입==
        //오리지날 = 타임스케일=> 슬로우스케일
        Time.timeScale = slowScale;
        //물리연산 업데이트 속도 조절 0.02가 가장 적당한듯
        Time.fixedDeltaTime = 0.02f * Time.deltaTime;

        //카메라 우선순위 변경-패리 카메라 priority 값이 크면돼
        vcamFX.Priority = 30;

        //임펄스(카메라 흔들림)
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }

        //슬로우 시간만큼 잠시 대기하고,
        yield return new WaitForSecondsRealtime(slowTime);

        //중첩 감소 시켜버려 - 강제 풀어버리기
        slowCount--;
        //그래도 안풀릴수 있잖아 
        if (slowCount <= 0)
        {
            //그냥 0으로 만들어버려
            slowCount = 0;
            //중첩 풀고나서 타임스케일 원복
            Time.timeScale = originalTimeScale;
            Time.fixedDeltaTime = 0.02f;
        }

        //카메라 우선순위 메인으로 복구
        vcamFX.Priority = 10;
    }
}
