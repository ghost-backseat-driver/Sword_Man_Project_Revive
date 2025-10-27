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

    //�̵����� �Լ�
    private void Move()
    {
        core.rb.velocity = new Vector2(insertDir.x * moveSpeed, core.rb.velocity.y);
        //�ִϸ��̼� �Ҵ�
        core.anim.SetFloat(moveHash, Mathf.Abs(core.rb.velocity.x));
        //���� ��ȯ
        if (insertDir.x < 0.0f) core.spriteRenderer.flipX = true;
        if (insertDir.x > 0.0f) core.spriteRenderer.flipX = false;
    }
    /*
     ���� �� ��ũ��Ʈ�� ������ ��
    -�̵�,������ȯ ����, �̵� �ִϸ��̼Ǳ���
    -Character_Move ������Ʈ ������, �ش� ������ ������ �Է°� ������ ��.
     */
}
