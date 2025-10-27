using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private string targetScene = "Stage1_Scene";

    [Header("�� ��ȯ ��ư")]
    [SerializeField] private Button sceneChangeButton;

    [Header("ȭ�� FX")]
    [SerializeField] private ScreenFx screenFx;

    //StartGame ������ ��Ȱ��ȭ �� �޴��г�
    [Header("�޴� ��Ȱ��ȭ��")]
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        //��ư �̺�Ʈ ���
        sceneChangeButton.onClick.AddListener(StartGame);
    }

    //�ߺ�����
    private void OnDestroy()
    {
        sceneChangeButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        //���� �Ҵ��Ұ� �ҷ�����
        SoundManager.Instance.PlayEffect("OK_SFX");
        //�޴��г� �Ⱥ��̰� ��Ȱ��ȭ
        menuPanel.SetActive(false);
        //�г� ȿ�� �÷��� �Ŀ� �ε��
        screenFx.Play(() => SceneManager.LoadScene(targetScene));
    }
}
