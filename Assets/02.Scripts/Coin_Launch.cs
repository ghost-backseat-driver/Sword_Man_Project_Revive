using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Coin_Launch : MonoBehaviour
{
    private Rigidbody2D rb; //������ �� �����տ� RB �־������
    private Coroutine lifeCoroutine;

    [Header("���� ���� �ð�(��)")]
    [SerializeField] private float lifeTime = 8.0f; //�θ� ������Ʈ ���-��Ȱ��ȭ �ð� ���Ϸθ� ����..

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //���� ������� ���޽�
    public void Launch(Vector2 dir, float force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * force, ForceMode2D.Impulse);

        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
        }

        lifeCoroutine = StartCoroutine(LifeTimerCo());
    }

    //���� �ð� �� Ǯ�� ��ȯ
    private IEnumerator LifeTimerCo()
    {
        yield return new WaitForSeconds(lifeTime);
        ReturnToPool();
    }

    //�÷��̾� �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            //���� �߰�
            SoundManager.Instance.PlayEffect("GetCoin_SFX");

            //���⼭ �÷��̾� ���� ���ο� ���� ��ߵ�
            Coin_UI.Instance.AddCoin();

            ReturnToPool();
        }
    }

    //Ǯ�� ����
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
