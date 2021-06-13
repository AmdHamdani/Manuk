using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    //[Range(0f, 2f)]
    //public float minSpawnInterval = 0.4f;
    //[Range(0f, 2f)]
    //public float maxSpawnInterval = 0.7f;
    public float spawnInterval = 1f;
    public GameObject obstacle;
    public GameObject cage;
    public float cageSpeed;
    public float obstacleSpeed;
    public Transform leftRange;
    public Transform rightRange;
    [HideInInspector]
    public int totalObstacles = 0;
    private GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnObstacle());
    }

    private IEnumerator SpawnObstacle()
    {
        while (manager.isRunning)
        {
            yield return null;
            yield return new WaitForSeconds(spawnInterval);
            GenerateObstacle();
        }
    }

    private void GenerateObstacle()
    {
        if (totalObstacles < 5)
        {
            var chance = Random.Range(0f, 1f);
            GameObject target;

            if (chance <= 0.6f)
            {
                target = obstacle;
            }
            else
            {
                target = cage;
            }

            var go = Instantiate(target, RandomPosition(), Quaternion.identity);
            go.AddComponent<ObstacleMover>().SetUp(obstacleSpeed, this);
            totalObstacles++;
        }
    }

    public Vector3 RandomPosition()
    {
        var position = Vector3.zero;
        position.x = Random.Range(leftRange.position.x, rightRange.position.x) + 0.2f;
        position.y = transform.position.y;
        return position;
    }
}
