using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEventHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ButtonAction onPointerDownAction;
    public ButtonAction onPointerUpAction;

    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDownAction?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUpAction?.Invoke();
    }
}

public delegate void ButtonAction();
