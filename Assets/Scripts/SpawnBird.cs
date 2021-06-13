using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBird : MonoBehaviour
{
    public GameObject bird;

    private bool spawned = false;
    private int totalSpawn = 2;
    private GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Baloon") && !spawned)
        {
            for(int i = 0; i < totalSpawn; i++)
            {
                var pos = transform.position;
                pos.x += .5f;
                var go = Instantiate(bird, transform.position, Quaternion.identity);
                go.GetComponent<BirdController>().SetDirection(new Vector2(Utility.GetRandomDirection(), 1f));
                manager.birds.Add(go);
            }

            spawned = true;
        }
    }
}
