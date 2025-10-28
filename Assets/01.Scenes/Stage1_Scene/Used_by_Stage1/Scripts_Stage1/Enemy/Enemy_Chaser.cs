using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

//ĳ���� ���� �ް�, ü�̽� ��� ����
//�÷��̾ �ν��ϸ�, ������ũ ��ũ��Ʈ�� ��Ȱ��ȭ
//������ �̵� �ӵ� ����
public class Enemy_Chaser : MonoBehaviour
{
    private Character_Move move;
    private Transform player;
    //������ũ ��Ȱ��ȭ �� ����
    private Enemy_RandomWalk randomWalk;

    //��¿�� ���� �ִϸ����� ���� ����..
    private Animator anim;

    [Header("�÷��̾� ���� ����")]
    [SerializeField] private float chaseRange = 2.5f;

    [Header("�ν��� ���̾�")]
    [SerializeField] private LayerMask playerLayer;

    [Header("�����ӵ� ������*")]
    [SerializeField] private float chaseSpeed = 1.5f;

    private void Awake()
    {
        move = GetComponent<Character_Move>();
        randomWalk = GetComponent<Enemy_RandomWalk>();
    }

    //�±� �� -�ҹ��� �Ѱ� �������� ���̾�� �빮��
    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if (playerObj != null) player = playerObj.transform;
    }

    private void Update()
    {
        if (player == null) return;

        //ü�̽� ����
        if (IsPlayerInRange())
        {
            //���� ���̸�, ������ũ ���� ��Ȱ��ȭ
            if (randomWalk != null && randomWalk.enabled) randomWalk.enabled = false;
            Vector2 originDir = (player.position - transform.position).normalized;
            Vector2 plusDir = new Vector2(originDir.x * chaseSpeed, 0.0f); //�����ӵ� �÷���->�����̶� ���ϱ��, ���ϱ�� ������ü�� ��ٶ���
            move.SetDir(new Vector2(plusDir.x, 0)); // �����ӵ��� X�� ���⸸ ����
        }
        else
        {
            //���� ���̸�, ������ũ ���� �ٽ� Ȱ��ȭ
            if (randomWalk != null && !randomWalk.enabled) randomWalk.enabled = true;
            move.SetDir(Vector2.zero); // ���� ����
        }
    }

    //�νĹ��� ��������Ŭ
    private bool IsPlayerInRange()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, chaseRange, playerLayer);
        return hit != null;
    }

    //Ȯ�ο� �����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
    /*
     ���� �� ��ũ��Ʈ�� ������ ��
    -ĳ���� ���꿡 �������� (Enemy)->(Player) ���� ���+ �����ӵ�����
    -������ũ ��Ȱ��ȭ ��� + ������ũ ���� �ܵ� ��� ����
     */
}
