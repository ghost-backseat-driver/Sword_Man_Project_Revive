using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss_Entrance : MonoBehaviour
{
    [SerializeField] private string targetScene = "Boss_Scene";

    [Header("씬 전환 버튼")]
    [SerializeField] private Button sceneChangeButton;
    
    [Header("화면 FX")]
    [SerializeField] private ScreenFx screenFx;

    [Header("실패 패널")]
    [SerializeField] private GameObject EnterFailPanel;

    [Header("실패패널 확인버튼")]
    [SerializeField] private Button FailButton;

    [Header("보스입장 성공시 패널 아웃")]
    [SerializeField] private GameObject PanelOut;

    private void Start()
    {
        //버튼 이벤트 등록
        sceneChangeButton.onClick.AddListener(StartBoss);
        FailButton.onClick.AddListener(CloseFailPanel);
    }

    //중복방지
    private void OnDestroy()
    {
        sceneChangeButton.onClick.RemoveListener(StartBoss);
        FailButton.onClick.AddListener(CloseFailPanel);
    }

    private void StartBoss()
    {
        int cost = 1; //열쇠 코스트용

        if (KeyUP_UI.Instance.keyCount < cost)
        {
            //실패 사운드
            SoundManager.Instance.PlayEffect("Cancel_SFX");

            //실패 패널 활성화
            EnterFailPanel.SetActive(true);
            return;
        }

        //플레이어 정보 파인드로 찾아주고
        Player_SaveLoad player = FindObjectOfType<Player_SaveLoad>();
        //배경음 꺼주고
        SoundManager.Instance.StopBGM();
        //사운드 할당할거 불러오고
        SoundManager.Instance.PlayEffect("OK_SFX");
        //패널 띄워진거 비활성화
        PanelOut.SetActive(false);
        //패널 효과 플레이 후에 로드씬
        screenFx.Play(() => SceneManager.LoadScene(targetScene));
    }
    private void CloseFailPanel()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX"); //닫기 버튼 사운드
        EnterFailPanel.SetActive(false);
    }
}
