using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;

public static class MathHelper
{
    static readonly double _EPSILON = 100;
    static readonly double EPSILON = 1 / _EPSILON;

    public class Vector3Comparer : IEqualityComparer<Vector3>
    {
        public bool Equals(Vector3 x, Vector3 y)
        {
            return Vector3.Distance(x, y) < EPSILON;
        }

        public int GetHashCode(Vector3 obj)
        {
            return ((int)(obj.x * _EPSILON)).GetHashCode() 
                 ^ ((int)(obj.y * _EPSILON)).GetHashCode() 
                 ^ ((int)(obj.z * _EPSILON)).GetHashCode();
        }
    }
    public class Vector2Comparer : IEqualityComparer<Vector2>
    {
        public bool Equals(Vector2 x, Vector2 y)
        {
            return Vector2.Distance(x, y) < EPSILON;
        }

        public int GetHashCode(Vector2 obj)
        {
            return ((int)(obj.x * _EPSILON)).GetHashCode()
                 ^ ((int)(obj.y * _EPSILON)).GetHashCode();
        }
    }
    static readonly Vector3Comparer v3Comparer = new Vector3Comparer();
    static readonly Vector2Comparer v2Comparer = new Vector2Comparer();

    public static Mesh BuildLine(List<Vector3> pointsOut, List<Vector3> pointsIn, Vector3 offset)
    {
        if (pointsOut.Count < 2) return null;
        Mesh m = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<Vector3> norms = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        pointsOut = pointsOut.ChangeEach(x => x.SetZ(0));
        pointsIn = pointsIn.ChangeEach(x => x.SetZ(0));

        var nearest = pointsIn.OrderBy(x => Vector3.Distance(x, pointsOut[0])).First();
        var inOffset = pointsIn.IndexOf(nearest);

        var up = Vector3.back;

        for (int j = 0; j < pointsOut.Count; j++)
        {
            verts.Add(offset + pointsOut[j]);
            verts.Add(offset + pointsIn[(j + inOffset) % pointsIn.Count]);
            uv.Add(new Vector2(j % 2, 1));
            uv.Add(new Vector2(j % 2, 0));
            norms.Add(up);
            norms.Add(up);
        }
        m.vertices = verts.ToArray();
        m.normals = norms.ToArray();
        m.uv = uv.ToArray();

        List<int> tris = new List<int>();
        for (int i = 0; i < m.vertices.Length; i++)
        {
            if (i % 2 == 0)
            {
                tris.Add((i + 2) % m.vertices.Length);
                tris.Add((i + 1) % m.vertices.Length);
                tris.Add(i % m.vertices.Length);
            }
            else
            {
                tris.Add(i % m.vertices.Length);
                tris.Add((i + 1) % m.vertices.Length);
                tris.Add((i + 2) % m.vertices.Length);
            }
        }
        m.triangles = tris.ToArray();

        m.name = "pathMesh";
        m.RecalculateNormals();
        m.RecalculateBounds();
        // m.Optimize();
        return m;
    }

    public static Vector3 GetNearestPointOnLine(Vector3 l1, Vector3 l2, Vector3 p)
    {
        Vector3 c = p - l1;   // Vector from a to Point
        Vector3 v = (l2 - l1).normalized; // Unit Vector from a to b
        float d = (l2 - l1).magnitude; // Length of the line segment
        float t = Vector3.Dot(v, c);    // Intersection point Distance from a

        // Check to see if the point is on the line
        // if not then return the endpoint
        if (t < 0) return l1;
        if (t > d) return l2;

        // get the distance to move from point a
        v *= t;

        // move from point a to the nearest point on the segment
        return l1 + v;
    }

    public static bool IsPointOnLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        var p0 = point;
        var p1 = lineStart;
        var p2 = lineEnd;

        var A = (p0.x - p1.x) / (p2.x - p1.x);
        var B = (p0.y - p1.y) / (p2.y - p1.y);
        var C = (p0.z - p1.z) / (p2.z - p1.z);
        var res = Mathf.Abs(A - B) < EPSILON && Mathf.Abs(B - C) < EPSILON;

        if (res)
        {
            return p1.x < p0.x && p0.x < p2.x && p1.x < p2.x ||
                p1.y < p0.y && p0.y < p2.y && p1.y < p2.y ||
                p1.z < p0.z && p0.z < p2.z && p1.z < p2.z;
        }
        return false;
    }



    public static Vector2 RoundProgress(float t, ref Vector2 position)
    {
        t *= Mathf.PI * 2;
        position.x = Mathf.Sin(t);
        position.y = Mathf.Cos(t);
        return position;
    }
}
