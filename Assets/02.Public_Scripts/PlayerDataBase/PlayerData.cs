using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//저장할 플레이어의 데이터 구조 먼저 설정

public class PlayerData
{
    //위치 저장
    public Vector3 playerPos;
    //현재 체력 저장
    public int playerHP;
    //최대 체력 저장
    public int playerMaxHP;
    //이동속도 저장
    public float playerMoveSpeed;
    //보유코인 저장
    public int playerCoin;

    //업그레이드 진행사항 저장용
    public int atkUp; //공격력업UI용
    public int defUP; //방어력업UI용
    public int speedUP; //스피드업UI용
    public int keyUP; //열쇠업UI용

    //공격 업그레이드용- 이벤트 콜라이더 각각
    public int playerATK1Power;
    public int playerATK2_1Power;
    public int playerATK2_2Power;
}
