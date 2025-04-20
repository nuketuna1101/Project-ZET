using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : BaseSingleton<PoolManager>
{
    public GameObject[] prefabs;
    private Dictionary<int, Queue<GameObject>> pools = new Dictionary<int, Queue<GameObject>>();
    private const int initPoolCount = 10;

    protected override void Awake()
    {
        base.Awake();

        // 프리팹 종류별 풀 생성
        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new Queue<GameObject>();
        }
        // 풀 초기화
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            for (int j = 0; j < initPoolCount; j++)
            {
                GameObject obj;
                obj = Instantiate(prefabs[i]);
                obj.GetComponent<IPoolable>()?.Initialize(o => ReturnObject(i, o));
                obj.SetActive(false);
                pools[i].Enqueue(obj);
            }
        }
    }

    public GameObject GetObject(int prefabIndex, Vector3 position, Quaternion rotation)
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Debug.LogError($"Prefab NOT Exist for {prefabIndex} index");
            return null;
        }

        GameObject obj;
        if (pools[prefabIndex].Count > 0)
        {
            obj = pools[prefabIndex].Dequeue();
        }
        else
        {
            // 요구사항 : Pool에 데이터가 없어도 새로 생성하지 않는다.
            Debug.LogError($"ObjectPool suffers LACK OF AVAILABLE Poolable item");
            return null;
        }

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);
        obj.GetComponent<IPoolable>()?.OnSpawn();
        return obj;
    }

    public void ReturnObject(int prefabIndex, GameObject obj)
    {
        if (!pools.ContainsKey(prefabIndex))
        {
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        pools[prefabIndex].Enqueue(obj);
    }
}