using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBird : MonoBehaviour
{
    public GameObject bird;
    public float minOffset, maxOffset;

    private bool spawned = false;
    private int totalSpawn = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Baloon") && !spawned)
        {
            for(int i = 0; i < totalSpawn; i++)
            {
                var pos = transform.position;
                pos.x += .5f;
                var go = Instantiate(bird, transform.position + RandomOffset(minOffset, maxOffset), Quaternion.identity);
                go.GetComponent<BirdController>().SetDirection(new Vector2(Utility.GetRandomDirection(), 1f));
            }

            spawned = true;
            gameObject.SetActive(false);
        }
    }
    private Vector3 RandomOffset(float min, float max){
        float x = Random.Range(min, max);
        float y = Random.Range(min, max);
        return new Vector2(x, y);
    }
}
