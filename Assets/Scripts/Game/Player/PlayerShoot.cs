using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private Transform gunOffset;

    [SerializeField]
    private float timeBetweenShots;

    private bool fireContinuously;
    private bool fireSingle;
    private float lastFireTime;


    // Update is called once per frame
    void Update()
    {
        if(fireContinuously || fireSingle){

            float timeSinceLastFire = Time.time - lastFireTime;

            if(timeSinceLastFire >= timeBetweenShots){
                FireBullet();

                lastFireTime = Time.time;
                fireSingle = false;
            }
        }
    }
    
    private void FireBullet(){
        GameObject bullet = Instantiate(bulletPrefab, gunOffset.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnFire(InputValue inputValue){
        fireContinuously = inputValue.isPressed;

        if(inputValue.isPressed){
            fireSingle = true;
        }
    }
}
