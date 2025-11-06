using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//지금 체력관련, 이동속도 관련을 에너미랑 같이 쓰고 있으니까,
//공용 스크립트인 체력, 이동속도 가져와서 여기서 참조를 할 수는 없어.
//그냥 이걸 플레이어 오브젝트가 컴포넌트로 가지고 있게하자.
//비활성화 되었을때도 상관없는게, 어차피 저장은 살아있을때에만 가능하니까
public class Player_SaveLoad : MonoBehaviour
{
    public Character_HP hp;
    public Character_Move move;

    //각 어택박스에서 참조를 못하고 있어서(컴포넌트 null) 세이브로드에서 가져올수있게 퍼블릭
    [SerializeField] public Player_ATKBox1 atkBox1;
    [SerializeField] public Player_ATKBox2_1 atkBox2_1;
    [SerializeField] public Player_ATKBox2_2 atkBox2_2;

    private void Awake()
    {
        //이미 같은 오브젝트에 붙어있으니까 GetComponent로 가져오면 됨
        hp = GetComponent<Character_HP>();
        move = GetComponent<Character_Move>();
    }

    //세이브 로드 에 필요한 get set 없는곳 일일히 추가해주고옴..
    public void Save()
    {
        //저장 전에 체력을 최대치로 채우기
        if (hp != null)
        {
            //현재체력을 최대치로 세팅
            hp.SetHP(hp.GetMaxHP());
        }

        PlayerData data = new PlayerData
        {
            playerPos = transform.position,
            playerHP = hp.GetHP(),
            playerMaxHP = hp.GetMaxHP(),
            playerMoveSpeed = move.GetMoveSpeed(),
            playerATK1Power = atkBox1.GetATK1Power(),
            playerATK2_1Power = atkBox2_1.GetATK2_1Power(),
            playerATK2_2Power = atkBox2_2.GetATK2_2Power(),

            playerCoin = Coin_UI.Instance.coinCount,
            atkUp = AtkUP_UI.Instance.atkUpCount,
            defUP = DefUP_UI.Instance.defUpCount,
            speedUP = SpeedUP_UI.Instance.speedUpCount,
            keyUP = KeyUP_UI.Instance.keyCount
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
        AtkUP_UI.Instance.atkUpCount = data.atkUp;
        AtkUP_UI.Instance.UpdateAtkUpUI();
        DefUP_UI.Instance.defUpCount = data.defUP;
        DefUP_UI.Instance.UpdateDefUpUI();
        SpeedUP_UI.Instance.speedUpCount = data.speedUP;
        SpeedUP_UI.Instance.UpdateSpeedUpUI();
        KeyUP_UI.Instance.keyCount = data.keyUP;
        KeyUP_UI.Instance.UpdateKeyUI();
    }
}
