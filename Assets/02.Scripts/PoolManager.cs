using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{ 
    public static PoolManager Instance { get; private set; }

    private Dictionary<string, object> pools = new Dictionary<string, object>();

    private void Awake()
    {
        if (Instance == null) //�ν��Ͻ��� null�̸�
        {
            Instance = this; //�� �ڽ��� �̱������� �����, ���
            DontDestroyOnLoad(gameObject); //�ı������ʰ� ����
        }
        else
        {
            Destroy(gameObject);  //�ߺ��� �ڽ��� ������ �ı�
        }
    }
    //�� �κ� �̱��� ��ũ��Ʈ �������� ������� �ٲ㼭 ��ü��..
    //�׷� ������ �� �� ������Ʈ�� �پ��־�ߵǴµ�? ���ݵ�? �� ������·� �������? �ƴѰ�? �ϴ� ����

    public void CreatePool<T>(T prefab, int initCount, Transform parent = null) where T : MonoBehaviour
    {
        if (prefab == null) return; //������ ������ ����������

        string key = prefab.name; //key�� �������̸�
        if (pools.ContainsKey(key)) return;  //�̹� ���� �̸��� Ǯ�� ������ ����x

        //����ȯ�� Ǯ�Ŵ��� �ڲ� �ı��Ǿ �̸� �ذ��� �뵵->�θ� PoolManager�� ����(����ȯ�� �Բ� ����)
        Transform rootParent = parent != null ? parent : this.transform;

        //������ �̸����� ���ο� Ǯ�� ��ųʸ��� ����ؼ�, �ʿ��� �� ã�ƾ��� ����
        pools.Add(key, new ObjectPool<T>(prefab, initCount, parent)); //���ο� Ǯ�� ����� ��ųʸ��� ���
    }

    //Ǯ���� ������ �뵵�� �Լ�
    public T GetFromPool<T>(T prefab) where T : MonoBehaviour
    {
        if (prefab == null) return null;

        //�̸����� Ǯ ã�� �õ�->����Ҷ� ��� ������ �̸����� Ǯ�� ã�°�
        if (!pools.TryGetValue(prefab.name, out var box))
        {
            return null; //�������� ������ nulló��
        }

        var pool = box as ObjectPool<T>; //object�� ����� Ǯ�� ���� ���׸� Ÿ������ ĳ����
        if (pool == null) return null;

        var obj = pool.Dequeue();

        // �̹� ������ ��ü��� null üũ
        if (obj == null)
        {
            pool.Rebuild(); // Ǯ �����-������Ʈ Ǯ���� �����
            return pool.Dequeue();
        }
        return obj;
    }
    //���Ϸ��� �ν��Ͻ� Ǯ�� �ǵ������ �Լ�
    public void ReturnPool<T>(T instance) where T : MonoBehaviour
    {
        if (instance == null) return;

        if (!pools.TryGetValue(instance.gameObject.name, out var box))
        {
            //��� Ǯ���� ������ ������ ����
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
