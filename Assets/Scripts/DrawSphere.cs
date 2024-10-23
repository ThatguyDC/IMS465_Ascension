using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSphere : MonoBehaviour
{
    private PlayerTestScript PlayerScript;

    public Transform GroundCheckObj;
    public int segments = 50; // Increase for smoother sphere
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;
    }

    void Update()
    {
        //DrawDebugSphere(GroundCheckObj.position, PlayerScript.GroundCheckDistance);
    }

    void DrawDebugSphere(Vector3 center, float radius)
    {
        float angle = 360f / segments;

        for (int i = 0; i < (segments + 1); i++)
        {
            float theta = Mathf.Deg2Rad * (angle * i);
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            lineRenderer.SetPosition(i, new Vector3(center.x + x, center.y + y, center.z));
        }
    }
}
