using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("UI ������Ʈ ����")]
    [SerializeField] private GameObject gameOverUI; // ��ü �г�
    [SerializeField] private Button restartButton; // ����� ��ư
    public Text gameOverText; // "GameOver" �ؽ�Ʈ

    private bool isGameOver = false;

    private void Start()
    {
        // UI ��Ȱ��ȭ
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        // ��ư�� Ŭ�� �̺�Ʈ ���
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

        //������� BGM ����
        SoundManager.Instance.StopBGM();
        //+���ӿ��� ���� �߰�
        SoundManager.Instance.PlayEffect("GameOver_SFX");
    }

    private void RestartGame()
    {
        //���⿡ ��ư ���� �߰�
        SoundManager.Instance.PlayEffect("Restart_Btn_SFX");
        // ���� �� �ٽ� �ε�
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
