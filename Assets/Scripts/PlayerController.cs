using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float screenBorder;
    private Rigidbody2D rb;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Camera mainCamera;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();

    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    private void RotateInDirectionOfInput(){
        if(_movementInput != Vector2.zero){
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        
            rb.MoveRotation(rotation);
        }
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothVelocity, 0.1f);
        
        rb.velocity = _smoothedMovementInput * moveSpeed;
    
        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen(){
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        if((screenPosition.x < screenBorder && rb.velocity.x < 0)
        || (screenPosition.x > mainCamera.pixelWidth - screenBorder && rb.velocity.x > 0)){
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if((screenPosition.y < screenBorder && rb.velocity.y < 0)
        || (screenPosition.y > mainCamera.pixelHeight - screenBorder && rb.velocity.y > 0)){
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}