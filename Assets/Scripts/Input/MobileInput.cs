using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour
{
    private int xInput = 0;
    private int yInput = 0;

    private Vector2 inputVector;

    [SerializeField] private Transform controlPanel;
    private PlayerController playerController;
    private void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Destroy(controlPanel.gameObject);
            Destroy(this);
        }
        Application.targetFrameRate = -1;

        Button leftBtn = controlPanel.Find("leftBtn").GetComponent<Button>();
        Button rightBtn = controlPanel.Find("rightBtn").GetComponent<Button>();
        Button upBtn = controlPanel.Find("upBtn").GetComponent<Button>();
        Button downBtn = controlPanel.Find("downBtn").GetComponent<Button>();

        AddButtonEventHandler(leftBtn, () => SideArrowPressed(-1), () => SideArrowPressed(1));
        AddButtonEventHandler(rightBtn, () => SideArrowPressed(1), () => SideArrowPressed(-1));
        AddButtonEventHandler(upBtn, () => VerticalArrowPressed(1), () => VerticalArrowPressed(-1));
        AddButtonEventHandler(downBtn, () => VerticalArrowPressed(-1), () => VerticalArrowPressed(1));

        playerController = PlayerController.Instance;
    }

    private void AddButtonEventHandler(Button button, ButtonAction onPointerDown, ButtonAction onPointerUp)
    {
        ButtonEventHandler eventHandler = button.gameObject.AddComponent<ButtonEventHandler>();
        eventHandler.onPointerDownAction = onPointerDown;
        eventHandler.onPointerUpAction = onPointerUp;
    }
    private void Update()
    {
        inputVector = new Vector2(xInput, yInput);
        playerController.SetInputVector(inputVector);
    }

    public void SideArrowPressed(int x)
    {
        Debug.Log("x " + x);
        xInput += x;
    }
    public void VerticalArrowPressed(int y)
    {
        Debug.Log("y " + y);
        yInput += y;
    }
}

