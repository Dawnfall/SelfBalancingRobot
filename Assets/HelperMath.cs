using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperMath
{
    public static float GetAngleBetween(Vector3 from, Vector3 to, Vector3 referenceNormal)
    {
        float cosAngle = Vector3.Dot(from, to);
        if (cosAngle == 1f)
            return 0f;

        Vector3 cross = Vector3.Cross(from, to);
        Debug.Log(Vector3.Dot(cross, referenceNormal));
        if (Vector3.Dot(cross, referenceNormal) > 0f)
            return Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
        return -Mathf.Acos(cosAngle) * Mathf.Rad2Deg;
    }
}
