using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    //�������϶� �Է°� ���� �뵵�� �ҹ�
    private bool isAttacking = false;

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
        //�����߿��� �Է°� ����
        if (isAttacking) return;

        //�̵�����========================
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) dir.x = -1.0f;
        if (Input.GetKey(KeyCode.RightArrow)) dir.x = 1.0f;

        //�̵� ���� ����===================
        move.SetDir(dir);

        //��������=========================
        if (Input.GetKeyDown(KeyCode.S))
        {
            //���� ��û
            jump.RequestJump(); //���� ����� Character_Jump�ʿ�
        }
        //=================================

        //���� ��ǲ == ����� ==============
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AtkType(normalAtkHash, "normal"));
        }
        //���� ��ǲ == ������ ==============
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(AtkType(strongAtkHash, "strong"));
        }
        //=================================
    }

    private IEnumerator AtkType(int hash, string type)
    {
        //������, �̵��Ұ�����
        isAttacking = true;
        move.canMove = false; 

        //������ ���� ����
        move.SetDir(Vector2.zero);

        //���� �ӵ� ���η�(�������� Ƣ����°� ����)
        core.rb.velocity = Vector2.zero;

        //���� �ٶ󺸴� ���� ���
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;
        //������ ������ ��¦ �о������ //���� ���ľߵ� move ���̶� ����
        core.rb.AddForce(new Vector2(dir * 3.0f, 0.0f),ForceMode2D.Impulse);

        core.anim.SetTrigger(hash);

        //yield return null; �� ������ ��ٷȴٰ� �ִϸ��̼� ������Ʈ ��ȯ �ǰ�
        //ù��° ����-���� �ִϸ��̼� ���̸�ŭ ��� -�ִϸ����� ������Ʈ ���ݱ��� ������ �ּ� Ǯ��
        //AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        //float animLength = stateInfo.length;

        //�ι�° ����-��ٸ� �ð� ����(�ӽ�)
        yield return new WaitForSeconds(1.0f); //animLength ���̷� �����Ұ� 

        //���ݻ��� ����, �̵����ɻ���
        move.canMove = true;
        isAttacking = false;
    }

    //�ִϸ��̼� �̺�Ʈ�� ȣ���� �͵�

    //����� �ݶ��̴� Ȱ��ȭ
    public void EnableNormalAttackCollider()
    {
        normalATK.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //����� �ݶ��̴� ��Ȱ��ȭ
    public void DisableNormalAttackCollider()
    {
        normalATK.SetActive(false);
    }
    //������ �ݶ��̴� Ȱ��ȭ
    public void EnableStrongAttackCollider()
    {
        strongATK.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //������ �ݶ��̴� ��Ȱ��ȭ
    public void DisableStrongAttackCollider()
    {
        strongATK.SetActive(false);
    }
}
