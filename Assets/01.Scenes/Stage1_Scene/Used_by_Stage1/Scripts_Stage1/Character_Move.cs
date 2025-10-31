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

    //외부에서 추가로 줄 힘(넉백/공격이동제어 등에 쓰일거)
    private Vector2 externalForce = Vector2.zero;

    public bool canMove = true;

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

    //외부 힘 작용
    public void AddForce(Vector2 force)
    {
        externalForce = force;
    }

    //이동관련 함수
    private void Move()
    {
        //이동 불가상태일때,
        if (!canMove)
        {
            if (externalForce != Vector2.zero)
            {
                core.rb.velocity = externalForce;
                externalForce = Vector2.zero;
            }
            core.anim.SetFloat(moveHash, Mathf.Abs(core.rb.velocity.x));
            return;
        }

        //일반 이동
        Vector2 velocity = new Vector2(insertDir.x * moveSpeed, core.rb.velocity.y);

        //익스터널포스 추가 해주고
        velocity += externalForce; 
        core.rb.velocity = velocity;
        //익스터널포스 제로로 만들고
        externalForce = Vector2.zero;

        //애니메이션 할당
        core.anim.SetFloat(moveHash, Mathf.Abs(core.rb.velocity.x));
        //방향 전환
        if (insertDir.x < 0.0f) core.spriteRenderer.flipX = true;
        if (insertDir.x > 0.0f) core.spriteRenderer.flipX = false;
    }

    //외부에서 접근할 수 있는 Getter,Setter 추가 -> 세이브+업그레이드용
    public float GetMoveSpeed() => moveSpeed;
    public void SetMoveSpeed(float value) => moveSpeed = value;

}
