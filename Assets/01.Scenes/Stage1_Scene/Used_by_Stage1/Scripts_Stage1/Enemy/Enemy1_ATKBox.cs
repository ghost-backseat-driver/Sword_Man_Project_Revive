using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_ATKBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            //플레이어 hp 감소시키기
            Character_HP playerHp = collision.GetComponent<Character_HP>();
            if (playerHp != null)
            {
                //몇 대미지, 공격자 위치 
                playerHp.TakeDamage(2, transform.position);
            }
            //타격성공 사운드
            //SoundManager.Instance.PlayEffect("Player_ATK1_SFX");
        }
    }
}
