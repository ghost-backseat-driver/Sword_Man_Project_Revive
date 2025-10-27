using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� �Է� ������ �͸� ���������, ��� ��ü�� ĳ����_��ũ��Ʈ�鿡 ����

public class Player_Control : MonoBehaviour
{
    //������ ���� ��ũ��Ʈ ������ ����
    private Character_Move move;
    private Character_Jump jump;

    //���� ���� ������
    //����Ŵ��� ���������ϱ�, ������û�Ҷ� ���� ȣ���ϸ�� �Ʒ��ʿ�
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

        //�̵� ���� ����
        move.SetDir(dir);

        if (Input.GetKeyDown(KeyCode.S))
        {
            //���� ��û
            jump.RequestJump();
            //���⿡ ����Ŵ��� ȣ��
        }
    }
    /*
    ���� �� ��ũ��Ʈ�� ������ ��
    �÷��̾� �Է°�(x���̵�,����)�� Character_Move,Jump�� ����
     */
}
