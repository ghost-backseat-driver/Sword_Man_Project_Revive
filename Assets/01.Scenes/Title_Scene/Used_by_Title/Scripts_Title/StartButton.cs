using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private string targetScene = "Stage1_Scene";

    [Header("�� ��ȯ ��ư")]
    [SerializeField] private Button sceneChangeButton;

    private void Start()
    {
        //��ư �̺�Ʈ ���
        sceneChangeButton.onClick.AddListener(SceneChange);
    }

    //�ߺ�����
    private void OnDestroy()
    {
        sceneChangeButton.onClick.AddListener(SceneChange);
    }

    private void SceneChange()
    {
        SoundManager.Instance.PlayEffect("OK_SFX");
        SceneManager.LoadScene(targetScene);
    }
}
