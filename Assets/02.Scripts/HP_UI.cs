using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    [Header("HP������Ʈ �޷��ִ� ������Ʈ ����")]
    [SerializeField] private Character_HP character_HP;

    [Header("ü�¹� �����̴�")]
    [SerializeField] private Slider hpSlider;

    [Header("ü�¹� �پ��� �ӵ�")]
    [SerializeField] private float slideSpeed = 1.0f;

    //���� ü�¹ٿ��� ǥ�õ� Hp
    private float trueHP;

    private void Start()
    {
        //�ʱ� HP ����
        UpdateHP(character_HP.GetHP(), character_HP.GetMaxHP());
    }

    private void Update()
    {
        //���� HP ����
        UpdateHP(character_HP.GetHP(), character_HP.GetMaxHP());
    }

    private void UpdateHP(int current, int max)
    {
        trueHP = Mathf.Lerp(trueHP, current, Time.deltaTime * slideSpeed);
        hpSlider.value = trueHP / max;
    }
}
