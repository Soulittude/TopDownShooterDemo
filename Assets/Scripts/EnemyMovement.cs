using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float screenBorder;
    private Rigidbody2D rb;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float changeDirectionCooldown;
    private Camera mainCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        rb  = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection(){
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        HandleEnemyOffScreen();
    }

    private void HandleRandomDirectionChange(){
        changeDirectionCooldown -= Time.deltaTime;

        if(changeDirectionCooldown <= 0){
            float angleChange = Random.Range(-90f, 90);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, Vector3.forward);
            _targetDirection = rotation * _targetDirection;

            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting(){
        if(_playerAwarenessController.AwareOfPlayer){
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
    }

    private void HandleEnemyOffScreen(){
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        if((screenPosition.x < screenBorder && _targetDirection.x < 0) || (screenPosition.x > mainCamera.pixelWidth - screenBorder && _targetDirection.x > 0)){
            _targetDirection = new Vector2(_targetDirection.x * -1, _targetDirection.y);
        }

        if((screenPosition.y < screenBorder && _targetDirection.y < 0) || (screenPosition.y > mainCamera.pixelHeight - screenBorder && _targetDirection.y > 0)){
            _targetDirection = new Vector2(_targetDirection.x, _targetDirection.y * -1);
        }
    }

    private void RotateTowardsTarget(){
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }

    private void SetVelocity(){
            rb.velocity = _targetDirection * moveSpeed;
    }
}
