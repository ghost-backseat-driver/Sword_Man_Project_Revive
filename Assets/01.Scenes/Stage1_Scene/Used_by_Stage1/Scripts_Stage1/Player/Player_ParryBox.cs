using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ParryBox : MonoBehaviour
{
    private Character_Core core;
    private Character_Damaged damaged;

    [Header("패링 넉백 힘 설정")]
    [SerializeField] private float parryNBForceX = 5.0f;
    [SerializeField] private float parryNBForceY = 2.0f;
    
    //패링용 카메라FX
    [SerializeField] private CamaraFxManager ParryFxManager;

    private bool isParry;

    private void Awake()
    {
        core = GetComponentInParent<Character_Core>();
        damaged = GetComponentInParent<Character_Damaged>();
    }

    //패링시작
    private void OnEnable()
    {
        isParry = true;

        //damaged쪽 무적상태true
        if (damaged != null) 
        {
            damaged.SetInvincible(true);
        }
    }

    //패링 종료
    private void OnDisable()
    {
        isParry = false;

        //damaged쪽 무적상태false
        if (damaged != null)
        {
            damaged.SetInvincible(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isParry) return; //중복방지
        if (!collision.CompareTag("enemyAtk")) return;

        // 패링 성공 사운드
        SoundManager.Instance.PlayEffect("Player_Parry_SFX");

        //패링 성공 카메라FX
        ParryFxManager.OnCameraFX();

        // 공격자 밀쳐내기
        Rigidbody2D enemyRb = collision.GetComponentInParent<Rigidbody2D>();
        if (enemyRb != null)
        {
            Vector2 dir = ((Vector2)enemyRb.transform.position - (Vector2)transform.position).normalized;
            Vector2 parryNBDir = new Vector2(dir.x * parryNBForceX, parryNBForceY);
            enemyRb.AddForce(parryNBDir, ForceMode2D.Impulse);
        }
    }
}
