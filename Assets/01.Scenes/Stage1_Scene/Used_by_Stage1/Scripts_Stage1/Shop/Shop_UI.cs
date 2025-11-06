using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    //각 버튼마다 다시 정리
    //공격력관련
    [Header("상점-공격력 관련 버튼,패널")]
    [SerializeField] private Button atkInfo_Button; //상점 화면에서 각 아이템 버튼->설명패널로 이동
    [SerializeField] private GameObject atkInfo_Panel; //공격력 아이템 설명 패널
    [SerializeField] private Button atkBuy_Button; //아이템버튼 후 각 구매 버튼 -> 조건에 따라서 완료/실패 패널로 이동
    [SerializeField] private Button atkCancel_Button; //설명패널에서 취소키-> 설명패널 비활성화
    //방어력관련
    [Header("상점-방어력 관련 버튼,패널")]
    [SerializeField] private Button defInfo_Button;
    [SerializeField] private GameObject defInfo_Panel; //체력(방어력) 아이템 설명 패널
    [SerializeField] private Button defBuy_Button;
    [SerializeField] private Button defCancel_Button;
    //이동속도관련
    [Header("상점-이동속도 관련 버튼,패널")]
    [SerializeField] private Button speedInfo_Button;
    [SerializeField] private GameObject speedInfo_Panel; //이동속도 아이템 설명 패널
    [SerializeField] private Button speedBuy_Button;
    [SerializeField] private Button speedCancel_Button;
    //열쇠관련
    [Header("상점-열쇠 관련 버튼,패널")]
    [SerializeField] private Button keyInfo_Button;
    [SerializeField] private GameObject keyInfo_Panel; //키 아이템 설명 패널
    [SerializeField] private Button keyBuy_Button;
    [SerializeField] private Button keyCancel_Button;
    //구매버튼 누른후,

    //=======구매성공관련========
    [Header("구매 완료 알림패널")]//여기에 구매완료 패널을 닫는 버튼 같이
    [SerializeField] private GameObject buySuccess_Panel; //구매완료 설명 패널
    [SerializeField] private Button buySuccess_Button; //구매완료확인 버튼,(완료패널을 닫음)
    //=======구매실패관련========
    [Header("구매 실패 알림패널")]//구매실패 패널을 닫는 버튼 같이
    [SerializeField] private GameObject buyFailed_Panel; //구매실패 설명 패널
    [SerializeField] private Button buyFailed_Button; //구매실패확인 버튼,(실패패널을 닫음)

    private void Start()
    {
        //공격관련-버튼 이벤트 등록
        atkInfo_Button.onClick.AddListener(OpenATKInfo); //설명창 열 버튼->설명패널 열기
        atkBuy_Button.onClick.AddListener(BuyATKSetting); //구매확인버튼 하나->조건에 따라 성공 실패 패널 열기
        atkCancel_Button.onClick.AddListener(CloseATKInfo); //구매취소버튼 하나->설명패널 닫기 

        //방어력관련-버튼 이벤트 등록
        defInfo_Button.onClick.AddListener(OpenDEFInfo);
        defBuy_Button.onClick.AddListener(BuyDEFSetting);
        defCancel_Button.onClick.AddListener(CloseDEFInfo);
        //이동속도관련-버튼 이벤트 등록
        speedInfo_Button.onClick.AddListener(OpenSPEEDInfo);
        speedBuy_Button.onClick.AddListener(BuySPEEDSetting);
        speedCancel_Button.onClick.AddListener(CloseSPEEDInfo);
        //열쇠관련-버튼 이벤트 등록
        keyInfo_Button.onClick.AddListener(OpenKEYInfo);
        keyBuy_Button.onClick.AddListener(BuyKEYSetting);
        keyCancel_Button.onClick.AddListener(CloseKEYInfo);


        //구매 성공실패 관련-버튼 이벤트 등록 //구매 성공확인버튼 //구매 실패확인버튼
        buySuccess_Button.onClick.AddListener(SuccessClose);
        buyFailed_Button.onClick.AddListener(FailClose);

    }

    //중복방지
    private void OnDestroy()
    {
        //공격관련-버튼 중복방지
        atkInfo_Button.onClick.RemoveListener(OpenATKInfo);
        atkBuy_Button.onClick.RemoveListener(BuyATKSetting);
        atkCancel_Button.onClick.RemoveListener(CloseATKInfo);
        //방어력관련-버튼 중복방지
        defInfo_Button.onClick.RemoveListener(OpenDEFInfo);
        defBuy_Button.onClick.RemoveListener(BuyDEFSetting);
        defCancel_Button.onClick.RemoveListener(CloseDEFInfo);
        //이동속도관련-버튼 중복방지
        speedInfo_Button.onClick.RemoveListener(OpenSPEEDInfo);
        speedBuy_Button.onClick.RemoveListener(BuySPEEDSetting);
        speedCancel_Button.onClick.RemoveListener(CloseSPEEDInfo);
        //열쇠관련-버튼 중복방지
        keyInfo_Button.onClick.RemoveListener(OpenKEYInfo);
        keyBuy_Button.onClick.RemoveListener(BuyKEYSetting);
        keyCancel_Button.onClick.RemoveListener(CloseKEYInfo);
    }

    //아이템 버튼 눌렀을때,

    //공격력관련=================================================================================================================

    //공격력 구매 버튼 눌렀을때,
    private void OpenATKInfo()
    {
        SoundManager.Instance.PlayEffect("Button_Move_SFX");//아이템 버튼 사운드 추가
        atkInfo_Panel.SetActive(true);
    }
    //공격력 구매 성공 여부
    private void BuyATKSetting()
    {
        int cost = 100;
        int addAtkPower = 2;
        //코인 갯수가 100개 미만이면, buyFailed_Panel.SetActive(true);
        if (Coin_UI.Instance.coinCount < cost)
        {
            // 실패 사운드
            SoundManager.Instance.PlayEffect("Cancel_SFX");

            // 실패 패널 활성화
            buyFailed_Panel.SetActive(true);
            return;
        }

        //보유코인 사용
        Coin_UI.Instance.UseCoin(cost);
        //플레이어 정보 파인드로 찾아주고
        Player_SaveLoad player = FindObjectOfType<Player_SaveLoad>();

        if (player != null)
        {
            //공격력 관련 컴포넌트 불러오기
            Player_ATKBox1 atk1 = player.atkBox1;
            Player_ATKBox2_1 atk2_1 = player.atkBox2_1;
            Player_ATKBox2_2 atk2_2 = player.atkBox2_2;

            //각 이벤트 콜라이더 마다 공격력 증가
            atk1.SetATK1Power(atk1.GetATK1Power() + addAtkPower);
            atk2_1.SetATK2_1Power(atk2_1.GetATK2_1Power() + addAtkPower);
            atk2_2.SetATK2_2Power(atk2_2.GetATK2_2Power() + addAtkPower);

            //어택업 UI증가
            AtkUP_UI.Instance.AddAtkUp();

            //성공 사운드
            SoundManager.Instance.PlayEffect("OK_SFX");

            //성공 패널 활성화
            buySuccess_Panel.SetActive(true);
        }
    }

    //공격력구매 후,닫기 버튼 눌렀을때,
    private void CloseATKInfo()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX"); //닫기 버튼 사운드
        atkInfo_Panel.SetActive(false);
    }
    //====================================================================================================================
    //방어력 관련
    //방어력 구매 버튼 눌렀을때,
    private void OpenDEFInfo()
    {
        SoundManager.Instance.PlayEffect("Button_Move_SFX");//아이템 버튼 사운드
        defInfo_Panel.SetActive(true);
    }

    //방어력 구매 성공여부
    private void BuyDEFSetting()
    {
        int cost = 80;
        int addDefHP = 50;
        
        if (Coin_UI.Instance.coinCount < cost)
        {
            //실패 사운드
            SoundManager.Instance.PlayEffect("Cancel_SFX");

            //실패 패널 활성화
            buyFailed_Panel.SetActive(true);
            return;
        }

        //보유코인 사용
        Coin_UI.Instance.UseCoin(cost);
        //플레이어 정보 파인드로 찾아주고
        Player_SaveLoad player = FindObjectOfType<Player_SaveLoad>();

        if (player != null)
        {
            player.hp.SetMaxHP(player.hp.GetMaxHP() + addDefHP);
        }

        //체력업 UI 갱신
        DefUP_UI.Instance.AddDefUp();

        //성공 사운드
        SoundManager.Instance.PlayEffect("OK_SFX");

        //성공 패널 활성화
        buySuccess_Panel.SetActive(true);

    }
    //방어력 구매 후,닫기 버튼 눌렀을때,
    private void CloseDEFInfo()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX"); //닫기 버튼 사운드
        defInfo_Panel.SetActive(false);
    }
    //===================================================================================================================
    //이동속도 관련
    //이동속도 구매 버튼 눌렀을때,
    private void OpenSPEEDInfo()
    {
        SoundManager.Instance.PlayEffect("Button_Move_SFX");//아이템 버튼 사운드
        speedInfo_Panel.SetActive(true);
    }

    //이동속도 구매 성공여부
    private void BuySPEEDSetting()
    {
        int cost = 70;
        int addSpeed = 1;

        if (Coin_UI.Instance.coinCount < cost)
        {
            //실패 사운드
            SoundManager.Instance.PlayEffect("Cancel_SFX");

            //실패 패널 활성화
            buyFailed_Panel.SetActive(true);
            return;
        }

        //보유코인 사용
        Coin_UI.Instance.UseCoin(cost);
        //플레이어 정보 파인드로 찾아주고
        Player_SaveLoad player = FindObjectOfType<Player_SaveLoad>();

        if (player != null)
        {
            player.move.SetMoveSpeed(player.move.GetMoveSpeed() + addSpeed);
        }

        //스피드업 UI 갱신
        SpeedUP_UI.Instance.AddSpeedUp();

        //성공 사운드
        SoundManager.Instance.PlayEffect("OK_SFX");

        //성공 패널 활성화
        buySuccess_Panel.SetActive(true);

    }

    //이동속도 구매 후,닫기 버튼 눌렀을때,
    private void CloseSPEEDInfo()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX"); //닫기 버튼 사운드
        speedInfo_Panel.SetActive(false);
    }

    //===================================================================================================================
    //열쇠관련
    //열쇠 구매 버튼 눌렀을때,
    private void OpenKEYInfo()
    {
        SoundManager.Instance.PlayEffect("Button_Move_SFX");//아이템 버튼 사운드
        keyInfo_Panel.SetActive(true);
    }

    //열쇠 구매 성공여부
    private void BuyKEYSetting()
    {
        int cost = 150;

        if (Coin_UI.Instance.coinCount < cost)
        {
            // 실패 사운드
            SoundManager.Instance.PlayEffect("Cancel_SFX");

            // 실패 패널 활성화
            buyFailed_Panel.SetActive(true);
            return;
        }

        //보유코인 사용
        Coin_UI.Instance.UseCoin(cost);

        //열쇠추가-갱신은 AddKey에서 할거야.
        KeyUP_UI.Instance.AddKey();

        //성공 사운드
        SoundManager.Instance.PlayEffect("OK_SFX");

        //성공 패널 활성화
        buySuccess_Panel.SetActive(true);

    }

    //열쇠 구매 후,닫기 버튼 눌렀을때,
    private void CloseKEYInfo()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX"); //닫기 버튼 사운드
        keyInfo_Panel.SetActive(false);
    }

    //===================================================================================================================
    //구매성공시 나오는 버튼->닫을 패널==============
    private void SuccessClose()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX"); //닫기 버튼 사운드
        buySuccess_Panel.SetActive(false);
    }
    //구매실패시 나오는 버튼->닫을 패널
    private void FailClose()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX"); //닫기 버튼 사운드
        buyFailed_Panel.SetActive(false);
    }
}
