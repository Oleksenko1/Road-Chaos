using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCInput : MonoBehaviour
{
    private PlayerController playerController;
    private Vector2 inputVector;
    private void Awake()
    {
        if (Application.platform != RuntimePlatform.WindowsPlayer)
        {
            Debug.Log("Windows controlls turned off");
            Destroy(this);
        }
    }
    private void Start()
    {
        playerController = PlayerController.Instance;
    }
    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerController.SetInputVector(inputVector);
    }
}
