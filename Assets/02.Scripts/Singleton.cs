using UnityEngine;

//모든 전역접근 가능한 매니저 형태의 스크립트에 부여해줄 싱글톤 패턴 
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    //인스턴스 저장용
    private static T instance;

    //외부에서 접근가능한 인스턴스
    public static T Instance
    {
        get
        {
            //인스턴스가 없으면?
            if (instance == null)
            {
                //인스턴스 있는지 찾고
                instance = FindObjectOfType<T>();
                //그래도 없으면?
                if (instance == null)
                {
                    //새로 오브젝트 만들고
                    GameObject obj = new GameObject(typeof(T).Name);
                    //컴포넌트 붙여
                    instance = obj.AddComponent<T>();
                    //전역접근이니까 돈 디스트로이 -씬 전환시에도 유지되게
                    DontDestroyOnLoad(obj);
                }
            }
            //인스턴스 내놔
            return instance;
        }
    }
    //싱글톤 중복 방지 어웨이크에서 사전 발동
    protected virtual void Awake()
    {
        //인스턴스가 없으면 현재 객체 등록
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        //있으면 현재 객체 파괴
        else
        {
            Destroy(gameObject);
        }
    }
}
