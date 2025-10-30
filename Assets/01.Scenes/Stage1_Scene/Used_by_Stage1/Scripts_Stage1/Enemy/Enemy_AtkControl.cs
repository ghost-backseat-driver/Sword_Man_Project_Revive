using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AtkControl : MonoBehaviour
{
    private Character_Core core;
    private Character_Move move;

    [Header("���� �νĿ� ����ĳ��Ʈ ����")]
    [SerializeField] private float atkRange = 1.0f; // ���� �Ÿ�
    [SerializeField] private LayerMask playerLayer;    // ������ ���̾�->�÷��̾�
    [SerializeField] private float atkCoolTime = 1.0f; // ���� ��Ÿ��

    //���ʹ� ���� �ݶ��̴���
    [Header("���ʹ� ����1")]
    [SerializeField] private GameObject enemyATK1;
    [Header("���ʹ� ����2")]
    [SerializeField] private GameObject enemyATK2;

    private static readonly int enemyAtk1Hash = Animator.StringToHash("isATK1");

    private bool isAttacking = false;
    private float nextAtkTime = 0.0f;

    //�÷��̾� ��ġ �����
    private Transform player;

    private void Start()
    {
        core = GetComponent<Character_Core>();
        move = GetComponent<Character_Move>();

        enemyATK1.SetActive(false);

        // �÷��̾� �±׷� ã�� 
        GameObject playerObj = GameObject.FindGameObjectWithTag("player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
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

        // ���� ���� ���� üũ
        if (Time.time >= nextAtkTime && IsPlayerInRange())
        {
            StartCoroutine(EnemyAtkCo(enemyAtk1Hash));
        }
    }

    private bool IsPlayerInRange()
    {
        // ���� �ٶ󺸴� ���� �������� ���� ���
        float dir = core.spriteRenderer.flipX ? -1f : 1f;
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * dir, atkRange, playerLayer);

        return hit.collider != null && hit.collider.CompareTag("player");
    }

    private IEnumerator EnemyAtkCo(int hash)
    {
        isAttacking = true;
        move.canMove = false;

        // ���� �ִϸ��̼� ����
        core.anim.SetTrigger(hash);
        
        yield return null;
        AnimatorStateInfo stateInfo = core.anim.GetCurrentAnimatorStateInfo(0);
        float animLength = stateInfo.length;

        yield return new WaitForSeconds(animLength);

        // ���� ���ݱ��� ��Ÿ��
        move.canMove = true;
        isAttacking = false;
        nextAtkTime = Time.time + atkCoolTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (core == null) core = GetComponent<Character_Core>();
        if (core == null || core.spriteRenderer == null) return;
        Gizmos.color = Color.red;
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * dir * atkRange);
    }

    private void ColliderPos(GameObject atkCollider)
    {
        //���ʹ̰� �ٶ󺸴� ���� �ø��� ���¸� -1���� 
        float dir = core.spriteRenderer.flipX ? -1.0f : 1.0f;
        
        //�ݶ��̴��� ���� ��ġ
        Vector3 pos = atkCollider.transform.localPosition;
        //�����̸� true -1.0f ���ع�����->������ ��ġ ���� ���� ��Ű��.
        pos.x = Mathf.Abs(pos.x) * dir;
        //���� ��ġ �ݶ��̴��� ����
        atkCollider.transform.localPosition = pos;
    }

    //====================================================
    //���ʹ�1 ���� �ݶ��̴� Ȱ��ȭ
    public void EnableEnemyAttack1Collider()
    {
        ColliderPos(enemyATK1);
        enemyATK1.SetActive(true);
        SoundManager.Instance.PlayEffect("Heavy_SwordSwing_SFX");
    }
    //���ʹ�1 ���� �ݶ��̴� ��Ȱ��ȭ
    public void DisableEnemyAttack1Collider()
    {
        enemyATK1.SetActive(false);
    }
    //====================================================
    //���ʹ�2 ���� �ݶ��̴� Ȱ��ȭ
    public void EnableEnemyAttack2Collider()
    {
        ColliderPos(enemyATK1);
        enemyATK1.SetActive(true);
        SoundManager.Instance.PlayEffect("SpearSwing_SFX");
    }
    //���ʹ�2 ���� �ݶ��̴� ��Ȱ��ȭ
    public void DisableEnemyAttack2Collider()
    {
        enemyATK1.SetActive(false);
    }
    //====================================================
    //���ʹ�2 �� ���� �Ϲ� �ݶ��̴� Ȱ��ȭ
    public void EnableEnemyshieldCollider()
    {
        ColliderPos(enemyATK2);
        enemyATK2.SetActive(true);
    }
    //���ʹ�2 ���� �ݶ��̴� ��Ȱ��ȭ
    public void DisableEnemyshieldCollider()
    {
        enemyATK2.SetActive(false);
    }
}
