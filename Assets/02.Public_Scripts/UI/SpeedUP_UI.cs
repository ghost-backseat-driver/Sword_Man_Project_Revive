using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedUP_UI : MonoBehaviour
{
    public static SpeedUP_UI Instance;

    //스피드업 누적용
    public int speedUpCount = 0;
    //스피드업 횟수 표시용
    public TMP_Text speedUpCountText;

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
        UpdateSpeedUpUI();
    }
    //스피드업 획득시 UI 스피드업 카운트 누적
    public void AddSpeedUp()
    {
        speedUpCount++;
        UpdateSpeedUpUI();
    }

    //UI에 표시
    public void UpdateSpeedUpUI()
    {
        if (speedUpCountText != null)
            speedUpCountText.text = speedUpCount.ToString();
    }
}
