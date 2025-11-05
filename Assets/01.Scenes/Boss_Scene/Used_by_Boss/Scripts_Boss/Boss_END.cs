using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_END : MonoBehaviour
{
    //열려라 참깨 숨겨진 비밀문 비활성화용
    [Header("비활성화 할 타일맵")]
    [SerializeField] private GameObject openWall;

    //보스 UI 숨기고
    [Header("비활성화 할 타일맵")]
    [SerializeField] private GameObject BossUI;

    public void DisableWallCollider()
    {
        //메인 브금 정지
        SoundManager.Instance.StopBGM();
        //사운드 호출 추가할것
        SoundManager.Instance.PlayEffect("WallBreak_SFX");
        openWall.SetActive(false);
        BossUI.SetActive(false);
    }
}
