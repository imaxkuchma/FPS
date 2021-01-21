using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickDetector : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IMovement, IPointerUpHandler
{
    [SerializeField] private GameObject _joystickParent;
    [SerializeField] private GameObject _thumble;

    [SerializeField] private float _radius = 160f;

    private Vector3 _thumbleStartPosition;

    public event Action<bool> IsJoystickUse;
    public event Action<Vector2> Direction;

    private void Awake()
    {
        _joystickParent.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _joystickParent.SetActive(true);
        _thumbleStartPosition = _thumble.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var direction = new Vector3(eventData.position.x, eventData.position.y) - _thumbleStartPosition;
        direction = direction.normalized;
        var distance = Mathf.Clamp(Vector3.Distance(_thumbleStartPosition, eventData.position), 0, _radius);
        
        _thumble.transform.position = _thumbleStartPosition + direction*distance;
        var normalizedMovementDirection = direction * distance / _radius;
        RaisJoystickDirectionEvent(normalizedMovementDirection);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _joystickParent.SetActive(false);
        _thumble.transform.position = _thumbleStartPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _joystickParent.transform.position = eventData.position;
        RaiseJoystickUseEvent(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RaiseJoystickUseEvent(false);
    }

    private void RaiseJoystickUseEvent(bool use)
    {
        IsJoystickUse?.Invoke(use);
    }

    private void RaisJoystickDirectionEvent(Vector2 direction)
    {
        Direction?.Invoke(direction);
    }
}

public interface IMovement
{
    event Action<bool> IsJoystickUse;
    event Action<Vector2> Direction;
}
