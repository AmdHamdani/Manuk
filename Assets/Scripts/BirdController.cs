using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float speed = 1f;
    public float baloonDistance = 3f;
    public float intervalToStop = 1.5f;
    public float checkFlockInterval = 5;
    public float birdRadius = 1f;

    private bool isMoving = false;
    private bool canCheckFlock = false;
    private bool disableMoveUp = false;
    private Vector3 direction = Vector3.zero;
    private Transform baloon;
    private GameManager manager;
    private Animator animator;

    private void Start()
    {
        baloon = GameObject.FindGameObjectWithTag("Baloon").transform;
        manager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        StartCoroutine(StopMovement());
        StartCoroutine(CheckFlock());
    }

    private void Update()
    {
        if (direction != Vector3.zero && isMoving)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Region"))
        {
            disableMoveUp = true;
        }

        if(collision.CompareTag("Obstacle"))
        {
            animator.SetTrigger("Fall");
            gameObject.SetActive(false);
            manager.birds.Remove(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Baloon"))
        {
            baloon = collision.transform;
            CheckBaloonPosition(collision.transform);
        }

        if(collision.CompareTag("Wall"))
        {
            CheckWall(collision);
        }

        if (collision.CompareTag("Region"))
        {
            disableMoveUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Region"))
        {
            disableMoveUp = false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, birdRadius);
    }
#endif

    private IEnumerator StopMovement()
    {
        while(manager.isRunning)
        {
            yield return null;
            if(isMoving)
            {
                yield return new WaitForSeconds(intervalToStop);
                var distance = Vector2.Distance(baloon.position, transform.position);
                if (distance >= baloonDistance)
                {
                    isMoving = false;
                    direction = Vector2.zero;
                }
            }
        }
    }

    private IEnumerator CheckFlock()
    {
        while(manager.isRunning)
        {
            yield return null;
            if(canCheckFlock)
            {
                yield return new WaitForSeconds(checkFlockInterval);
                var colliders = Physics2D.OverlapCircleAll(transform.position, birdRadius);

                var flockCounter = 0;
                foreach(var collider in colliders)
                {
                    if(collider.CompareTag("Bird"))
                    {
                        flockCounter++;
                    }
                }

                if(flockCounter <= 1)
                {
                    animator.SetTrigger("Fall");
                    gameObject.SetActive(false);
                    manager.birds.Remove(gameObject);
                }
            }
        }
    }

    private void CheckBaloonPosition(Transform baloon)
    {
        if (IsInLeft(baloon.position, transform.position))
        {
            SetDirection(new Vector2(1, disableMoveUp? 0f: Utility.GetRandomDirection()));
        }
        else if (IsInRight(baloon.position, transform.position))
        {
            SetDirection(new Vector2(-1, disableMoveUp ? 0f : Utility.GetRandomDirection()));
        }
        else if (IsBelow(baloon.position, transform.position))
        {
            SetDirection(new Vector2(Utility.GetRandomDirection(), disableMoveUp ? 0f : 1f));
        }
        else if (IsAbove(baloon.position, transform.position))
        {
            SetDirection(new Vector2(Utility.GetRandomDirection(), -1));
        }
    }

    private void CheckWall(Collider2D collision)
    {
        var wall = collision.transform;
        if (IsInLeft(wall.position, transform.position))
        {
            SetDirection(new Vector2(1, Utility.GetRandomDirection()));
        }
        else if (IsInRight(wall.position, transform.position))
        {
            SetDirection(new Vector2(-1, Utility.GetRandomDirection()));
        }
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
        isMoving = true;
        canCheckFlock = true;
    }

    private bool IsInLeft(Vector3 a, Vector3 b)
    {
        return (int)a.x < (int)b.x && Difference(a.y, b.y) <= 1f;
    }

    private bool IsInRight(Vector3 a, Vector3 b)
    {
        return (int)a.x > (int)b.x && Difference(a.y, b.y) <= 1f;
    }

    private bool IsBelow(Vector3 a, Vector3 b)
    {
        return (int)a.y < (int)b.y && Difference(a.x, b.x) <= 1f;
    }

    private bool IsAbove(Vector3 a, Vector3 b)
    {
        return (int)a.y > (int)b.y && Difference(a.x, b.x) <= 1f;
    }

    private float Difference(float a, float b)
    {
        return Mathf.Abs(a) - Mathf.Abs(b);
    }
}
