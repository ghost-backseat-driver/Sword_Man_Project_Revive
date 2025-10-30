using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private T prefab;
    private Queue<T> pool = new Queue<T>();

    // Ǯ�� ��Ƶ� �θ� ������Ʈ(�����̳� ����)
    public Transform Root { get; private set; }

    // Ǯ ������ 
    public ObjectPool(T prefab, int initCount, Transform parent = null)
    {
        this.prefab = prefab;
        //Ǯ �����̳� ����(Root) -> �̸��� "[������ �̸�]"__pool
        Root = new GameObject($"{prefab.name}_pool").transform;

        if (parent != null)
        {
            Root.SetParent(parent, false);
        }

        //ó���� ������ ���� ��ŭ �̸� ���� ť�� �־�д�.
        for (int i = 0; i < initCount; i++)
        {
            var inst = Object.Instantiate(prefab, Root); //Root�� �ڽ����� ����
            
            //�̸� ����
            inst.name = prefab.name; //�̸��� �����հ� �����ϰ�
            inst.gameObject.SetActive(false); //�������·� ���
            pool.Enqueue(inst); //(ť��) �ֱ�
        }
    }
    //Ǯ���� ������ ���
    public T Dequeue()
    {
        if (pool.Count == 0)
        {
            // Ǯ�� ����� ���, ���� �����ؼ� ��ȯ
            var newObj = Object.Instantiate(prefab, Root);
            newObj.name = prefab.name;
            return newObj;
        }
        var inst = pool.Dequeue(); //���� ����������, �ϳ� ���� ���

        //�ı��� ��ü�� �����ִٸ� �ǳʶٱ�
        if (inst == null)
        {
            return Dequeue();
        }

        inst.gameObject.SetActive(true); //������ Ȱ��ȭ
        return inst; //Ȱ��ȭ �� �� ���
    }
    //Ǯ ��ȯ��
    public void Enqueue(T instance)
    {
        if (instance == null) return; //����ߴ��� ������ ������ nulló��

        instance.gameObject.SetActive(false); //��������� ��Ȱ��ȭ
        instance.transform.SetParent(Root);
        pool.Enqueue(instance); //��Ȱ��ȭ �� �� �ֱ�
    }

    //Root �ı��� Ǯ ��ü�� �������������� ����
    public void Rebuild()
    {
        if (Root == null)
        {
            Root = new GameObject($"{prefab.name}_pool").transform;
            Root.SetParent(PoolManager.Instance.transform, false);
        }
    }
}
