using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Final_Object : MonoBehaviour
{
    [SerializeField] private string targetScene = "Ending_Scene";

    [Header("씬 전환 버튼")]
    [SerializeField] private Button endingButton;

    [Header("화면 FX")]
    [SerializeField] private ScreenFx screenFx;

    [Header("화면FX 담은 캔버스")]
    [SerializeField] private GameObject screenFxCanvas;

    [Header("엔딩시 기존 패널 아웃")]
    [SerializeField] private GameObject PanelOut;

    private void Start()
    {
        //버튼 이벤트 등록
        endingButton.onClick.AddListener(Ending);
    }

    //중복방지
    private void OnDestroy()
    {
        endingButton.onClick.RemoveListener(Ending);
    }

    private void Ending()
    {
        //사운드 할당할거 불러오고
        SoundManager.Instance.PlayEffect("OK_SFX");
        //패널 띄워진거 비활성화
        PanelOut.SetActive(false);
        //효과 패널 담은 캔버스 활성화
        screenFxCanvas.SetActive(true);
        //패널 효과 플레이 후에 로드씬
        screenFx.Play(() => SceneManager.LoadScene(targetScene));
    }
}
