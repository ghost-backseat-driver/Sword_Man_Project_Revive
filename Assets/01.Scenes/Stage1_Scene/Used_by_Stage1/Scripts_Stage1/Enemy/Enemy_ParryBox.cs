using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 패링과 구조적으로 같음-다른점은 오직, 태그비교대상, 카메라FX 사운드
public class Enemy_ParryBox : MonoBehaviour
{
    private Character_Core core;
    private Character_Damaged damaged;

    [Header("패링 넉백 힘 설정")]
    [SerializeField] private float parryNBForceX = 1.0f;
    [SerializeField] private float parryNBForceY = 1.0f;

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
        if (!collision.CompareTag("playerAtk")) return;

        // 패링 성공 사운드
        SoundManager.Instance.PlayEffect("Player_Parry_SFX"); // 이거 똑같은거로 쓸거야?

        // 공격자 밀쳐내기
        Rigidbody2D playerRb = collision.GetComponentInParent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 dir = ((Vector2)playerRb.transform.position - (Vector2)transform.position).normalized;
            Vector2 parryNBDir = new Vector2(dir.x * parryNBForceX, parryNBForceY);
            playerRb.AddForce(parryNBDir, ForceMode2D.Impulse);
        }
    }
}
