using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
    [Header("���� �г�")]
    [SerializeField] private GameObject settingPanel; // ���� �г�
    [SerializeField] private GameObject menuPanel; // �޴� �г�


    [Header("���� ��ư open/close")]
    [SerializeField] private Button openButton;  // Ÿ��Ʋ ȭ�鿡�� ���� ��ư
    [SerializeField] private Button closeButton; // ���� �г� ���� �ڷΰ��� ��ư

    private void Start()
    {
        // �г� �ʱ� ���� ��Ȱ��ȭ
        settingPanel.SetActive(false);

        // ��ư �̺�Ʈ ���
        openButton.onClick.AddListener(OpenSettings);
        closeButton.onClick.AddListener(CloseSettings);
    }

    //�ߺ�����
    private void OnDestroy()
    {
        openButton.onClick.RemoveListener(OpenSettings);
        closeButton.onClick.RemoveListener(CloseSettings);
    }

    private void OpenSettings()
    {
        SoundManager.Instance.PlayEffect("Button_Move_SFX");//���� �߰�
        settingPanel.SetActive(true); //������ �����г� Ȱ��ȭ
        menuPanel.SetActive(false); //�޴��г� ��Ȱ��ȭ

    }

    private void CloseSettings()
    {
        SoundManager.Instance.PlayEffect("Cancel_SFX");
        settingPanel.SetActive(false); //������ �����г� ��Ȱ��ȭ
        menuPanel.SetActive(true); //������ �޴��г� Ȱ��ȭ
    }
}
