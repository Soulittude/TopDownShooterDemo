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

    // Start is called before the first frame update
    private void Awake()
    {
        rb  = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection(){
        if(_playerAwarenessController.AwareOfPlayer){
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        } else {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget(){
        if(_targetDirection == Vector2.zero){
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }

    private void SetVelocity(){
        
        if(_targetDirection == Vector2.zero){
            rb.velocity = Vector2.zero;
        }
        else{
            rb.velocity = _targetDirection * moveSpeed;
        }

    }
}
