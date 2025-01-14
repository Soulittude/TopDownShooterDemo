using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake(){
        mainCamera = Camera.main;
    }

    private void Update(){
        DestroyWhenOffScreen();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<EnemyMovement>()){
            HealthController healthController = collision.GetComponent<HealthController>();
            healthController.TakeDamage(10);
            Destroy(gameObject);
        }
    }

    private void DestroyWhenOffScreen(){
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(transform.position);
        if(screenPosition.x < 0 || 
        screenPosition.x > mainCamera.pixelWidth || 
        screenPosition.y < 0 || 
        screenPosition.y > mainCamera.pixelHeight){
            Destroy(gameObject);
        }
    }
}
