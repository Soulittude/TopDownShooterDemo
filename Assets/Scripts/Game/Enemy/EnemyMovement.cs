using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float screenBorder;

    [SerializeField]
    private float obstacleCheckCircleRadius;

    [SerializeField]
    private float obstacleCheckDistance;

    [SerializeField]
    private LayerMask obstacleLayerMask;

    private Rigidbody2D rb;
    private PlayerAwarenessController playerAwarenessController;
    private Vector2 targetDirection;
    private float changeDirectionCooldown;
    private Camera mainCamera;
    private RaycastHit2D[] obstacleCollisions;
    private float obstacleAvoidanceCooldown;
    private Vector2 obstacleAvoidanceTargetDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        targetDirection = transform.up;
        mainCamera = Camera.main;
        obstacleCollisions = new RaycastHit2D[10];
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        HandleObstacles();
        HandleEnemyOffScreen();
    }

    private void HandleRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.deltaTime;

        if (changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            targetDirection = rotation * targetDirection;

            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (playerAwarenessController.AwareOfPlayer)
        {
            targetDirection = playerAwarenessController.DirectionToPlayer;
        }
    }

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < screenBorder && targetDirection.x < 0) ||
            (screenPosition.x > mainCamera.pixelWidth - screenBorder && targetDirection.x > 0))
        {
            targetDirection = new Vector2(-targetDirection.x, targetDirection.y);
        }

        if ((screenPosition.y < screenBorder && targetDirection.y < 0) ||
            (screenPosition.y > mainCamera.pixelHeight - screenBorder && targetDirection.y > 0))
        {
            targetDirection = new Vector2(targetDirection.x, -targetDirection.y);
        }
    }

    private void HandleObstacles()
    {
        obstacleAvoidanceCooldown -= Time.deltaTime;

        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(obstacleLayerMask);

        int numberOfCollisions = Physics2D.CircleCast(
            transform.position,
            obstacleCheckCircleRadius,
            transform.up,
            contactFilter,
            obstacleCollisions,
            obstacleCheckDistance);

        for (int index = 0; index < numberOfCollisions; index++)
        {
            var obstacleCollision = obstacleCollisions[index];

            if (obstacleCollision.collider.gameObject == gameObject)
            {
                continue;
            }

            if (obstacleAvoidanceCooldown <= 0)
            {
                obstacleAvoidanceTargetDirection = obstacleCollision.normal;
                obstacleAvoidanceCooldown = 0.5f;
            }

            var targetRotation = Quaternion.LookRotation(transform.forward, obstacleAvoidanceTargetDirection);
            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            targetDirection = rotation * Vector2.up;
            break;
        }
    }

    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        rb.velocity = transform.up * speed;
    }
}
