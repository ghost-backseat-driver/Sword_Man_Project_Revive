using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ATKBox1 : MonoBehaviour
{
    [Header("�÷��̾� ���ùڽ�1 ���ݷ�")]
    [SerializeField] private int ATK1Power = 1;

    //�ܺο��� ������ �� �ִ� Getter,Setter �߰� -> ���̺�+���׷��̵��
    public int GetATK1Power() => ATK1Power;
    public void SetATK1Power(int value) => ATK1Power = value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // ���ʹ� hp ���ҽ�Ű��
            Character_HP enemyHp = collision.GetComponent<Character_HP>();
            if (enemyHp != null)
            {
                //�� �����, ������ ��ġ 
                enemyHp.TakeDamage(ATK1Power, transform.position);
            }
            //Ÿ�ݼ��� ����
            //SoundManager.Instance.PlayEffect("Player_ATK1_SFX");
        }
    }
}
