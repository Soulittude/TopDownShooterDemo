using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D rb;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float changeDirectionCooldown;

    // Start is called before the first frame update
    private void Awake()
    {
        rb  = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;
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

    private void RotateTowardsTarget(){
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }

    private void SetVelocity(){
            rb.velocity = _targetDirection * moveSpeed;
    }
}
