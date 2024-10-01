using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject wall;
    private void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(wall);
    }
}
