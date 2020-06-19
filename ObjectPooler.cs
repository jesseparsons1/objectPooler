using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static List<T> PoolPrefabs<T>(T prefabToPool, int amount, Transform poolParent = null) where T : Component
    {
        List<T> pool = new List<T>();
        for (int i = 0; i < amount; i++)
        {
            GameObject newGameObject = Instantiate(prefabToPool.gameObject, poolParent);

            T newComponent = newGameObject.GetComponent<T>();

            newGameObject.SetActive(false);

            pool.Add(newComponent);
        }

        return pool;
    }

    public static List<T> AddToPool<T>(List<T> pool, T prefabToPool, int amount, Transform poolParent = null) where T : Component
    {
        List<T> prefabsAdded = new List<T>();

        for (int i = 0; i < amount; i++)
        {
            GameObject newGameObject = Instantiate(prefabToPool.gameObject, poolParent);

            T newComponent = newGameObject.GetComponent<T>();

            newGameObject.gameObject.SetActive(false);

            pool.Add(newComponent);
            prefabsAdded.Add(newComponent);
        }

        return prefabsAdded;
    }

    public static T GetFromPool<T>(List<T> pool) where T : Component
    {
        if (pool.Count > 0)
        {
            foreach (T pooledObject in pool)
            {
                if (!pooledObject.gameObject.activeInHierarchy)
                {
                    return pooledObject;
                }
            }

            List<T> newPooledObject = AddToPool(pool, pool[0], 1, pool[0].transform.parent);

            return newPooledObject[0];
        }
        else
        {
            Debug.LogWarning("Pool is empty and ObjectPooler doesn't know what to add to it");
            return null;
        }
    }
}