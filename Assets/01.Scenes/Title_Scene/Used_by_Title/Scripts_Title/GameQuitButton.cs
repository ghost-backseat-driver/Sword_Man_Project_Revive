using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameQuitButton : MonoBehaviour
{
    [Header("게임 종료 버튼")]
    [SerializeField] private Button quitGameButton;
    private void Start()
    {
        //버튼 이벤트 등록
        quitGameButton.onClick.AddListener(QuitGame);
    }
    public void QuitGame()
    {
        Application.Quit(); // 실제 빌드에서는 게임 종료
        Debug.Log("게임 종료"); // 에디터에서 확인용 로그-빌드시 지울것
    }
}
