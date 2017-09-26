using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CameraController управляет движением камеры.
/// Камера всегда повернута в сторону target
/// </summary>

namespace Navigator
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(target);
        }
    }
}
