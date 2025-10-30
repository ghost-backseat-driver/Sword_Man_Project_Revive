using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3_Arrow : MonoBehaviour
{
    [Header("�̵� �ӵ�")]
    [SerializeField] private float speed = 5.0f;

    [Header("���� �ð� (��)")]
    [SerializeField] private float lifeTime = 3.0f;

    private Vector2 moveDir;

    private Coroutine lifeCoroutine;

    // ������ �����ϰ� �̵� ����
    public void Shoot(Vector2 direction)
    {
        moveDir = direction.normalized;

        // ���� �ڷ�ƾ�� ���� ���̸� ����
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
        }

        lifeCoroutine = StartCoroutine(LifeTimerCo());
    }

    private void Update()
    {
        // ��� �̵�
        transform.Translate(moveDir * speed * Time.deltaTime, Space.World);
    }

    // ���� �ð� �� Ǯ�� ��ȯ
    private IEnumerator LifeTimerCo()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool();
    }

    // �÷��̾�� �浹 �� ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            // �÷��̾� �ǰ� ó��
            Character_HP playerHp = collision.GetComponent<Character_HP>();
            if (playerHp != null)
            {
                playerHp.TakeDamage(1, transform.position); // �ʿ信 ���� ������ ����
            }

            ReturnToPool();
        }
        // �ʿ� ��, �ٸ� �浹 ó�� �߰� ����
    }
    // Ǯ�� ����
    private void ReturnToPool()
    {
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }

        GameManagers.Pool.ReturnPool(this);
    }
}
