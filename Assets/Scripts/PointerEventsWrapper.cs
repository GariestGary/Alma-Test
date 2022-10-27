using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEventsWrapper : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    public UnityEvent<PointerEventData> PointerDownEvent;
    public UnityEvent<PointerEventData> PointerMoveEvent;
    public UnityEvent<PointerEventData> PointerUpEvent;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDownEvent?.Invoke(eventData);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        PointerMoveEvent?.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUpEvent?.Invoke(eventData);
    }
}
