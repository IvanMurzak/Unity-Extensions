using UnityEngine;

public static class ExtensionsQuaternion
{
    public static void Multiply(this Quaternion rotation, Vector3 point, ref Vector3 res)
    {
        float x = rotation.x * 2F;
        float y = rotation.y * 2F;
        float z = rotation.z * 2F;
        float xx = rotation.x * x;
        float yy = rotation.y * y;
        float zz = rotation.z * z;
        float xy = rotation.x * y;
        float xz = rotation.x * z;
        float yz = rotation.y * z;
        float wx = rotation.w * x;
        float wy = rotation.w * y;
        float wz = rotation.w * z;

        res.x = (1F - (yy + zz)) * point.x + (xy - wz) * point.y + (xz + wy) * point.z;
        res.y = (xy + wz) * point.x + (1F - (xx + zz)) * point.y + (yz - wx) * point.z;
        res.z = (xz - wy) * point.x + (yz + wx) * point.y + (1F - (xx + yy)) * point.z;
    }
}