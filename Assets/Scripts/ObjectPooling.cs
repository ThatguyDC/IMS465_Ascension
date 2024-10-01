
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject PoolerMesh;
    public GameObject ObjToPool;
    public int PoolSize = 20;
    public Vector2 areaSize = new Vector2(10f, 10f); // Define the area of dispersion (x and z dimensions)
    public float SpawnHeight = 10f; // Max height from which to raycast downward
    public LayerMask groundLayer; // LayerMask to specify what is considered "ground"
    private List<GameObject> PooledObjects; // List to store pooled objects

    // Gizmo settings
    public Color gizmoColor = Color.green; // Color for the gizmo in the Scene view

    void Start()
    {
        PoolerMesh.SetActive(false); // Disable the pooler mesh at the start
        PooledObjects = new List<GameObject>();

        // Instantiate the pool of objects
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject obj = Instantiate(ObjToPool);  // Create a new object
            PositionObject(obj);                      // Position the object on the ground layer
            obj.SetActive(true);                      // Activate the object
            PooledObjects.Add(obj);                   // Add it to the pooled list
        }
    }

    // Get an inactive object from the pool
    public GameObject GetPooledObject()
    {
        foreach (var obj in PooledObjects)
        {
            if (!obj.activeInHierarchy)
            {
                PositionObject(obj); // Reposition the object when retrieved
                return obj;
            }
        }

        // If no inactive objects, create a new one
        GameObject newObj = Instantiate(ObjToPool);
        PositionObject(newObj);
        newObj.SetActive(false); // Keep it deactivated until needed
        PooledObjects.Add(newObj);
        return newObj;
    }

    // Position the object on the ground layer
    private void PositionObject(GameObject obj)
    {
        // Randomly position the object within the defined area (x and z coordinates)
        Vector3 randomPosition = new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            0f,  // We'll override the Y value with a set height below
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );

        randomPosition += transform.position; // Offset by the pooler's position

        // Set a fixed Y position (height)
        randomPosition.y = transform.position.y; // Use maxHeight as the fixed spawn height

        obj.transform.position = randomPosition;
        //obj.transform.rotation = Random.rotation; // Optionally add random rotation
    }


    // Draw the debug zone in the scene
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        // Draw a wireframe cube showing the dispersion area
        Vector3 areaCenter = transform.position;
        Vector3 areaSize3D = new Vector3(areaSize.x, 0.1f, areaSize.y); // Set the height of the gizmo to a small value

        // Draw a wireframe cube to represent the bounds
        Gizmos.DrawWireCube(areaCenter, areaSize3D);
    }
}
