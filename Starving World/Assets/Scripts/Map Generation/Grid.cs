using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour
{
    public int XSize, YSize;
    private Vector3[] vertices;
    private Mesh m_mesh;

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = m_mesh = new Mesh();
        m_mesh.name = "Procedural Grid";
        // Amount of vertices
        vertices = new Vector3[(XSize + 1) * (YSize + 1)];
        for (int i = 0, y = 0; y <= YSize; y++)
        {
            for (int x = 0; x <= XSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y, Random.Range(0,2));
            }
        }

        m_mesh.vertices = vertices;

        // Create Triangles
        int[] triangles = new int[XSize * YSize * 6]; // * 6 for creating two triangles.
        for (int ti = 0, vi = 0, y = 0; y < YSize; y++, vi++) {
            for (int x = 0; x < XSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + XSize + 1;
                triangles[ti + 5] = vi + XSize + 2;
            }
        }
        m_mesh.triangles = triangles;
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
