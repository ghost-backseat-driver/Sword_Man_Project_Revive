using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy3_ArrowShooter : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;
    private Character_HP hp;

    [Header("ȭ�� ������")]
    [SerializeField] private Enemy3_Arrow arrowPrefab;

    [Header("�߻� ��ġ")]
    [SerializeField] private Transform firePoint;

    [Header("�߻� ����")]
    [SerializeField] private float fireInterval = 1.5f;

    [Header("�÷��̾� �ν� ����")]
    [SerializeField] private float detectRadius = 4.0f;

    [Header("�ν��� ���̾� -> Player")]
    [SerializeField] private LayerMask playerLayer;

    private static readonly int enemyAtk1Hash = Animator.StringToHash("isATK1");

    private bool isAttacking = false;
    private float nextFireTime = 0.0f;

    //�÷��̾� ��ġ �����
    private Transform player;

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();

        hp = GetComponent<Character_HP>();

        // �÷��̾� �±׷� ã�� 
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void OnEnable()
    {
        // ������Ʈ Ǯ ����
        GameManagers.Pool.CreatePool(arrowPrefab, 10);
    }

    private void Update()
    {
        //�÷��̾� ��ġ �𸣸� ����
        if (player == null) return;

        // ���� ���̸� �̵� ����
        if (isAttacking)
        {
            move.SetDir(Vector2.zero);
            return;
        }

        //������ ���� �ѹ� �� �ִ°� �³�..
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer);

        // ���� ���� ���� üũ
        if (hit !=null && Time.time >= nextFireTime)
        {
            StartCoroutine(ArrowAtkCo(enemyAtk1Hash));
        }
    }

    private IEnumerator ArrowAtkCo(int hash)
    {
        isAttacking = true;
        move.canMove = false;

        //���� ������Ʈ �Ⱦ��Ŵϱ�, �߻� ������ �ø�����ߵǰ�
        if (player != null && !hp.isDead)// ���� ���¿����� �ø��Ǳ淡 ���ϰ� �߰�
        {
            Vector2 dir = player.position - transform.position;
            core.spriteRenderer.flipX = dir.x < 0.0f;
        }

        //���� �ִϸ��̼� ����
        core.anim.SetTrigger(hash);

        yield return null;
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;

        yield return new WaitForSeconds(animLength);

        // ���� ���ݱ��� ��Ÿ��
        move.canMove = true;
        isAttacking = false;
        nextFireTime = Time.time + fireInterval;
    }

    //�ִϸ��̼� �̺�Ʈ �ݶ��̴� Ȱ��ȭ
    public void EnableEnemyArrowcollider()
    {
        //�ݶ��̴� �浹ó���� Arrow�� ���� ���� Ȱ��ȭ�� �ϸ��

        //�÷��̾� ���������� ����
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectRadius, playerLayer);
        if (hit == null) return; //���� �ȵǸ� ����

        // ���� ���
        Vector2 direction = (hit.transform.position - firePoint.position).normalized;

        // Ǯ���� �Ѿ� ��������
        Enemy3_Arrow Arrow = GameManagers.Pool.GetFromPool(arrowPrefab);
        if (Arrow != null)
        {
            //�߻���ġ�� �̵�
            Arrow.transform.position = firePoint.position;
            //ȭ�� ȸ�����ѾߵǴµ� �ø��� �ƴϰ� ȸ���̾� 
            Arrow.transform.right = direction; //ȭ������ �������̴ϱ� �׳� �ٶ󺸴� ���� ���������� ����

            Arrow.Shoot(direction); // ���� ����
        }
        //ȭ�� �߻� ���� ���⿡ �߰�
        SoundManager.Instance.PlayEffect("ArrowSwish_SFX");

    }

    // ����׿� ��������
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
