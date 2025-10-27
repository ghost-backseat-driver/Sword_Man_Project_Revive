using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//움직이는(이동 관련만) 로직 자체를 여기다가
//플레이어, 적 스크립트에 같이 사용할 것이므로, 입력받아야되는 구조는 제외
public class Character_Move : MonoBehaviour
{
    private Character_Core core;  //코어 들고오기-항상 잊지말것

    [Header("이동")]
    [SerializeField] private float moveSpeed = 5.0f;

    //외부에서 전달 받을 이동관련 입력 변수
    private Vector2 insertDir = Vector2.zero;

    //적 플레이어 애니메이터 문자열 모두 동일한 문자열로 둘것-**
    private static readonly int moveHash = Animator.StringToHash("Speed"); //애니메이터 나중에
    private void Start()
    {
        core = GetComponent<Character_Core>();  //어웨이크 생략-스타트에서 코어 불러오기-항상 잊지말것
    }
    //외부 이동관련 입력값 전달받을 public함수
    public void SetDir(Vector2 dir)
    {
        insertDir = dir;
    }
    private void FixedUpdate()
    {
        Move();
    }

    //이동관련 함수
    private void Move()
    {
        core.rb.velocity = new Vector2(insertDir.x * moveSpeed, core.rb.velocity.y);
        //애니메이션 할당
        core.anim.SetFloat(moveHash, Mathf.Abs(core.rb.velocity.x));
        //방향 전환
        if (insertDir.x < 0.0f) core.spriteRenderer.flipX = true;
        if (insertDir.x > 0.0f) core.spriteRenderer.flipX = false;
    }
    /*
     현재 이 스크립트에 구현된 것
    -이동,방향전환 로직, 이동 애니메이션까지
    -Character_Move 컴포넌트 참조시, 해당 로직에 전달할 입력값 생성할 것.
     */
}
