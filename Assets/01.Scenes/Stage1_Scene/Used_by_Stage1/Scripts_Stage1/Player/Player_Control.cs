using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

//�÷��̾� �Է� ������ �͸� ���������, ��� ��ü�� ĳ����_��ũ��Ʈ�鿡 ����

public class Player_Control : MonoBehaviour
{
    //������ ���� ��ũ��Ʈ ������ ����
    private Character_Core core;
    private Character_Move move;
    private Character_Jump jump;

    //ĳ���� ���� �ݶ��̴���
    [Header("�����")]
    [SerializeField] private GameObject normalATK;
    [Header("������")]
    [SerializeField] private GameObject strongATK;

    //���� �ִϸ��̼� �ؽ�
    private static readonly int normalAtkHash = Animator.StringToHash("isATK1");
    private static readonly int strongAtkHash = Animator.StringToHash("isATK2");

    //���� ���� ������
    //����Ŵ��� ���������ϱ�, ������û�Ҷ� ���� ȣ���ϸ�� �Ʒ��ʿ�
    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();
        jump = GetComponent<Character_Jump>();

        //���ݿ� �ݶ��̴� ��Ȱ��ȭ�� ����
        normalATK.SetActive(false);
        strongATK.SetActive(false);
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
        }
        //���� ��ǲ == �����
        if (Input.GetKeyDown(KeyCode.A))
        {
            //����1 �ݶ��̴� Ȱ��ȭ
            normalATK.SetActive(true);
            //�ִϸ��̼� (Ʈ����)
            core.anim.SetTrigger(normalAtkHash);
            //���� �߰� �� ��
            SoundManager.Instance.PlayEffect("swordSwingSFX1");// ���� ������ ��� ã�ƾߵ� ���� �����´�� ������ݾ�.
            //�����ð��� ��Ȱ��ȭ
        }
        //���� ��ǲ == ������
        if (Input.GetKeyDown(KeyCode.D))
        {
            //����2 �ݶ��̴� Ȱ��ȭ
            strongATK.SetActive(true);
            //�ִϸ��̼� (Ʈ����)
            core.anim.SetTrigger(strongAtkHash);
            //���� �߰��� ��
            SoundManager.Instance.PlayEffect("swordSwingSFX1");
        }
    }
    /*
    ���� �� ��ũ��Ʈ�� ������ ��
    �÷��̾� �Է°�(x���̵�,����)�� Character_Move,Jump�� ����
     */
}
