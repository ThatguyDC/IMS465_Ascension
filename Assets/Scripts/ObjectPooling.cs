/*using System.Collections;
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
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject PoolerMesh;
    public GameObject ObjToPool;
    public int PoolSize = 20;
    public Vector2 areaSize = new Vector2(10f, 10f); // Define the area of dispersion (x and z dimensions)
    public float maxHeight = 10f; // Max height from which to raycast downward
    public LayerMask groundLayer; // LayerMask to specify what is considered "ground"
    public Terrain terrain; // Reference to the terrain in the scene
    private List<GameObject> PooledObjects;

    // Gizmo settings
    public Color gizmoColor = Color.green; // Color for the gizmo in the Scene view

    void Start()
    {
        PoolerMesh.SetActive(false);
        PooledObjects = new List<GameObject>();
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject obj = Instantiate(ObjToPool);
            PositionObject(obj);
            obj.SetActive(true);
            PooledObjects.Add(obj);
        }
    }

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

        GameObject newObj = Instantiate(ObjToPool);
        PositionObject(newObj);
        newObj.SetActive(false);
        PooledObjects.Add(newObj);
        return newObj;
    }

    private void PositionObject(GameObject obj)
    {
        // Randomly position within the defined area
        Vector3 randomPosition = new Vector3(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            maxHeight,
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );

        randomPosition += transform.position; // Offset by the object's position

        // Get the terrain height at the (x, z) coordinates
        if (terrain != null)
        {
            randomPosition.y = terrain.SampleHeight(randomPosition) + terrain.transform.position.y;
        }
        else
        {
            // Use a raycast if no terrain is assigned
            RaycastHit hit;
            if (Physics.Raycast(randomPosition, Vector3.down, out hit, Mathf.Infinity, groundLayer))
            {
                randomPosition.y = hit.point.y; // Set the Y position to the ground's height
            }
            else
            {
                randomPosition.y = 0; // Default to ground level if no hit (adjust as needed)
            }
        }

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



