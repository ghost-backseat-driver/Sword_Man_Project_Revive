using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    //스트링타입 씬이름 으로 씬 로드하기
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //현재 씬 다시 불러오기
    public void ReloadScene()
    {
        //현재 씬의 객체 불러오기
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    //저장된 데이터로 씬 불러오기(추가할것)

    //현재 씬 이름 반환용
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
