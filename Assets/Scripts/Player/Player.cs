using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveSmoothInput;
    private Vector2 moveSmoothVelocity;
    private Camera camera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        if (!IsOwner) return;
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        Rotation();
    }

    private void SetPlayerVelocity()
    {
        moveSmoothInput = Vector2.SmoothDamp(moveSmoothInput, moveInput, ref moveSmoothVelocity, 0.1f);

        rb.velocity = moveSmoothInput * speed;
    }

    private void Rotation()
    {
        if(moveInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, moveSmoothInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }


}
