using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Algorithms;

public class HilbertMesh : MonoBehaviour
{

    Mesh mesh;
    MeshFilter m_MeshFilter;
    LineRenderer m_LineRenderer;
    Vector3[] vertices;
    public int xSize = 16;
    public int ySize = 16;

    [SerializeField]
    HilbertCurve m_HilbertCurve;

    private int m_startN;
    private void Start()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
        m_MeshFilter = GetComponent<MeshFilter>();
        m_startN = m_HilbertCurve.m_n;
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < m_HilbertCurve.m_Points.Count-1; i++)
        {
            Gizmos.DrawLine(m_HilbertCurve.m_Points[i], m_HilbertCurve.m_Points[i + 1]);            
        }
        
    }
    private void DrawHilbert()
    {
        m_LineRenderer.positionCount = m_HilbertCurve.m_Points.Count;
        for (int i = 0; i < m_HilbertCurve.m_Points.Count; i++)
        {
            m_LineRenderer.SetPosition(i, m_HilbertCurve.m_Points[i]);
        }
    }

    private void Update()
    {
        DrawHilbert();
    }

    private void GenerateHilbertMesh()
    {
        m_MeshFilter.mesh = mesh = new Mesh();
        mesh.name = "HilbertMesh";

        xSize = Mathf.ClosestPowerOfTwo(m_HilbertCurve.m_Points.Count);

        ySize = xSize;

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];


        for (int i = 0; i < m_HilbertCurve.m_Points.Count; i++)
        {
            m_LineRenderer.SetPosition(i, m_HilbertCurve.m_Points[i]);
        }

        mesh.vertices = vertices;

        int[] triangles = new int[xSize * ySize * 6];

        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }

        mesh.triangles = triangles;

        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
                uv[i] = new Vector2(x / xSize, y / ySize);
            }
        }

        mesh.uv = uv;
    }
}
