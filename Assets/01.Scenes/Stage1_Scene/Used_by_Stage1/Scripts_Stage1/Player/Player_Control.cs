using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 입력 관련한 것만 집어넣을것, 기능 자체는 캐릭터_스크립트들에 구현

public class Player_Control : MonoBehaviour
{
    //만들어둔 무브 스크립트 가져올 변수
    private Character_Move move;
    private Character_Jump jump;

    //점프 사운드 넣을것
    //사운드매니저 만들어놨으니까, 점프요청할때 사운드 호출하면돼 아래쪽에
    private void Start()
    {
        move = GetComponent<Character_Move>();
        jump = GetComponent<Character_Jump>();

    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) dir.x = -1.0f;
        if (Input.GetKey(KeyCode.RightArrow)) dir.x = 1.0f;

        //이동 방향 전달
        move.SetDir(dir);

        if (Input.GetKeyDown(KeyCode.S))
        {
            //점프 요청
            jump.RequestJump();
            //여기에 사운드매니저 호출
        }
    }
    /*
    현재 이 스크립트에 구현된 것
    플레이어 입력값(x축이동,점프)을 Character_Move,Jump에 전달
     */
}
