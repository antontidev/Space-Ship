using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLookAddOn : MonoBehaviour
{
    [Range(0f, 10f)] public float LookSpeed = 1f;
    public bool InvertY = false;
    private CinemachineFreeLook _freeLookComponent;

    void Start()
    {
        _freeLookComponent = GetComponent<CinemachineFreeLook>();
    }

    void OnLook(InputAction.CallbackContext context)
    {

    }
}
