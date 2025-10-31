using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ü�°���, �̵��ӵ� ������ ���ʹ̶� ���� ���� �����ϱ�,
//���� ��ũ��Ʈ�� ü��, �̵��ӵ� �����ͼ� ���⼭ ������ �� ���� ����.
//�׳� �̰� �÷��̾� ������Ʈ�� ������Ʈ�� ������ �ְ�����.
//��Ȱ��ȭ �Ǿ������� ������°�, ������ ������ ������������� �����ϴϱ�
public class Player_SaveLoad : MonoBehaviour
{
    private Character_HP hp;
    private Character_Move move;

    [SerializeField] private Player_ATKBox1 atkBox1;
    [SerializeField] private Player_ATKBox2_1 atkBox2_1;
    [SerializeField] private Player_ATKBox2_2 atkBox2_2;

    private void Awake()
    {
        // �̹� ���� ������Ʈ�� �پ������ϱ� GetComponent�� �������� ��
        hp = GetComponent<Character_HP>();
        move = GetComponent<Character_Move>();
    }

    //���̺� �ε� �� �ʿ��� get set ���°� ������ �߰����ְ��..
    public void Save()
    {
        PlayerData data = new PlayerData
        {
            playerPos = transform.position,
            playerHP = hp.GetHP(),
            playerMaxHP = hp.GetMaxHP(),
            playerMoveSpeed = move.GetMoveSpeed(),
            playerATK1Power = atkBox1.GetATK1Power(),
            playerATK2_1Power = atkBox2_1.GetATK2_1Power(),
            playerATK2_2Power = atkBox2_2.GetATK2_2Power(),
            playerCoin = Coin_UI.Instance.coinCount
        };

        SaveSystem.SavePlayer(data);
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null) return;

        transform.position = data.playerPos;
        hp.SetHP(data.playerHP);
        hp.SetMaxHP(data.playerMaxHP);
        move.SetMoveSpeed(data.playerMoveSpeed);
        atkBox1.SetATK1Power(data.playerATK1Power);
        atkBox2_1.SetATK2_1Power(data.playerATK2_1Power);
        atkBox2_2.SetATK2_2Power(data.playerATK2_2Power);
        Coin_UI.Instance.coinCount = data.playerCoin;
        Coin_UI.Instance.UpdateCoinUI();
    }
}
