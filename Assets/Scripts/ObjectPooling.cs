using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject ObjToPool;
    public int PoolSize = 20;
    private List<GameObject> PooledObjects;


    void Start()
    {
        PooledObjects = new List<GameObject>();
        for (int i = 0; i < PoolSize; i++)
        {
            //add random rotation and location quaternion/vector to spawn
            GameObject obj = Instantiate(ObjToPool);
            obj.SetActive(true);
            PooledObjects.Add(obj);
        }

    }

    public GameObject GetPooledObject()
    {
        foreach (var obj in PooledObjects)
        {
            if(!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        GameObject NewObj = Instantiate(ObjToPool);
        NewObj.SetActive(false);
        PooledObjects.Add(NewObj);
        return NewObj;
    }

}
