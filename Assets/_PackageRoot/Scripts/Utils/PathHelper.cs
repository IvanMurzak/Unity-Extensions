using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PathHelper
{
    public static int GetNearestPointIndex(Vector3[] vectors, float length, float p)
    {
        if (vectors.Length <= 1) return 0;
        if (vectors.Length == 2) return p < 0.5f ? 0 : 1;

        var point = GetPoint(vectors, length, p);

        return GetNearestPointIndex(vectors, length, point);
    }

    public static int GetNearestPointIndex(Vector3[] vectors, float length, Vector3 point)
    {
        if (vectors.Length <= 1) return 0;

        var minLength = float.MaxValue;
        var minPointIndex = 0;
        for (int i = 0; i < vectors.Length; i++)
        {
            var distance = Vector3.Distance(point, vectors[i]);
            if (distance < minLength)
            {
                minLength = distance;
                minPointIndex = i;
            }
        }
        return minPointIndex;
    }

    public static Vector3 GetNearestPoint(Vector3[] path, float length, Vector3 point)
    {
        float percent;
        return GetNearestPoint(path, length, point, out percent);
    }

    public static float GetNearestPointPercent(Vector3[] path, float length, Vector3 point)
    {
        float percent;
        GetNearestPoint(path, length, point, out percent);
        return percent;
    }

    public static Vector3 GetNearestPoint(Vector3[] path, float length, Vector3 point, out float percent)
    {
        var bufferPathLength = 0f;
        var minPathLength = float.MaxValue;
        var minDistance = float.MaxValue;
        Vector3 theNearestPoint = Vector3.zero;
        for(int i = 1; i < path.Length; i++)
        {
            var nearestPoint = MathHelper.GetNearestPointOnLine(path[i - 1], path[i], point);
            var nearestDistance = Vector3.Distance(nearestPoint, point);
            if (minDistance > nearestDistance)
            {
                minDistance = nearestDistance;
                minPathLength = bufferPathLength + Vector3.Distance(path[i - 1], nearestPoint);
                theNearestPoint = nearestPoint;
            }
            bufferPathLength += Vector3.Distance(path[i - 1], path[i]);
        }

        percent = minPathLength / length;
        return theNearestPoint;
    }

    public static Vector3 GetPoint(Vector3[] vectors, float length, float p)
    {
        int index = 0;
        return GetPoint(vectors, length, p, out index);
    }

    public static Vector3 GetPoint(Vector3[] vectors, float length, float p, out int index)
    {
        if (vectors.Length < 1)
        {
            index = -1;
            return Vector3.zero; // if the list is empty
        }
        else if (vectors.Length < 2)
        {
            return vectors[index = 0]; //if there is only one point in the list
        }

        float dist = length * Mathf.Clamp(p, 0, 1);

        for (int i = 0; i < vectors.Length - 1; i++)
        {
            Vector3 v0 = vectors[i];
            Vector3 v1 = vectors[i + 1];
            float this_dist = Vector3.Distance(v0, v1); // TODO: it may produce NaN value of "this_dist=0" it is possible when 'v0' = 'v2'

            if (this_dist < dist)
            {
                dist -= this_dist; //if the remaining distance is more than the distance between these vectors then minus the current distance from the remaining and go to the next vector
                continue;
            }

            index = i + 1;
            return Vector3.Lerp(v0, v1, dist / this_dist); //if the distance between these vectors is more or equal to the remaining distance then find how far along the gap it is and return
        }
        return vectors[index = vectors.Length - 1];
    }

    public static Vector3[] SubPathFixed(Vector3[] vectors, float length, float pStart, float pEnd)
    {
        var from = GetNearestPointIndex(vectors, length, pStart);
        var to = GetNearestPointIndex(vectors, length, pEnd);
        var subLength = to - from + 1;

        var subVectors = new Vector3[subLength];
        System.Array.Copy(vectors, from, subVectors, 0, subLength);
        return subVectors;
    }

    public static Vector3[] SubPath(Vector3[] vectors, float length, float pStart, float pEnd)
    {
        int from = 0, to = 0;

        var startPoint = GetPoint(vectors, length, pStart, out from);
        var endPoint = GetPoint(vectors, length, pEnd, out to);

        var originalSubLenght = to - from + 1;

        var subLength = originalSubLenght;

        if (vectors[from] != startPoint) subLength++;
        if (vectors[to] != endPoint) subLength++;

        var subVectors = new Vector3[subLength];

        if (vectors[from] != startPoint) subVectors[0] = startPoint;
        if (vectors[to] != endPoint) subVectors[subVectors.Length - 1] = endPoint;

        System.Array.Copy(vectors, from, subVectors, vectors[from] != startPoint ? 1 : 0, originalSubLenght);
        return subVectors;
    }

    public static float Length(Vector3[] vertices)
    {
        float length = 0;
        for (int i = 1; i < vertices.Length; i++)
            length += Vector3.Distance(vertices[i - 1], vertices[i]);
        return length;
    }
    public static float Length(List<Vector3> vertices)
    {
        float length = 0;
        for (int i = 1; i < vertices.Count; i++)
            length += Vector3.Distance(vertices[i - 1], vertices[i]);
        return length;
    }

    public static void RandomizeXY(ref Vector3[] vectors, float xRange, float yRange)
    {
        for(int i = 0; i < vectors.Length; i++)
        {
            vectors[i].x += Random.Range(-xRange, xRange);
            vectors[i].y += Random.Range(-yRange, yRange);
        }
    }

    public static void RandomizePath(ref Vector3[] vectors, Vector3[] perpendiculars, Vector2 randomRangeOffset)
    {
        for (int i = 1; i < vectors.Length - 1; i++)
        {
            vectors[i] += perpendiculars[i-1] * randomRangeOffset.Random();
        }
    }

    public static Vector3[] BuildPerpendiculars(Vector3[] vertices3d, Vector3 normal)
    {
        var perpendiculars = new Vector3[vertices3d.Length - 1];
        for (int i = 1; i < vertices3d.Length; i++)
            perpendiculars[i - 1] = Vector3.Cross(vertices3d[i] - vertices3d[i - 1], normal);
        return perpendiculars;
    }
}
