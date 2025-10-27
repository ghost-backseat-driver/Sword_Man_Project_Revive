using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ĳ������ ü��-> �ǰ� �� ��, ü���� 0�� �� ���� ó���� ������
public class Character_HP : MonoBehaviour
{
    //�ھ� ������
    private Character_Core core;

    [Header("ü�� ����")]
    [SerializeField] private int startHp = 1;

    [Header("�ǰ� ��� �ִ���?")]
    [SerializeField] private bool damageAnim = false; //�÷��̾� ���ʹ� ��� �ǰ� ��� ������ ���� ���ص� ��

    private int totalHp;

    //��� ��ó�� ���� ���� �߰� �ؾ���(����� ��� �� �ؽ�Ʈ �������)
    public bool isDead { get; private set; } = false;
    
    //���/�ǰ� �ִϸ��̼� �߰��� ���� �ּ� Ǯ��
    //private static readonly int deadHash = Animator.StringToHash("isDead");
    //private static readonly int damagedHash = Animator.StringToHash("isDamaged");
    private void Start()
    {
        //�ھ� �ҷ�����
        core = GetComponent<Character_Core>();
        totalHp = startHp;
    }

    // �ǰ� ����� �Լ� (�ܺ� �Է°� �޾ƿ� ��)
    public void TakeDamage(int damage)
    {
        totalHp -= damage;

        //�ǰ� �ִϸ��̼� �߰��� �ִϸ��̼� �ߵ�
        //if (damageAnim)
        //{
        //    core.anim.SetTrigger(damagedHash);
        //}

        if (totalHp <= 0)
        {
            Die();
        }
    }

    // ĳ���� ����ó�� �Լ� + ��ó�� ���� ����� ��� private-> protected virtual
    protected virtual void Die()
    {
        //��ó�� ���ؼ� �߰��� ����
        if (isDead) return; //�ߺ�������
        isDead = true;

        //ĳ���� ���갪 ����
        Character_Move move = GetComponent<Character_Move>();
        if (move != null) move.enabled = false;

        //�÷��̾� ��Ʈ�� �Է°� ����
        Player_Control control = GetComponent<Player_Control>();
        if (control != null) control.enabled = false;

        //��� ��� �ߵ�
        //core.anim.SetTrigger(deadHash);

        Invoke(nameof(DieDelay), 0.4f);

    }
    private void DieDelay() //�ڷ�ƾ�ᵵ �Ǵµ�, �����ѰŴϱ� �޸� ���鿡�� �κ�ũ
    {
        gameObject.SetActive(false);
    }
    /*
    ���� �� ��ũ��Ʈ�� ������ ��
    ĳ������ Hp ����(�ʱ�ü��-�����),���ó��(�̵��� ����, ����ִϸ��̼�, ������Ʈ ��Ȱ��ȭ)
    */
}
