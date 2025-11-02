using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AtkUP_UI : MonoBehaviour
{
    public static AtkUP_UI Instance;

    //어택업 누적용
    public int atkUpCount = 0;
    //어택업 횟수 표시용
    public TMP_Text atkUpCountText;

    private void Awake()
    {
        //싱글톤 초기화
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateAtkUpUI();
    }
    //어택업 획득시 UI 어택업 카운트 누적
    public void AddAtkUp()
    {
        atkUpCount++;
        UpdateAtkUpUI();
    }

    //UI에 표시
    public void UpdateAtkUpUI()
    {
        if (atkUpCountText != null)
            atkUpCountText.text = atkUpCount.ToString();
    }
}
