using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ATKBox2_2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // 에너미 hp 감소시키기
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                //몇 대미지, 공격자 위치 
                enemyHp.TakeDamage(3, transform.position);
            }
            //타격성공 사운드
            //SoundManager.Instance.PlayEffect("Player_ATK2_SFX");
        }
    }
}
