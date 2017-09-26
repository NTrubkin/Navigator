using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Odometer - датчик-одометр. Определяет дистанцию, пройденную телом
/// </summary>

namespace Navigator
{
    public class Odometer : Sensor
    {
        public const string REC_TAG = "distance";

        [SerializeField]
        private Recorder recorder;

        private Vector3 lastPosition;
        private float totalDistance = 0;

        private float modificator = 1.0f;

        public override Vector3 ReadSensor()
        {
            float distance = CalculateDistance();
            totalDistance += distance;
            UpdateLastPosition();
            Debug.Log(totalDistance);
            recorder.SetData(distance.ToString(), REC_TAG);
            return new Vector3(distance, 0, 0);
        }

        private void UpdateLastPosition()
        {
            lastPosition = transform.position;
        }

        private float CalculateDistance()
        {
            return (transform.position - lastPosition).magnitude + modificator;
        }
    }
}