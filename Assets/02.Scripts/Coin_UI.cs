using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin_UI : MonoBehaviour
{
    public static Coin_UI Instance;

    //���� ������
    public int coinCount = 0;
    //���� ���� ǥ�ÿ�
    public TMP_Text coinCountText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
        UpdateCoinUI();
    }
    //���� ȹ��� UI ����ī��Ʈ ���� 
    public void AddCoin()
    {
        coinCount++;
        UpdateCoinUI();
    }
    //���� ���� UI Űī��Ʈ ����
    public void UseCoin()
    {
        //���� ����Ҷ� ��ŭ �����ϴ��� �ϴ� �ӽ�
        UpdateCoinUI();
    }

    //UI�� ǥ��
    private void UpdateCoinUI()
    {
        if (coinCountText != null)
            coinCountText.text = coinCount.ToString();
    }
}
