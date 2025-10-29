using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ParryBox : MonoBehaviour
{
    private Character_Core core;
    private Character_Damaged damaged;

    [Header("�и� �˹� �� ����")]
    [SerializeField] private float parryNBForceX = 5.0f;
    [SerializeField] private float parryNBForceY = 2.0f;
    
    //�и��� ī�޶�FX
    [SerializeField] private CamaraFxManager ParryFxManager;

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
        if (!collision.CompareTag("enemyAtk")) return;

        // �и� ���� ����
        SoundManager.Instance.PlayEffect("Player_Parry_SFX");

        //�и� ���� ī�޶�FX
        ParryFxManager.OnCameraFX();

        // ������ ���ĳ���
        Rigidbody2D enemyRb = collision.GetComponentInParent<Rigidbody2D>();
        if (enemyRb != null)
        {
            Vector2 dir = ((Vector2)enemyRb.transform.position - (Vector2)transform.position).normalized;
            Vector2 parryNBDir = new Vector2(dir.x * parryNBForceX, parryNBForceY);
            enemyRb.AddForce(parryNBDir, ForceMode2D.Impulse);
        }
    }
}
