using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    //��Ʈ��Ÿ�� ���̸� ���� �� �ε��ϱ�
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //���� �� �ٽ� �ҷ�����
    public void ReloadScene()
    {
        //���� ���� ��ü �ҷ�����
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    //����� �����ͷ� �� �ҷ�����(�߰��Ұ�)

    //���� �� �̸� ��ȯ��
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
