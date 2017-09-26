using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SmoothPlayerController - контроллер движения персонажа
/// Ввод с помощью мыши.
/// Сглаживает ввод сглаживается для имитации иннертности персонажа а также возможности рассчета ускорения
/// Доступно переключение контроля клавишей controllSwitchKey
/// </summary>

public class SmoothPlayerController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private float sensetive = 0.2f;             // чувствительность ввода. Чем выше, тем большее расстояние пройдет персонаж

    [SerializeField]
    private float smoothTime = 0.3F;

    [SerializeField]
    private bool isControllable = true;

    private const KeyCode controllSwitchKey = KeyCode.Q;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(controllSwitchKey))
        {
            SwitchControll();
        }
        Vector3 target = new Vector3(transform.position.x + GetInput().x, 0, transform.position.z + GetInput().z);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }

    private void SwitchControll()
    {
        isControllable = !isControllable;
    }

    private Vector3 GetInput()
    {
        if(isControllable)
        {
            return new Vector3(Input.GetAxis("Mouse X") * sensetive, 0, Input.GetAxis("Mouse Y") * sensetive);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
