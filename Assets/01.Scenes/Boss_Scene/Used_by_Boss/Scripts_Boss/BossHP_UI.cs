using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP_UI : MonoBehaviour
{
    [Header("HP컴포넌트 달려있는 보스 오브젝트")]
    [SerializeField] private Character_HP boss_HP; //씬에 배치할거니까 인스펙터에서 적용

    [Header("체력바 슬라이더")]
    [SerializeField] private Slider bossHpSlider;

    [Header("체력바 줄어드는 속도")]
    [SerializeField] private float slideSpeed = 1.0f;

    //실제 체력바에서 표시될 Hp
    private float trueHP;

    private void Update()
    {
        UpdateHP(boss_HP.GetHP(), boss_HP.GetMaxHP());
    }

    public void UpdateHP(int current, int max)
    {
        trueHP = Mathf.Lerp(trueHP, current, Time.deltaTime * slideSpeed);
        bossHpSlider.value = trueHP / max;
    }
}
