using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Boss_WhirlWind_Control : MonoBehaviour
{
    [Header("보스 훨윈드")]
    [SerializeField] private GameObject whirlWind;

    private void Start()
    {
        whirlWind.SetActive(false);
    }
    public void EnableBosswhirlWindCollider()
    {
        whirlWind.SetActive(true);
        SoundManager.Instance.PlayEffect("AxeWind_SFX");
    }
    //보스 공격 콜라이더1 비활성화
    public void DisableBosswhirlWindCollider()
    {
        whirlWind.SetActive(false);
    }
}
