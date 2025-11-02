using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private Queue<T> pool = new Queue<T>();

    // 풀을 담아둘 부모 오브젝트(컨테이너 역할)
    public Transform Root { get; private set; }

    // 풀 생성자 
    public ObjectPool(T prefab, int initCount, Transform parent = null)
    {
        this.prefab = prefab;
        //풀 컨테이너 생성(Root) -> 이름은 "[프리팹 이름]"__pool
        Root = new GameObject($"{prefab.name}_pool").transform;

        if (parent != null)
        {
            Root.SetParent(parent, false);
        }

        //처음에 지정한 갯수 만큼 미리 만들어서 큐에 넣어둔다.
        for (int i = 0; i < initCount; i++)
        {
            var inst = Object.Instantiate(prefab, Root); //Root의 자식으로 생성
            
            //이름 지정
            inst.name = prefab.name; //이름은 프리팹과 동일하게
            inst.gameObject.SetActive(false); //꺼진상태로 대기
            pool.Enqueue(inst); //(큐에) 넣기
        }
    }
    //풀에서 꺼내서 사용
    public T Dequeue()
    {
        if (pool.Count == 0)
        {
            // 풀이 비었을 경우, 새로 생성해서 반환
            var newObj = Object.Instantiate(prefab, Root);
            newObj.name = prefab.name;
            return newObj;
        }
        var inst = pool.Dequeue(); //뺄거 남아있으면, 하나 빼서 사용

        //파괴된 객체가 남아있다면 건너뛰기
        if (inst == null)
        {
            return Dequeue();
        }

        inst.gameObject.SetActive(true); //꺼낸거 활성화
        return inst; //활성화 한 것 사용
    }
    //풀 반환용
    public void Enqueue(T instance)
    {
        if (instance == null) return; //사용했던거 넣을게 없으면 null처리

        instance.gameObject.SetActive(false); //집어넣을거 비활성화
        instance.transform.SetParent(Root);
        pool.Enqueue(instance); //비활성화 한 것 넣기
    }

    //Root 파괴로 풀 자체가 무너져버렸을때 대비용
    public void Rebuild()
    {
        if (Root == null)
        {
            Root = new GameObject($"{prefab.name}_pool").transform;
            Root.SetParent(PoolManager.Instance.transform, false);
        }
    }
}
