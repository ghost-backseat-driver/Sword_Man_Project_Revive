using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("UI 오브젝트 연결")]
    [SerializeField] private GameObject gameOverUI; // 전체 패널
    [SerializeField] private Button restartButton; // 재시작 버튼
    public Text gameOverText; // "GameOver" 텍스트

    private bool isGameOver = false;

    private void Start()
    {
        // UI 비활성화
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // 버튼에 클릭 이벤트 등록
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void ShowGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = "GameOver";

        //재생중인 BGM 정지
        SoundManager.Instance.StopBGM();
        //+게임오버 사운드 추가
        SoundManager.Instance.PlayEffect("GameOver_SFX");
    }

    private void RestartGame()
    {
        //여기에 버튼 사운드 추가
        SoundManager.Instance.PlayEffect("Restart_Btn_SFX");
        // 현재 씬 다시 로드
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
