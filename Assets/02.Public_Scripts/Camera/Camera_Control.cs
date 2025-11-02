using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    [Header("시네머신 메인카메라")]
    [SerializeField] private CinemachineVirtualCamera vcamMain;
    [Header("시네머신 패리카메라")]
    [SerializeField] private CinemachineVirtualCamera vcamParry;

    private bool isLinked = false;

    private void Update()
    {
        //링크 상태면 연결끝
        if (isLinked) return;

        //태그로 플레이어 찾아서
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if (player != null)
        {
            //가상 카메라 Follow/LookAt에 플레이어 위치 연결
            if (vcamMain != null)
            {
                vcamMain.Follow = player.transform;
                vcamMain.LookAt = player.transform;
            }

            if (vcamParry != null)
            {
                vcamParry.Follow = player.transform;
                vcamParry.LookAt = player.transform;
            }

            //링크 완료
            isLinked = true;
        }
    }
}
