using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ATKBox1 : MonoBehaviour
{
    [Header("플레이어 어택박스1 공격력")]
    [SerializeField] private int ATK1Power = 1;

    //외부에서 접근할 수 있는 Getter,Setter 추가 -> 세이브+업그레이드용
    public int GetATK1Power() => ATK1Power;
    public void SetATK1Power(int value) => ATK1Power = value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // 에너미 hp 감소시키기
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                //몇 대미지, 공격자 위치 
                enemyHp.TakeDamage(ATK1Power, transform.position);
            }
            //타격성공 사운드
            //SoundManager.Instance.PlayEffect("Player_ATK1_SFX");
        }
    }
}
