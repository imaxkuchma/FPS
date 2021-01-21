using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{

    private Transform _playerCamera;
    private float _rotationX;

    [SerializeField] private float sensitivityX = 5.0f;
    [SerializeField] private float sensitivityY = 5.0f;

    private void Start()
    { 
        _playerCamera = transform.GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x > (Screen.width / 2))
            {    
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 delta = touch.deltaPosition;

                    float axisX = delta.x * sensitivityX * Time.deltaTime;

                    float axisY = delta.y * sensitivityY * Time.deltaTime;
                    _rotationX += axisY * -1;

                    _rotationX = Mathf.Clamp(_rotationX,-90,90);

                    transform.Rotate(Vector3.up * axisX);
                    _playerCamera.localRotation = Quaternion.Euler(_rotationX, 0, 0);
                }
                break;
            }

        }
    }
}
