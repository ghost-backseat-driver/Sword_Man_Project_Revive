using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//저장할 플레이어의 데이터 구조 먼저 설정

public class PlayerData
{
    //위치 저장
    public Vector3 playerPos;
    //체력 저장
    public int playerHP;
    //이동속도 저장
    public float playerMoveSpeed;
    //보유코인 저장
    public int playerCoin;

    //공격 업그레이드용- 이벤트 콜라이더 각각
    public int playerATK1Power;
    public int playerATK2_1Power;
    public int playerATK2_2Power;
}
