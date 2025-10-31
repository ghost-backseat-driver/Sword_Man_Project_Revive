using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin_UI : MonoBehaviour
{
    public static Coin_UI Instance;

    //키 누적용
    public int coinCount = 0;
    //키 갯수 표시용
    public TMP_Text coinCountText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }
    //키 획득시 UI 키카운트 누적 
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinUI();
    }
    //키 사용시 UI 키카운트 감소
    public void UseCoin()
    {
        //코인 사용할때 얼만큼 지불하는지 일단 임시
        UpdateCoinUI();
    }

    //UI에 표시
    private void UpdateCoinUI()
    {
        if (coinCountText != null)
            coinCountText.text = coinCount.ToString();
    }
}
