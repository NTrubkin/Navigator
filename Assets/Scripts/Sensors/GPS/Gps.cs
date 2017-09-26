using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestSimpleRNG;

/// <summary>
/// Gps - датчик положения тела в пространстве.
/// Возвращает зашумленные данные. Точность настраивается standardDeviation.
/// Шум, влияющий на удаленность от реального местоположения генерируется по нормальному закону.
/// Направление шумового отклонения - равновероятно
/// </summary>

namespace Navigator
{
    public class Gps : Sensor
    {
        public const string REC_X_TAG = "position X";
        public const string REC_Z_TAG = "position Z";

        [SerializeField]
        private Recorder recorder;

        [SerializeField]
        private GameObject marker;

        [SerializeField]
        private float standardDeviation = 1.0f;

        private const float DEFAULT_MEAN = 0;
        private const float MIN_GEN_ANGLE = 0;
        private const float MAX_GEN_ANGLE = 180;


        public override Vector3 ReadSensor()
        {
            float distanceMod = System.Convert.ToSingle(SimpleRNG.GetNormal(DEFAULT_MEAN, standardDeviation));
            float angleMod = Random.Range(MIN_GEN_ANGLE, MAX_GEN_ANGLE);
            Vector3 modificator = CoordinatesConverter.PolarToCartesian(distanceMod, angleMod);
            Vector3 result = transform.position + modificator;
            UpdateMarker(result);
            recorder.SetData(transform.position.x.ToString(), REC_X_TAG);
            recorder.SetData(transform.position.z.ToString(), REC_Z_TAG);
            return result;
        }

        private void UpdateMarker(Vector3 position)
        {            
            if (visualize)
            {
                marker.SetActive(true);
                marker.transform.position = position;
            }
            else
            {
                marker.SetActive(false);
            }
        }
    }
}