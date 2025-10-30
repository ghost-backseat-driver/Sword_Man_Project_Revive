using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers
{
    //��� �Ŵ����� ������Ʈ���� �θ����� �ϴ� �� ���� ������Ʈ->��Ƽ� ����
    private static GameObject _root;

    //Ǯ�Ŵ��� 
    private static PoolManager _pool;

    //���Ŵ���
    private static MainSceneManager _scene;

    private static void Init()
    {
        if (_root == null)
        {
            _root = new GameObject("@Managers");//_root ������@Managers ��� �̸����� ����� ������Ʈ ����
            Object.DontDestroyOnLoad(_root);
        }
    }
    private static void CreateManager<T>(ref T manager, string name) where T : Component
    {
        if (manager == null)
        {
            Init(); //���� �Լ� ȣ��-> ��Ʈ������ ����

            //���ο� ���� ������Ʈ ����
            GameObject obj = new GameObject(name);

            //�ش� ������Ʈ�� T Ÿ���� �Ŵ��� ������Ʈ�� �߰�
            manager = obj.AddComponent<T>();

            Object.DontDestroyOnLoad(obj);

            //@Managers ��� �� ���� ������Ʈ ������ �ٿ���, ���� ����
            obj.transform.SetParent(_root.transform);
        }
    }

    // Ǯ �Ŵ��� ������
    public static PoolManager Pool
    {
        get //PoolManager�� ������
        {
            CreateManager(ref _pool, "PoolManager");
            return _pool;
        }
    }

    //�� �Ŵ��� �߰� �Ұ�
    public static MainSceneManager Scene
    {
        get
        {
            CreateManager(ref _scene, "SceneManager");
            return _scene;
        }
    }
}
