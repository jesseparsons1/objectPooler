using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static List<T> PoolPrefabs<T>(T prefab, int amount, Transform poolParent = null) where T : Component
    {
        //List of objects in the pool
        List<T> pool = new List<T>();

        for (int i = 0; i < amount; i++)
        {
            //Create new instance of object and set inactive
            GameObject newInstance = Instantiate(prefab.gameObject, poolParent);
            newInstance.SetActive(false);

            //Get component of type T and add to pool
            T newComponent = newInstance.GetComponent<T>();
            pool.Add(newComponent);
        }

        return pool;
    }

    public static List<T> AddToPool<T>(List<T> pool, T prefab, int amount, Transform poolParent = null) where T : Component
    {
        //List of objects added to the pool
        List<T> prefabsAdded = new List<T>();

        for (int i = 0; i < amount; i++)
        {
            //Create new instance of object and set inactive
            GameObject newInstance = Instantiate(prefab.gameObject, poolParent);
            newInstance.SetActive(false);

            //Get component of type T and add to pool
            T newComponent = newInstance.GetComponent<T>();
            pool.Add(newComponent);
            prefabsAdded.Add(newComponent);
        }

        return prefabsAdded;
    }

    public static T GetFromPool<T>(List<T> pool, T extraPrefab, bool setActive = true, Transform poolParent = null) where T : Component
    {
        //Search through existing objects in pool
        foreach (T pooledObject in pool)
        {
            //If one is inactive, return it, and set active according to argument
            if (!pooledObject.gameObject.activeInHierarchy)
            {
                pooledObject.gameObject.SetActive(setActive);
                return pooledObject;
            }
        }

        //Otherwise add a new object to the pool
        List<T> prefabsAdded = AddToPool(pool, extraPrefab, 1, poolParent);

        return prefabsAdded[0];
    }
}