using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AccelerometerAdapter - датчик определения направления ускорения, основаный на акселерометре
/// </summary>
namespace Navigator
{  
    public class AccelerometerAdapter : Sensor
    {
        public const string REC_X_TAG = "normal direction X";
        public const string REC_Z_TAG = "normal direction Y";

        [SerializeField]
        private Recorder recorder;

        [SerializeField]
        private Accelerometer accelerometer;
        [SerializeField]
        private GameObject marker;
        [SerializeField]
        private GameObject normalMarker;

        private Vector3 modificator = new Vector3(0, 0, 0);

        public override Vector3 ReadSensor()
        {
            Vector3 direction = CalculateDirection();
            Vector3 normalDirection = Vector3.Normalize(direction);
            UpdateMarker(direction, normalDirection);
            recorder.SetData(normalDirection.x.ToString(), REC_X_TAG);
            recorder.SetData(normalDirection.z.ToString(), REC_Z_TAG);
            return normalDirection;
        }

        private Vector3 CalculateDirection()
        {
            Vector3 lastVelocity = accelerometer.GetLastVelocity();
            float deltaTime = accelerometer.GetDeltaTime();
            if(deltaTime.Equals(0.0f))
            {
                return Vector3.zero;
            }

            Vector3 acceleration = accelerometer.ReadSensor();
            Vector3 direction = lastVelocity * deltaTime + (acceleration * deltaTime * deltaTime / 2);
            return direction + modificator;
        }

        public void UpdateMarker(Vector3 direction, Vector3 normalDirection)
        {
            if (visualize)
            {
                marker.transform.localPosition = direction;
                normalMarker.transform.localPosition = normalDirection;
                marker.SetActive(true);
            }
            else
            {
                marker.SetActive(false);
            }
        }
    }
}