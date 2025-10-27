using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
    [Header("세팅 패널")]
    [SerializeField] private GameObject settingPanel; // 세팅 패널
    [SerializeField] private GameObject menuPanel; // 메뉴 패널


    [Header("세팅 버튼 open/close")]
    [SerializeField] private Button openButton;  // 타이틀 화면에서 세팅 버튼
    [SerializeField] private Button closeButton; // 세팅 패널 안의 뒤로가기 버튼

    private void Start()
    {
        // 패널 초기 상태 비활성화
        settingPanel.SetActive(false);

        // 버튼 이벤트 등록
        openButton.onClick.AddListener(OpenSettings);
        closeButton.onClick.AddListener(CloseSettings);
    }

    //중복방지
    private void OnDestroy()
    {
        openButton.onClick.RemoveListener(OpenSettings);
        closeButton.onClick.RemoveListener(CloseSettings);
    }

    private void OpenSettings()
    {
        SoundManager.Instance.PlayEffect("Button_Move_SFX");//사운드 추가
        settingPanel.SetActive(true); //열리면 세팅패널 활성화
        menuPanel.SetActive(false); //메뉴패널 비활성화

    }

    private void CloseSettings()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX");
        settingPanel.SetActive(false); //닫으면 세팅패널 비활성화
        menuPanel.SetActive(true); //닫으면 메뉴패널 활성화
    }
}
