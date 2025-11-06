using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePause : MonoBehaviour
{

    [Header("일시정지 버튼 pause/resume")]
    [SerializeField] private Button puaseButton; //설정열면 멈출 버튼
    [SerializeField] private Button resumeButton; //닫기 누르면 다시 재생할 버튼

    [Header("ToTitle버튼 누를때도 resume")]
    [SerializeField] private Button toTitleButton;

    [Header("pause 되면 어두워질 화면")]
    [SerializeField] private GameObject pausePanel;

    //원복에 필요한 변수
    private float originalTimeScale;

    private void Awake()
    {
        //타임스케일 초기값 저장하고 시작
        originalTimeScale = Time.timeScale;
    }

    private void Start()
    {
        //버튼 이벤트 등록
        puaseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        toTitleButton.onClick.AddListener(Resume);
    }

    //중복방지
    private void OnDestroy()
    {
        puaseButton.onClick.RemoveListener(Pause);
        resumeButton.onClick.RemoveListener(Resume);
        toTitleButton.onClick.RemoveListener(Resume);
    }

    //일시정지 눌렀을때,
    private void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    //패널 닫을때->일시정지 해제
    private void Resume()
    {
        Time.timeScale = originalTimeScale;
        pausePanel.SetActive(false);
    }
}
