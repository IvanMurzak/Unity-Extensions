using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasMesh : Graphic
{
    // Inspector properties
    public Mesh mesh = null;

	public Mesh Mesh
	{
		get
		{
			if (mesh == null)
			{
				mesh = GetComponent<MeshFilter>()?.mesh;
				SetVerticesDirty();
				SetMaterialDirty();
			}
			return mesh;
		}
	}

	protected override void OnRectTransformDimensionsChange()
	{
		base.OnRectTransformDimensionsChange();
		SetVerticesDirty();
		SetMaterialDirty();
	}


	/// <summary>
	/// Callback function when a UI element needs to generate vertices.
	/// </summary>
	/// <param name="vh">VertexHelper utility.</param>
	protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (Mesh == null) return;
        // Get data from mesh
        var uvs = Mesh.uv;
        if (uvs.Length < Mesh.vertices.Length)
            uvs = new Vector2[Mesh.vertices.Length];
        // Get mesh bounds parameters
        Vector2 meshMin = Mesh.bounds.min;
        Vector2 meshSize = Mesh.bounds.size;
        // Add scaled vertices
        for (int ii = 0; ii < Mesh.vertices.Length; ii++)
        {
            Vector2 v = Mesh.vertices[ii];
            v.x = (v.x - meshMin.x) / meshSize.x;
            v.y = (v.y - meshMin.y) / meshSize.y;
            v = Vector2.Scale(v - rectTransform.pivot, rectTransform.rect.size);
            vh.AddVert(v, color, uvs[ii]);
        }
        // Add triangles
        var tris = Mesh.triangles;
        for (int ii = 0; ii < tris.Length; ii += 3)
            vh.AddTriangle(tris[ii], tris[ii + 1], tris[ii + 2]);
    }

    /// <summary>
    /// Converts a vertex in mesh coordinates to a point in world coordinates.
    /// </summary>
    /// <param name="vertex">The input vertex.</param>
    /// <returns>A point in world coordinates.</returns>
    public Vector3 TransformVertex(Vector3 vertex)
    {
        // Convert vertex into local coordinates
        Vector2 v;
        v.x = (vertex.x - Mesh.bounds.min.x) / Mesh.bounds.size.x;
        v.y = (vertex.y - Mesh.bounds.min.y) / Mesh.bounds.size.y;
        v = Vector2.Scale(v - rectTransform.pivot, rectTransform.rect.size);
        // Convert from local into world
        return transform.TransformPoint(v);
    }

    /// <summary>
    /// Converts a vertex in world coordinates into a vertex in mesh coordinates.
    /// </summary>
    /// <param name="vertex">The input vertex.</param>
    /// <returns>A point in mesh coordinates.</returns>
    public Vector3 InverseTransformVertex(Vector3 vertex)
    {
        // Convert from world into local coordinates
        Vector2 v = transform.InverseTransformPoint(vertex);
        // Convert into mesh coordinates
        v.x /= rectTransform.rect.size.x;
        v.y /= rectTransform.rect.size.y;
        v += rectTransform.pivot;
        v = Vector2.Scale(v, Mesh.bounds.size);
        v.x += Mesh.bounds.min.x;
        v.y += Mesh.bounds.min.y;
        return v;
    }
}