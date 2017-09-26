using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Accelerometer - датчик-акселерометр, определяющий ускорение по каждой из осей
/// </summary>

namespace Navigator
{
    public class Accelerometer : Sensor
    {
        [SerializeField]
        private GameObject marker;
        [SerializeField]
        [Range(0.1f, 10.0f)]
        private float markerScale = 5.0f;

        private Vector3 lastVelocity;
        private Vector3 lastPositon;
        private float lastTimestamp;

        private void Awake()
        {
            lastVelocity = Vector3.zero;
            UpdateLastPosition();
            UpdateLastTimestamp();
        }

        private Vector3 CalculateAcceleration()
        {
            float deltaTime = GetDeltaTime();
            if(deltaTime.Equals(0.0f))
            {
                return Vector3.zero;
            }
            Vector3 acceleration = 2 * (transform.position - lastPositon - lastVelocity * deltaTime) / (deltaTime * deltaTime);
            if(acceleration.x.Equals(float.NaN))
            {
                acceleration.x = 0.0f;
            }
            if (acceleration.y.Equals(float.NaN))
            {
                acceleration.y = 0.0f;
            }
            if (acceleration.z.Equals(float.NaN))
            {
                acceleration.z = 0.0f;
            }
            return acceleration;
        }

        private Vector3 CalculateVelocity()
        {
            return (transform.position - lastPositon) / GetDeltaTime();
        }

        public Vector3 GetLastVelocity()
        {
            return lastVelocity;
        }

        private Vector3 GetLastPosition()
        {
            return lastPositon;
        }

        private void UpdateLastTimestamp()
        {
            lastTimestamp = Time.time;
        }

        private void UpdateLastPosition()
        {
            lastPositon = transform.position;
        }

        private void UpdateLastVelocity()
        {
            lastVelocity = CalculateVelocity();
        }

        private void UpdateParams()
        {
            UpdateLastVelocity();
            UpdateLastPosition();
            UpdateLastTimestamp();
        }

        public float GetDeltaTime()
        {
            return Time.time - lastTimestamp;
        }

        public override Vector3 ReadSensor()
        {
            Vector3 acceleration = CalculateAcceleration();
            UpdateMarker(acceleration);
            UpdateParams();
            return acceleration;
        }

        private void UpdateMarker(Vector3 acceleration)
        {
            if (visualize)
            {
                marker.SetActive(true);
                float markerSize = acceleration.magnitude * markerScale;
                marker.transform.localScale = new Vector3(markerSize, marker.transform.localScale.y, marker.transform.localScale.z);
                marker.transform.rotation = Quaternion.Euler(0, Vector3.Angle(Vector3.right, acceleration), 0);
            }
            else
            {
                marker.SetActive(false);
            }
        }
    }
}
