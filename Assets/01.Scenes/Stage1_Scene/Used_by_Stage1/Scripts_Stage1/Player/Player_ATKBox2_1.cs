using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ATKBox2_1 : MonoBehaviour
{
    [Header("플레이어 어택박스2 공격력")]
    [SerializeField] private int ATK2_1Power = 1;

    //외부에서 접근할 수 있는 Getter,Setter 추가 -> 세이브+업그레이드용
    public int GetATK2_1Power() => ATK2_1Power;
    public void SetATK2_1Power(int value) => ATK2_1Power = value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // 에너미 hp 감소시키기
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                //몇 대미지, 공격자 위치 
                enemyHp.TakeDamage(ATK2_1Power, transform.position);
            }
            //타격성공 사운드
            //SoundManager.Instance.PlayEffect("Player_ATK2_SFX");
        }
    }
}
