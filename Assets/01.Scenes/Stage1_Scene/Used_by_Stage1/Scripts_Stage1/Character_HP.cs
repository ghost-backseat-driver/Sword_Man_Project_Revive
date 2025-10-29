using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ĳ������ ü��-> �ǰ� �� ��, ü���� 0�� �� ���� ó���� ������
public class Character_HP : MonoBehaviour
{
    //�ھ� ������
    private Character_Core core;
    //�ǰݷ��� ������
    private Character_Damaged damaged;


    [Header("ü�� ����")]
    [SerializeField] private int startHp = 1;

    private int totalHp;

    //��� ��ó�� ���� ���� �߰� �ؾ���(����� ��� �� �ؽ�Ʈ �������)
    public bool isDead { get; private set; } = false;

    //��� �ִϸ��̼� �߰�
    private static readonly int deadHash = Animator.StringToHash("isDead");
    private void Start()
    {
        //�ھ� �ҷ�����
        core = GetComponent<Character_Core>();
        //�ǰݷ��� �ҷ�����
        damaged = GetComponent<Character_Damaged>();

        totalHp = startHp;
    }

    // �ǰ� ����� �Լ� (�ܺ� �Է°� �޾ƿ� ��)
    public void TakeDamage(int damage, Vector2 attackerPos)
    {
        if (isDead) return; //�ߺ�����        

        totalHp -= damage;

        if (damaged != null)
        {
            //�ǰݹ������̸� �ǰݴ���� �ȵ����� �ؾ���
            if (damaged.IsInvincible) return;
            //�ǰݷ��� ����
            damaged.OnHit(attackerPos);
        }

        if (totalHp <= 0)
        {
            Die();
        }
    }
    // ĳ���� ����ó�� �Լ� + ��ó�� ���� ����� ��� private-> protected virtual
    protected virtual void Die()
    {
        //��ó�� ���ؼ� �߰��� ����
        if (isDead) return; //�ߺ�����
        isDead = true;

        //ĳ���� ���갪 ����
        Character_Move move = GetComponent<Character_Move>();
        if (move != null) move.enabled = false;

        //�÷��̾� ��Ʈ�� �Է°� ����
        Player_Control control = GetComponent<Player_Control>();
        if (control != null) control.enabled = false;

        //��� ��� �ߵ�
        core.anim.SetTrigger(deadHash);

        //��� ���� ȣ�� ���⿡�ٰ� ������
        //->Soundmanager.Instance.PlayEffect("����Ҹ�")

        Invoke(nameof(DieDelay), 1.5f);

    }
    private void DieDelay()
    {
        gameObject.SetActive(false);
    }
    /*
    ���� �� ��ũ��Ʈ�� ������ ��
    ĳ������ Hp ����(�ʱ�ü��-�����),���ó��(�̵��� ����, ����ִϸ��̼�, ������Ʈ ��Ȱ��ȭ)
    */
}
