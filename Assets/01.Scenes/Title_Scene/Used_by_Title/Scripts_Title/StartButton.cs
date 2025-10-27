using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private string targetScene = "Stage1_Scene";

    [Header("씬 전환 버튼")]
    [SerializeField] private Button sceneChangeButton;

    [Header("화면 FX")]
    [SerializeField] private ScreenFx screenFx;

    //StartGame 누르면 비활성화 할 메뉴패널
    [Header("메뉴 비활성화용")]
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        //버튼 이벤트 등록
        sceneChangeButton.onClick.AddListener(StartGame);
    }

    //중복방지
    private void OnDestroy()
    {
        sceneChangeButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        //사운드 할당할거 불러오고
        SoundManager.Instance.PlayEffect("OK_SFX");
        //메뉴패널 안보이게 비활성화
        menuPanel.SetActive(false);
        //패널 효과 플레이 후에 로드씬
        screenFx.Play(() => SceneManager.LoadScene(targetScene));
    }
}
