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

    //�÷��̾� ���� �ݶ��̴���
    [Header("�����")]
    [SerializeField] private GameObject normalATK;
    [Header("������1�� ������")]
    [SerializeField] private GameObject strongATK1st;
    [Header("������2�� ������")]
    [SerializeField] private GameObject strongATK2nd;

    //�÷��̾� �и� �ݶ��̴���
    [Header("���")]
    [SerializeField] private GameObject parry;

    //���� �ִϸ��̼� �ؽ�
    private static readonly int normalAtkHash = Animator.StringToHash("isATK1");
    private static readonly int strongAtkHash = Animator.StringToHash("isATK2");

    //�и� �ִϸ��̼� �ؽ�
    private static readonly int parryHash = Animator.StringToHash("isParry");

    //�������϶� �Է°� ���� �뵵�� �ҹ�
    private bool isAttacking = false;

    //���� ���� ������
    //����Ŵ��� ���������ϱ�, ������û�Ҷ� ���� ȣ���ϸ�� �Ʒ��ʿ�
    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();
        jump = GetComponent<Character_Jump>();
    }

    private void Update()
    {
        //�����߿��� �Է°� ����
        if (isAttacking) return;

        //���� ��ǲ == ����� ==============
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AtkTypeCo(normalAtkHash, "normal"));
        }
        //���� ��ǲ == ������ ==============
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(AtkTypeCo(strongAtkHash, "strong"));
        }
        //=================================

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

        //�и�����==========================
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(ParryCo(parryHash));
        }
        //==================================
    }

    //���ݰ��� �ڷ�ƾ
    private IEnumerator AtkTypeCo(int hash, string type)
    {
        //������, �̵��Ұ�����
        isAttacking = true;
        move.canMove = false; 

        //������ ���� ����
        move.SetDir(Vector2.zero); //�̰� �ȵǴµ�? �����ϸ鼭 ������Ʈ ��ġ�� �Ǵϱ� ��Ų��..? �ϴ� ����

        //���� �ӵ� ���η�(�������� Ƣ����°� ����)
        core.rb.velocity = Vector2.zero;

        //���� �ٶ󺸴� ���� ���
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;
        //������ ������ ��¦ �о������ //���� ���ľߵ� move ���̶� ����
        core.rb.AddForce(new Vector2(dir * 3.0f, 0.0f),ForceMode2D.Impulse);

        //�ִϸ��̼�
        core.anim.SetTrigger(hash);

        yield return null; //�� ������ ��ٷȴٰ� �ִϸ��̼� ������Ʈ ��ȯ �ǰ�
        //ù��° ����-���� �ִϸ��̼� ���̸�ŭ ��� -�ִϸ����� ������Ʈ ���ݱ��� ������ �ּ� Ǯ��
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;

        //�ι�° ����-��ٸ� �ð� ����
        yield return new WaitForSeconds(animLength); //animLength ���̷� �����Ұ� 

        //���ݻ��� ����, �̵����ɻ���
        move.canMove = true;
        isAttacking = false;
    }

    //�и����� �ڷ�ƾ
    private IEnumerator ParryCo(int hash)
    {
        //�̵� �����ϰ�
        move.canMove = false;
        move.SetDir(Vector2.zero);
        core.rb.velocity = Vector2.zero;

        //�ִϸ��̼�
        core.anim.SetTrigger(hash);
        
        yield return null;
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;
        yield return new WaitForSeconds(animLength);

        move.canMove= true;
    }

    //�ִϸ��̼� �̺�Ʈ�� ȣ���� �͵�=========================

    //�÷��̾� ���⿡ �°� �̺�Ʈ �ݶ��̴� �ø���
    private void FlipCollider(GameObject atkCollider)
    {
        //�ݶ��̴��� ���� ��ġ
        Vector3 pos = atkCollider.transform.localPosition;
        //�����̸� true -1.0f ���ع�����->������ ��ġ ���� ���� ��Ű��.
        pos.x = Mathf.Abs(pos.x) * (core.spriteRenderer.flipX ? -1.0f : 1.0f);
        //���� ��ġ �ݶ��̴��� ����
        atkCollider.transform.localPosition = pos;
    }

    //����� �ݶ��̴� Ȱ��ȭ
    public void EnableNormalAttackCollider()
    {
        FlipCollider(normalATK);
        normalATK.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //����� �ݶ��̴� ��Ȱ��ȭ
    public void DisableNormalAttackCollider()
    {
        normalATK.SetActive(false);
    }
    //������1�� ������ �ݶ��̴� Ȱ��ȭ
    public void EnableStrongAttack1stCollider()
    {
        FlipCollider(strongATK1st);
        strongATK1st.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //������1�� ������ �ݶ��̴� ��Ȱ��ȭ
    public void DisableStrongAttack1stCollider()
    {
        strongATK1st.SetActive(false);
    }

    //������2�� ������ �ݶ��̴� Ȱ��ȭ
    public void EnableStrongAttack2ndCollider()
    {
        FlipCollider(strongATK2nd);
        strongATK2nd.SetActive(true);
        SoundManager.Instance.PlayEffect("swordSwingSFX1");
    }
    //������2�� ������ �ݶ��̴� ��Ȱ��ȭ
    public void DisableStrongAttack2ndCollider()
    {
        strongATK2nd.SetActive(false);
    }

    //�и� ������ �ݶ��̴� Ȱ��ȭ
    public void EnableParryCollider()
    {
        //�ø����Ұ�
        parry.SetActive(true);
        SoundManager.Instance.PlayEffect("Player_ShieldReady_SFX");
    }
    //�и� ������ �ݶ��̴� ��Ȱ��ȭ
    public void DisableParryCollider()
    {
        parry.SetActive(false);
    }
}
