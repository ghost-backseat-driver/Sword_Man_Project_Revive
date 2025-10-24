using UnityEngine;

//��� �������� ������ �Ŵ��� ������ ��ũ��Ʈ�� �ο����� �̱��� ���� 
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //�ν��Ͻ� �����
    private static T instance;

    //�ܺο��� ���ٰ����� �ν��Ͻ�
    public static T Instance
    {
        get
        {
            //�ν��Ͻ��� ������?
            if (instance == null)
            {
                //�ν��Ͻ� �ִ��� ã��
                instance = FindObjectOfType<T>();
                //�׷��� ������?
                if (instance == null)
                {
                    //���� ������Ʈ �����
                    GameObject obj = new GameObject(typeof(T).Name);
                    //������Ʈ �ٿ�
                    instance = obj.AddComponent<T>();
                    //���������̴ϱ� �� ��Ʈ���� -�� ��ȯ�ÿ��� �����ǰ�
                    DontDestroyOnLoad(obj);
                }
            }
            //�ν��Ͻ� ����
            return instance;
        }
    }
    //�̱��� �ߺ� ���� �����ũ���� ���� �ߵ�
    protected virtual void Awake()
    {
        //�ν��Ͻ��� ������ ���� ��ü ���
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        //������ ���� ��ü �ı�
        else
        {
            Destroy(gameObject);
        }
    }
}
