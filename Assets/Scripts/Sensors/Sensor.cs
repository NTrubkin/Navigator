using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sensor - абстрактный датчик. Реализует необходимый минимальный функционал любого датчика
/// </summary>

namespace Navigator
{
    public abstract class Sensor : MonoBehaviour, ISensory
    {
        [SerializeField]
        protected bool visualize = true;

        public abstract Vector3 ReadSensor();
    }
}
