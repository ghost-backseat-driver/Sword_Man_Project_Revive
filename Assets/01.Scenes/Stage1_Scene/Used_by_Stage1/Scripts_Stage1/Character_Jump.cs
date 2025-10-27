using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����̴�(�̵� ���� ���ø�) ���� ��ü�� ����ٰ�
//�÷��̾�, �� ��ũ��Ʈ�� ���� ����� ���̹Ƿ�, �Է¹޾ƾߵǴ� ������ ����
public class Character_Jump : MonoBehaviour
{
    private Character_Core core;  //�ھ� ������-�׻� ��������

    [Header("������")]
    [SerializeField] private float jumpForce = 5.0f;

    [Header("�ٴ� üũ ������")]
    public Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;

    [Header("üũ��� ���̾�->GroundLayer")]
    [SerializeField] private LayerMask groundLayer;

    public bool isGrounded;
    private bool jumpRequested;

    //�� �÷��̾� �ִϸ����� ���ڿ� ��� ������ ���ڿ��� �Ѱ�-**
    //private static readonly int jumpHash = Animator.StringToHash("isJumping"); //�ִϸ����� ���߿�
    private void Start()
    {
        core = GetComponent<Character_Core>();  //�����ũ ����-��ŸƮ���� �ھ� �ҷ�����-�׻� ��������
    }
    //�ܺ� �������� ��û ���� �� �ִ� public�Լ�
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

    //�������� �Լ�-������û�� ��������,
    private void Jump()
    {
        if (jumpRequested)
        {
            //core.anim.SetBool(jumpHash, true);
            jumpRequested = false;
        }

        //�ִϸ��̼� �߰��ÿ� �ߵ��Ұ�
        //if (isGrounded && core.rb.velocity.y <= 0.05f)
        //{
        //    core.anim.SetBool(jumpHash, false);
        //}
    }

    //�ٴ�üũ �Լ�
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    /*
     ���� �� ��ũ��Ʈ�� ������ ��
    -����, �ٴ� üũ
    -Character_Jump ������Ʈ ������, �ش� ������ ������ �Է°� ������ ��.
     */
}
