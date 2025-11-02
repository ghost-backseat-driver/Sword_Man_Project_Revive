using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefUP_UI : MonoBehaviour
{
    public static DefUP_UI Instance;

    //디펜스업 누적용
    public int defUpCount = 0;
    //디펜스업 횟수 표시용
    public TMP_Text defUpCountText;

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
        UpdateDefUpUI();
    }
    //디펜스업 획득시 UI 디펜스업 카운트 누적
    public void AddDefUp()
    {
        defUpCount++;
        UpdateDefUpUI();
    }

    //UI에 표시
    public void UpdateDefUpUI()
    {
        if (defUpCountText != null)
            defUpCountText.text = defUpCount.ToString();
    }
}
