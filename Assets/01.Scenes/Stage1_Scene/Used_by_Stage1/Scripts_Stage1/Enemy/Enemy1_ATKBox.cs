using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_ATKBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            // 에너미 hp 감소시키기
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                enemyHp.TakeDamage(3);
            }
            //ATK1 성공시 사운드
            SoundManager.Instance.PlayEffect("Player_ATK1_SFX");
        }
    }
}
