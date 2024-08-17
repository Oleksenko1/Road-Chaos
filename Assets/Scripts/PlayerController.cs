using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Forward movement")]
    [SerializeField] private float maxForwardSpeed = 30f;
    [SerializeField] private float forwardAccelTime = 0.2f;
    [SerializeField] private float forwardStopTime = 0.3f;

    [Header("Turn movement")]
    [SerializeField] private float maxTurnSpeed = 20f;
    [SerializeField] private float turnAccelTime = 0.2f;
    [SerializeField] private float turnStopTime = 0.15f;

    [Header("Sounds")]
    [SerializeField] private AudioClip explodeSFX;
    [SerializeField] private AudioClip carCrashSFX;

    [Header("VFX")]
    [SerializeField] private Transform explodeVFX;

    public event Action OnDied;

    private Vector2 inputVector = Vector2.zero;
    private Rigidbody rb;
    private float forwardSpeed = 0;
    private float turnSpeed = 0;

    private bool isAlive = true;
    private float yVelRef = 0; // Ref value that is needed for smooth forward speed stop
    private float xVelRef = 0; // Ref value that is needed for smooth turn speed stop
    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody>();

        Time.timeScale = 1f;
    }
    private void Update()
    {
        if(isAlive)
        {
            MovementHandler();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MovementHandler() // Handles all of the movement
    {
        ForwardMovement();

        TurnMovement();

        Vector3 velocity = new Vector3(turnSpeed, 0, forwardSpeed).normalized;
        rb.velocity = new Vector3(Mathf.Abs(turnSpeed) * velocity.x, 0, Mathf.Abs(forwardSpeed) * velocity.z);
    }
    private void ForwardMovement()
    {
        float xInput = inputVector.x;

        if (xInput != 0) // If player is pressing A or D
        {
            turnSpeed = Mathf.SmoothDamp(turnSpeed, xInput * maxTurnSpeed, ref xVelRef, turnAccelTime);
        }
        else // Smooth stop
        {
            turnSpeed = Mathf.SmoothDamp(turnSpeed, 0, ref xVelRef, turnStopTime);
        }
    }
    private void TurnMovement()
    {
        float yInput = inputVector.y;

        if (yInput != 0) // If player is pressing W or S
        {
            forwardSpeed = Mathf.SmoothDamp(forwardSpeed, yInput * maxForwardSpeed, ref yVelRef, forwardAccelTime);
        }
        else // Smooth stop
        {
            forwardSpeed = Mathf.SmoothDamp(forwardSpeed, 0, ref yVelRef, forwardStopTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }
    public void SetInputVector(Vector2 inputVector)
    {
        this.inputVector = inputVector;
    }
    private void Die()
    {
        isAlive = false;
        OnDied?.Invoke();

        AudioSource.PlayClipAtPoint(explodeSFX, transform.position);
        AudioSource.PlayClipAtPoint(carCrashSFX, transform.position);

        Instantiate(explodeVFX, transform.position, explodeVFX.rotation);

        Destroy(gameObject);
    }
}
