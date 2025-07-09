using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ContinuousSwipeDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerUpHandler
{
    public float touchSensitivity = 0.2f;
    private Vector2 startTouchPosition;
    private Vector2 currentSwipeDelta;
    private bool isSwiping = false;

    public UnityEvent<Vector2> swipeEvent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startTouchPosition = eventData.position;
        isSwiping = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isSwiping) return;

        float distance = Vector2.Distance(startTouchPosition, eventData.position);
        if (distance > 0.5f)
        {
            currentSwipeDelta = eventData.position - startTouchPosition;
            swipeEvent.Invoke(new Vector2(currentSwipeDelta.x, -currentSwipeDelta.y) * touchSensitivity);
            startTouchPosition = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSwiping = false;
        swipeEvent.Invoke(Vector2.zero);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isSwiping = false;
        swipeEvent.Invoke(Vector2.zero);
    }

    public void TouchSensitivity(float sensitivity)
    {
        touchSensitivity = sensitivity;
    }
}