using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private ICollectableBehaviour collectableBehaviour;

    private void Awake()
    {
        collectableBehaviour = GetComponent<ICollectableBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            collectableBehaviour.OnCollected(player.gameObject);
            Destroy(gameObject);
        }

    }
}
