using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Navigator - прибор определения местоположения в пространстве. Работает неточно.
/// Базируется на работе датчиков: GPS, акселерометра и одометра.
/// Комбинирует данные на основе упрощенного фильтра Калмана, где коэффициент Калмана подбирается вручную
/// </summary>

namespace Navigator
{
    public class Navigator : Sensor
    {
        [SerializeField]
        private bool isActive = true;
        [SerializeField]
        [Range(1, 100)]
        private float frequency;
        [SerializeField]
        [Range(0.001f, 0.999f)]
        private float kalmanKoef;

        [SerializeField]
        private Sensor gps;
        [SerializeField]
        private Sensor odometer;
        [SerializeField]
        private Sensor accelerometerAdapter;

        [SerializeField]
        private GameObject marker;

        private Vector3 lastCalculatedPos;

        private void Start()
        {
            lastCalculatedPos = transform.position;
            StartCoroutine(UseRepeately());
        }

        private IEnumerator UseRepeately()
        {
            while (isActive)
            {
                Use();
                yield return new WaitForSeconds(1 / frequency);
            }
        }

        public void Use()
        {
            UpdateMarker(ReadSensor());
        }

        public override Vector3 ReadSensor()
        {            
            Vector3 readedPos = gps.ReadSensor();
            Vector3 calculatedPos = lastCalculatedPos + odometer.ReadSensor().x * accelerometerAdapter.ReadSensor();
            Vector3 result = readedPos * kalmanKoef + (1.0f - kalmanKoef) * calculatedPos;
            lastCalculatedPos = result; 
            return result;
        }

        private void UpdateMarker(Vector3 position)
        {
            if(visualize)
            {                
                marker.transform.position = position;
                marker.SetActive(true);
            }
            else
            {
                marker.SetActive(false);
            }            
        }
    }
}
