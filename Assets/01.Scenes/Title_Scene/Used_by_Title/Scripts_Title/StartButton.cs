using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private string targetScene = "Stage1_Scene";

    [Header("씬 전환 버튼")]
    [SerializeField] private Button sceneChangeButton;

    private void Start()
    {
        //버튼 이벤트 등록
        sceneChangeButton.onClick.AddListener(SceneChange);
    }

    //중복방지
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
