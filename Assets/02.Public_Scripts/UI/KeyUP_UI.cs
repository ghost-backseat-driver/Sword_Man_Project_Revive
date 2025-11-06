using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyUP_UI : MonoBehaviour
{
    public static KeyUP_UI Instance;

    //열쇠 누적용
    public int keyCount = 0;
    //열쇠 갯수 표시용
    public TMP_Text keyCountText;

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
        UpdateKeyUI();
    }
    //열쇠 획득시 UI 열쇠카운트 누적
    public void AddKey()
    {
        keyCount++;
        UpdateKeyUI();
    }
    //열쇠 사용시 UI 키카운트 감소
    public void UseKey() //이거 사용해버리면, 보스전에서 죽었을때, 저장데이터로 돌아올때, 키 없어서 문제됨->일단 냅둠
    {
        //열쇠 사용할때 1개 차감누적
        keyCount--;
        UpdateKeyUI();
    }

    //UI에 표시
    public void UpdateKeyUI()
    {
        if (keyCountText != null)
            keyCountText.text = keyCount.ToString();
    }
}
