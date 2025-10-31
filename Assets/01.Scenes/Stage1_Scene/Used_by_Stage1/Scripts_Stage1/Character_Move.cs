using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����̴�(�̵� ���ø�) ���� ��ü�� ����ٰ�
//�÷��̾�, �� ��ũ��Ʈ�� ���� ����� ���̹Ƿ�, �Է¹޾ƾߵǴ� ������ ����
public class Character_Move : MonoBehaviour
{
    private Character_Core core;  //�ھ� ������-�׻� ��������

    [Header("�̵�")]
    [SerializeField] private float moveSpeed = 5.0f;

    //�ܺο��� ���� ���� �̵����� �Է� ����
    private Vector2 insertDir = Vector2.zero;

    //�ܺο��� �߰��� �� ��(�˹�/�����̵����� � ���ϰ�)
    private Vector2 externalForce = Vector2.zero;

    public bool canMove = true;

    //�� �÷��̾� �ִϸ����� ���ڿ� ��� ������ ���ڿ��� �Ѱ�-**
    private static readonly int moveHash = Animator.StringToHash("Speed"); //�ִϸ����� ���߿�
    private void Start()
    {
        core = GetComponent<Character_Core>();  //�����ũ ����-��ŸƮ���� �ھ� �ҷ�����-�׻� ��������
    }
    //�ܺ� �̵����� �Է°� ���޹��� public�Լ�
    public void SetDir(Vector2 dir)
    {
        insertDir = dir;
    }
    private void FixedUpdate()
    {
        Move();
    }

    //�ܺ� �� �ۿ�
    public void AddForce(Vector2 force)
    {
        externalForce = force;
    }

    //�̵����� �Լ�
    private void Move()
    {
        //�̵� �Ұ������϶�,
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

        //�Ϲ� �̵�
        Vector2 velocity = new Vector2(insertDir.x * moveSpeed, core.rb.velocity.y);

        //�ͽ��ͳ����� �߰� ���ְ�
        velocity += externalForce; 
        core.rb.velocity = velocity;
        //�ͽ��ͳ����� ���η� �����
        externalForce = Vector2.zero;

        //�ִϸ��̼� �Ҵ�
        core.anim.SetFloat(moveHash, Mathf.Abs(core.rb.velocity.x));
        //���� ��ȯ
        if (insertDir.x < 0.0f) core.spriteRenderer.flipX = true;
        if (insertDir.x > 0.0f) core.spriteRenderer.flipX = false;
    }

    //�ܺο��� ������ �� �ִ� Getter,Setter �߰� -> ���̺�+���׷��̵��
    public float GetMoveSpeed() => moveSpeed;
    public void SetMoveSpeed(float value) => moveSpeed = value;

}
