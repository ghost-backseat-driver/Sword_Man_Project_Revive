using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//움직이는(이동 점프 관련만) 로직 자체를 여기다가
//플레이어, 적 스크립트에 같이 사용할 것이므로, 입력받아야되는 구조는 제외
public class Character_Jump : MonoBehaviour
{
    private Character_Core core;  //코어 들고오기-항상 잊지말것

    [Header("점프력")]
    [SerializeField] private float jumpForce = 5.0f;

    [Header("바닥 체크 반지름")]
    public Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;

    [Header("체크대상 레이어->GroundLayer")]
    [SerializeField] private LayerMask groundLayer;

    public bool isGrounded;
    private bool jumpRequested;

    //적 플레이어 애니메이터 문자열 모두 동일한 문자열로 둘것-**
    //private static readonly int jumpHash = Animator.StringToHash("isJumping"); //애니메이터 나중에
    private void Start()
    {
        core = GetComponent<Character_Core>();  //어웨이크 생략-스타트에서 코어 불러오기-항상 잊지말것
    }
    //외부 점프관련 요청 받을 수 있는 public함수
    public void RequestJump()
    {
        if (isGrounded)
        {
            core.rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequested = true;
        }
    }
    private void FixedUpdate()
    {
        Jump();
        CheckGround();
    }

    //점프관련 함수-점프요청이 들어왔을때,
    private void Jump()
    {
        if (jumpRequested)
        {
            //core.anim.SetBool(jumpHash, true);
            jumpRequested = false;
        }

        //애니메이션 추가시에 발동할것
        //if (isGrounded && core.rb.velocity.y <= 0.05f)
        //{
        //    core.anim.SetBool(jumpHash, false);
        //}
    }

    //바닥체크 함수
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    /*
     현재 이 스크립트에 구현된 것
    -점프, 바닥 체크
    -Character_Jump 컴포넌트 참조시, 해당 로직에 전달할 입력값 생성할 것.
     */
}
