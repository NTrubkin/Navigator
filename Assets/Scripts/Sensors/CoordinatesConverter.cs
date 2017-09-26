using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Navigator
{
    public static class CoordinatesConverter
    {
        public static Vector3 PolarToCartesian(float radius, float angle)
        {
            return new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));

        }

        public static float CartesianToPolarAngle(Vector3 coord)
        {
            return Mathf.Rad2Deg * Mathf.Atan(coord.x / coord.z);
        }
    }
}
