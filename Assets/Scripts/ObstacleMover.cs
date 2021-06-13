using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private float speed;
    private ObstacleSpawner spawner;

    public void SetUp(float speed, ObstacleSpawner spawner)
    {
        this.speed = speed;
        this.spawner = spawner;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if(collision.CompareTag("Remover"))
        {
            if(spawner != null)
            {
                spawner.totalObstacles--;
            }
            Destroy(gameObject);
        }
    }
}
