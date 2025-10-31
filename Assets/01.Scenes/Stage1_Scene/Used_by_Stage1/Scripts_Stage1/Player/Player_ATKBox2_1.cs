using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ATKBox2_1 : MonoBehaviour
{
    [Header("�÷��̾� ���ùڽ�2 ���ݷ�")]
    [SerializeField] private int ATK2_1Power = 1;

    //�ܺο��� ������ �� �ִ� Getter,Setter �߰� -> ���̺�+���׷��̵��
    public int GetATK2_1Power() => ATK2_1Power;
    public void SetATK2_1Power(int value) => ATK2_1Power = value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // ���ʹ� hp ���ҽ�Ű��
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                //�� �����, ������ ��ġ 
                enemyHp.TakeDamage(ATK2_1Power, transform.position);
            }
            //Ÿ�ݼ��� ����
            //SoundManager.Instance.PlayEffect("Player_ATK2_SFX");
        }
    }
}
