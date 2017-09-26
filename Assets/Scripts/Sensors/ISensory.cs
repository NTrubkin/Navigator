using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ISensory - интерфейс, который реализует каждый датчик
/// </summary>
namespace Navigator
{
    public interface ISensory
    {
        /// <summary>
        /// Счититывает показания датчика
        /// </summary>
        /// <returns>Показания датчика в текущий момент</returns>
        Vector3 ReadSensor();
    }
}
