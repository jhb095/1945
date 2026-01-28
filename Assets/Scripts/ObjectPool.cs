using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools;

    private readonly Dictionary<string, List<GameObject>> poolDict = new();
    private readonly Dictionary<string, Pool> poolDefs = new();

    protected override void Awake()
    {
        base.Awake();

        foreach (Pool pool in pools)
        {
            if (!poolDefs.ContainsKey(pool.key))
                poolDefs.Add(pool.key, pool);

            List<GameObject> objList = new();
            poolDict.Add(pool.key, objList);

            AddPoolSize(pool.key);
        }
    }

    public GameObject GetObject(string key)
    {
        if (poolDict.ContainsKey(key))
        {
            foreach(GameObject obj in poolDict[key])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.SetParent(null);
                    obj.transform.localPosition = Vector2.zero;
                    return obj;
                }
            }

            // 부족한 경우 생성
            return AddPoolSize(key);
        }

        return null;
    }

    // 풀이 꽉차면 추가 생성
    private GameObject AddPoolSize(string key)
    {
        if(poolDefs.TryGetValue(key, out Pool def))
        {
            for(int i = 0; i < def.size; i++)
            {
                GameObject obj = Instantiate(def.prefab);
                obj.SetActive(false);
                poolDict[key].Add(obj);
            }

            return poolDict[key][^def.size];
        }

        return null;
    }
}
