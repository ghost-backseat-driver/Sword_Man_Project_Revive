using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dead : MonoBehaviour
{
    private Character_HP hp;

    private void Start()
    {
        hp = GetComponent<Character_HP>();
    }

    private void Update()
    {
        if (hp != null && hp.isDead)
        {
            // 사망 후 한 번만 처리하기 위해 Update에서 처리 후 비활성화 또는 별도 플래그 추가 가능
            // 게임 오버 매니저 스크립트 추가해야함.
            GameOverManager manager = FindObjectOfType<GameOverManager>();
            if (manager != null)
            {
                manager.ShowGameOver();
            }
            //enabled = false; // 한번 처리 후 스크립트 비활성화
        }
    }
}
