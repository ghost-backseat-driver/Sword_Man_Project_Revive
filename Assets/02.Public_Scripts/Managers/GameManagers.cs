using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers
{
    //모든 매니저들 오브젝트들의 부모역할을 하는 빈 게임 오브젝트->모아서 관리
    private static GameObject _root;

    //풀매니저 
    private static PoolManager _pool;

    //씬매니저
    private static MainSceneManager _scene;

    private static void Init()
    {
        if (_root == null)
        {
            _root = new GameObject("@Managers");//_root 없으면@Managers 라는 이름으로 빈게임 오브젝트 생성
            Object.DontDestroyOnLoad(_root);
        }
    }
    private static void CreateManager<T>(ref T manager, string name) where T : Component
    {
        if (manager == null)
        {
            Init(); //위에 함수 호출-> 루트없으면 생성

            //새로운 게임 오브젝트 생성
            GameObject obj = new GameObject(name);

            //해당 오브젝트에 T 타입의 매니저 컴포넌트를 추가
            manager = obj.AddComponent<T>();

            Object.DontDestroyOnLoad(obj);

            //@Managers 라는 빈 게임 오브젝트 밑으로 붙여서, 계층 정리
            obj.transform.SetParent(_root.transform);
        }
    }

    // 풀 매니저 접근자
    public static PoolManager Pool
    {
        get //PoolManager의 접근자
        {
            CreateManager(ref _pool, "PoolManager");
            return _pool;
        }
    }

    //씬 매니저 추가 할것
    public static MainSceneManager Scene
    {
        get
        {
            CreateManager(ref _scene, "SceneManager");
            return _scene;
        }
    }
}
