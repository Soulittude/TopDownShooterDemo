using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollectableDrop : MonoBehaviour
{
    [SerializeField]
    private float dropChance;

    private CollectableSpawn collectableSpawn;

    private void Awake()
    {
        collectableSpawn = FindObjectOfType<CollectableSpawn>();
    }

    public void RandomlyDropCollectable()
    {
        float random = Random.Range(0f, 1f);

        if(dropChance >= random)
        {
            collectableSpawn.SpawnCollectable(transform.position);
        }
    }
}