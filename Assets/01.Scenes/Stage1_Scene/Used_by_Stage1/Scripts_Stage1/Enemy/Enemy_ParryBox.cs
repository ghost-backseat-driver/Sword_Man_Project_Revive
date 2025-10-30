using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾� �и��� ���������� ����-�ٸ����� ����, �±׺񱳴��, ī�޶�FX ����
public class Enemy_ParryBox : MonoBehaviour
{
    private Character_Core core;
    private Character_Damaged damaged;

    [Header("�и� �˹� �� ����")]
    [SerializeField] private float parryNBForceX = 1.0f;
    [SerializeField] private float parryNBForceY = 1.0f;

    private bool isParry;

    private void Awake()
    {
        core = GetComponentInParent<Character_Core>();
        damaged = GetComponentInParent<Character_Damaged>();
    }

    //�и�����
    private void OnEnable()
    {
        isParry = true;

        //damaged�� ��������true
        if (damaged != null)
        {
            damaged.SetInvincible(true);
        }
    }

    //�и� ����
    private void OnDisable()
    {
        isParry = false;

        //damaged�� ��������false
        if (damaged != null)
        {
            damaged.SetInvincible(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isParry) return; //�ߺ�����
        if (!collision.CompareTag("playerAtk")) return;

        // �и� ���� ����
        SoundManager.Instance.PlayEffect("Player_Parry_SFX"); // �̰� �Ȱ����ŷ� ���ž�?

        // ������ ���ĳ���
        Rigidbody2D playerRb = collision.GetComponentInParent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 dir = ((Vector2)playerRb.transform.position - (Vector2)transform.position).normalized;
            Vector2 parryNBDir = new Vector2(dir.x * parryNBForceX, parryNBForceY);
            playerRb.AddForce(parryNBDir, ForceMode2D.Impulse);
        }
    }
}
