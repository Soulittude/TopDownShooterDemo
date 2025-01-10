using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    private bool _fireContinuously;


    // Update is called once per frame
    void Update()
    {
        if(_fireContinuously){
            FireBullet();
        }
    }
    
    private void FireBullet(){
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        rb.velocity = transform.up * _bulletSpeed;
    }

    private void OnFire(InputValue inputValue){
        _fireContinuously = inputValue.isPressed;
    }
}
