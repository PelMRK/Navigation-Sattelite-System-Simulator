using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    Mesh mesh;

    List<Vector3> vertices;
    List<int> triangles;

    Vector3 pos;
    private float origin;
    private float height;
    private float radius;
    private int segments = 10;
    private float degrees = 19f;  //19 градусов у спутников в настоящем (ширина конуса радиосигнала) (ширина диаграммы направленности)

    float angle;
    float angleAmount;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.Magnitude(transform.position);
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position);

        vertices = new List<Vector3>();
        pos = new Vector3();

        angleAmount = 2 * (float)System.Math.PI / segments;
        angle = 0.0f;

        vertices.Add(Vector3.zero);
        vertices.Add(new Vector3(0, 0, -origin));

        for (int i = 0; i < segments; i++)
        {
            height = Vector3.Magnitude(transform.position);
            radius = Mathf.Tan((degrees * (Mathf.PI)) / 180) * height;

            pos.x = radius * Mathf.Sin(angle);
            pos.y = radius * Mathf.Cos(angle);
            pos.z = height;

            vertices.Add(new Vector3(-pos.x, -pos.y, -pos.z));

            angle -= angleAmount;
        }

        mesh.vertices = vertices.ToArray();

        triangles = new List<int>();

        for (int i = 2; i < segments + 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i+1);
        }
        
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(segments + 1);

        mesh.triangles = triangles.ToArray();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
