using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    [Header("HP컴포넌트 달려있는 오브젝트 연결")]
    [SerializeField] private Character_HP character_HP;

    [Header("체력바 슬라이더")]
    [SerializeField] private Slider hpSlider;

    [Header("체력바 줄어드는 속도")]
    [SerializeField] private float slideSpeed = 1.0f;

    //실제 체력바에서 표시될 Hp
    private float trueHP;

    private void Start()
    {
        //초기 HP 세팅
        UpdateHP(character_HP.GetHP(), character_HP.GetMaxHP());
    }

    private void Update()
    {
        //현재 HP 갱신
        UpdateHP(character_HP.GetHP(), character_HP.GetMaxHP());
    }

    private void UpdateHP(int current, int max)
    {
        trueHP = Mathf.Lerp(trueHP, current, Time.deltaTime * slideSpeed);
        hpSlider.value = trueHP / max;
    }
}
