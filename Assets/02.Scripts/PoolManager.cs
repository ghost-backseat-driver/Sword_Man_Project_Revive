using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{ 
    public static PoolManager Instance { get; private set; }

    private Dictionary<string, object> pools = new Dictionary<string, object>();

    private void Awake()
    {
        if (Instance == null) //인스턴스가 null이면
        {
            Instance = this; //내 자신을 싱글톤으로 만들고, 등록
            DontDestroyOnLoad(gameObject); //파괴되지않고 유지
        }
        else
        {
            Destroy(gameObject);  //중복된 자신이 있으면 파괴
        }
    }
    //이 부분 싱글톤 스크립트 빼놓은거 상속으로 바꿔서 교체할..
    //그럼 무조건 씬 내 오브젝트에 붙어있어야되는데? 에반데? 왜 상속형태로 만들었지? 아닌가? 일단 납둬

    public void CreatePool<T>(T prefab, int initCount, Transform parent = null) where T : MonoBehaviour
    {
        if (prefab == null) return; //프리팹 없으면 만들지말고

        string key = prefab.name; //key는 프리팹이름
        if (pools.ContainsKey(key)) return;  //이미 같은 이름의 풀이 있으면 생성x

        //씬전환시 풀매니저 자꾸 파괴되어서 이를 해결할 용도->부모를 PoolManager로 통일(씬전환시 함께 유지)
        Transform rootParent = parent != null ? parent : this.transform;

        //프리팹 이름으로 새로운 풀을 딕셔너리에 등록해서, 필요할 때 찾아쓰기 위해
        pools.Add(key, new ObjectPool<T>(prefab, initCount, parent)); //새로운 풀을 만들고 딕셔너리에 등록
    }

    //풀에서 꺼내는 용도의 함수
    public T GetFromPool<T>(T prefab) where T : MonoBehaviour
    {
        if (prefab == null) return null;

        //이름으로 풀 찾기 시도->등록할때 썼던 프리팹 이름으로 풀을 찾는거
        if (!pools.TryGetValue(prefab.name, out var box))
        {
            return null; //프리팹이 없으면 null처리
        }

        var pool = box as ObjectPool<T>; //object로 저장된 풀을 원래 제네릭 타입으로 캐스팅
        if (pool == null) return null;

        var obj = pool.Dequeue();

        // 이미 삭제된 객체라면 null 체크
        if (obj == null)
        {
            pool.Rebuild(); // 풀 재생성-오브젝트 풀에서 재생성
            return pool.Dequeue();
        }
        return obj;
    }
    //사용완료한 인스턴스 풀로 되돌리기용 함수
    public void ReturnPool<T>(T instance) where T : MonoBehaviour
    {
        if (instance == null) return;

        if (!pools.TryGetValue(instance.gameObject.name, out var box))
        {
            //어느 풀에도 속하지 않으면 제거
            Destroy(instance.gameObject);
            return;
        }

        var pool = box as ObjectPool<T>;

        if (pool != null)
        {
            pool.Enqueue(instance);
        }
    }
}
